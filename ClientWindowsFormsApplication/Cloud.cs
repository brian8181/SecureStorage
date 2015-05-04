using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Utility;
using Utility.IO;

namespace ClientWindowsFormsApplication
{
    public class ClientCloud : IRemoteData
    {
        //BKP to remove direct reference to service
        IStorage remote_store = null;
        private ServiceReference.IService cloud = new ServiceReference.ServiceClient();
        private byte[] key = null;
        private byte[] iv  = null;
        private const string TMP_FILE_NAME = "tmp.xml";
        private const string ROOT_FILE_NAME = "/";
        private const int CHUNK_SIZE = 1000;
        
        /// <summary>
        /// true if key has been loaded, otherwise false
        /// </summary>
        public bool KeyLoaded
        {
            get
            {
                if(key == null || iv ==  null)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// create a key and write it to specified name
        /// </summary>
        /// <param name="name">name to write the key</param>
        public void CreateKey(string path)
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] key = new byte[aes.Key.Length + aes.IV.Length];

            Array.Copy(aes.Key, key, aes.Key.Length);
            Array.Copy(aes.IV, 0, key, aes.Key.Length, aes.IV.Length);

            File.WriteAllBytes(path, key);
        }

        /// <summary>
        /// loads key from file into variables (key & iv)
        /// </summary>
        /// <param name="name"></param>
        public void LoadKey(string path)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            key = new byte[32];
            iv = new byte[16];

            Array.Copy(key_iv, key, 32);
            Array.Copy(key_iv, 32, iv, 0, 16);
        }

        //TODO move to GUI, all name should be in cloud format in first place
        /// <summary>
        /// gets name / name used for cloud, aka removes local root & adjust slashes
        /// </summary>
        /// <param name="name">name / name to convert</param>
        /// <returns>cloud name / name</returns>
        private string GetCloudPath(string path)
        {
            string local = Properties.Settings.Default.init_input_dir;
            path = path.Remove(0, local.Length);
            path = path.Replace('\\', '/');
            path = path.TrimStart('/'); // may trim start ?
            return path + "/";
        }
        
        /// <summary>
        /// inititalize/create an empty root attempt to send / store
        /// </summary>
        /// <returns>returns true if successful, otherwise false</returns>
        public bool Initialize()
        {
            cloud.DeleteAll();
            return CreateDirectoryXml(ROOT_FILE_NAME);
        }

        /// <summary>
        /// internal function, abstarcts Creating direcoties at specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CreateDirectoryXml(string name)
        {
            // create a file system on server
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // create xml file for root directory
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            // create empty doc 
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            string secure_named_dir = GetSecureName(name);
            doc.Save(secure_named_dir);

            // encrypt xml file
            byte[] xml_file_data = File.ReadAllBytes(secure_named_dir);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);

            // send/store it using secure name
            if (cloud.Exists(secure_named_dir) != true)
                return cloud.CreateAppend(secure_named_dir, encrypted_xml_file_data);

            return false;
        }
        
        /// <summary>
        /// initialize/create a directory based on input directory
        /// </summary>
        /// <param name="in_path"></param>
        /// <param name="out_path"></param>
        public void InitializeLocalRoot(string input_dir, string output_dir)
        {
            DirectoryInfo di = new DirectoryInfo(input_dir);
            InitializeLocal(di, output_dir);
        }

        private void InitializeLocal(DirectoryInfo dir, string output_dir)
        {
            string cloud_dir_name = GetCloudPath(dir.FullName);
            string secure_dir_name = GetSecureName(cloud_dir_name);

            XmlDocument doc = new XmlDocument();
            //xml declaration is recommended, but not mandatory
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);
            
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo file in fis)
            {
                // todo write file to output
                string full_name = cloud_dir_name + file.Name; //??
                full_name = full_name.TrimStart('/');

                byte[] data = File.ReadAllBytes(file.FullName);
                //todo, write file to disk
                string secure_name = GetSecureName(file.Name);
                byte[] secure_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);
                File.WriteAllBytes(output_dir + "\\" + secure_name, secure_data);

                // create a file node
                XmlNode file_node = doc.CreateElement(string.Empty, "file", string.Empty);
                //APPEND name
                XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                name_node.InnerText = full_name;
                file_node.AppendChild(name_node);

                //APPEND signature
                byte[] sha256 = Utility.CryptoFunctions.SHA256(data);
                XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
                signature_node.InnerText = Convert.ToBase64String(sha256);
                file_node.AppendChild(signature_node);

                //APPEND dates
                DateTime dt = DateTime.Now;

                XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
                created_node.InnerText = dt.ToFileTime().ToString();
                file_node.AppendChild(created_node);

                XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
                modified_node.InnerText = dt.ToFileTime().ToString();
                file_node.AppendChild(modified_node);

                root.AppendChild(file_node);
            }

            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo sub_dir in dis)
            {
                string sub_dir_name = GetCloudPath(sub_dir.FullName);
                
                // create a file node
                XmlNode dir_node = doc.CreateElement(string.Empty, "directory", string.Empty);
                //APPEND name
                XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                name_node.InnerText = sub_dir_name;
                dir_node.AppendChild(name_node);

                //APPEND dates
                DateTime dt = DateTime.Now;

                XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
                created_node.InnerText = dt.ToFileTime().ToString();
                dir_node.AppendChild(created_node);

                XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
                modified_node.InnerText = dt.ToFileTime().ToString();
                dir_node.AppendChild(modified_node);

                root.AppendChild(dir_node);

                InitializeLocal(sub_dir, output_dir);
            }
            
            doc.Save(secure_dir_name);

            // write directory file to output
            byte[] xml_file_data = File.ReadAllBytes(secure_dir_name);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);
            File.WriteAllBytes(output_dir + "\\" + secure_dir_name, encrypted_xml_file_data);
        }

        /// <summary>
        /// create an empty file, fill with random or zeroed data
        /// </summary>
        /// <param name="name">name of file</param>
        /// <param name="len">length of ile</param>
        /// <param name="random">if true fills with random otherwise zeros</param>
        public void CreateEmptyFile(string name, int len, bool random = true)
        {
            string secure_name = GetSecureName(name);
            cloud.CreateEmpty(secure_name, len, random);
        }

        /// <summary>
        /// utility removes UTF-8 byte order mark from xml string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string RemoveByteOrderMarkUTF8(string xml)
        {
            string byte_order_mark = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml.StartsWith(byte_order_mark))
                xml = xml.Remove(0, byte_order_mark.Length);
            return xml;
        }

        /// <summary>
        /// get files in encrypted directory (ROOT)
        /// </summary>
        /// <returns>files as xml</returns>
        public XmlNodeList GetFiles()
        {
            return GetFiles(ROOT_FILE_NAME);
        }

        /// <summary>
        /// get file in encrypted directory
        /// </summary>
        /// <param name="dir_name">directory name</param>
        /// <returns>files as xml</returns>
        public XmlNodeList  GetFiles(string dir_name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // decrypt
            string secure_name = GetSecureName(dir_name);
            byte[] encrypted_data = cloud.Read(secure_name, 0, 0);
            byte[] data = CryptoFunctions.DecryptAES(key, encrypted_data, iv);

            XmlDocument doc = new XmlDocument();
            // encode to string & remove: byte order mark (BOM)
            string xml = UTF8Encoding.UTF8.GetString(data);
            xml = RemoveByteOrderMarkUTF8(xml);
            // save doc
            doc.LoadXml(xml);

            XmlNodeList nodes = doc.SelectNodes("/root/file | /root/directory");

            return nodes;
        }

        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private string GetSecureName(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return CryptoFunctions.FromBytesToHex(hash);
        }
        
        /// <summary>
        /// create / or append to file (creates one file from all parts)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encrypted_data"></param>
        private void CreateAppendFragment(string name, byte[] data)
        {
            // write all fragments
            long LEN = data.Length;
            byte[] data_chunk = null; 

            long idx = 0; // reset index
            while ((idx + CHUNK_SIZE) <= LEN)
            {
                data_chunk = new byte[CHUNK_SIZE];
                Array.Copy(data, idx, data_chunk, 0, CHUNK_SIZE);

                cloud.CreateAppend(name, data_chunk);
                idx += CHUNK_SIZE;
            }

            long left_over = (LEN - idx);
            if (left_over > 0)
            {
                data_chunk = new byte[left_over];
                Array.Copy(data, idx, data_chunk, 0, left_over);

                cloud.CreateAppend(name, data_chunk);
            }
        }
        
        public void CreateDirectory(string name)
        {
            // trim root slash from name
            name = name.TrimStart('/');

            // assert key is loaded
            if (KeyLoaded != true)
                throw new CloudException("Key not loaded.");

            // assert name/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            if (cloud.Exists(name) != false)
                throw new CloudException("Error creating file. (file exists");

            // assert name/directory exists
            string sub_dir_name = CloudPath.GetDirectory(name);
            string secure_sub_dir_name = GetSecureName(sub_dir_name);
            if (cloud.Exists(secure_sub_dir_name) != true)
                throw new CloudException("Directory does not exists.");

            XmlDocument doc = GetDirectoryDocument(secure_sub_dir_name);
            XmlNode root = doc.DocumentElement;
            XmlNode file = doc.CreateElement(string.Empty, "directory", string.Empty);

            // APPEND name
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = name;
            file.AppendChild(name_node);

            // APPEND dates
            DateTime dt = DateTime.Now;
            // created
            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(created_node);
            // modified
            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(modified_node);

            root.AppendChild(file);
            // BKP hack, could just load string but there is an issue here
            doc.Save("tmp.xml");

            // delete old dir file
            cloud.Delete(secure_sub_dir_name);
            // create new ROOT/dir
            // BKP hack, could just load string but there is an issue here
            byte[] data = File.ReadAllBytes("tmp.xml");
            byte[] encrypted_data = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_sub_dir_name, encrypted_data);

            // create new directory file 
            CreateDirectoryXml(name);
        }

        public void CreateFile(string name, byte[] data)
        {
            // trim root slash from name
            name = name.TrimStart('/');

            // assert key is loaded
            if (KeyLoaded != true)
                throw new CloudException("Key not loaded.");

            // assert name/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            if (cloud.Exists(name) != false)
                throw new CloudException("Error creating file. (file exists");

            // assert name/directory exists
            string sub_dir_name = CloudPath.GetDirectory(name);
            string secure_sub_dir_name = GetSecureName(sub_dir_name);
            if (cloud.Exists(secure_sub_dir_name) != true)
                throw new CloudException("Directory does not exists.");
            
            // encrypt file to upload
            byte[] encrypted_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            // upload file
            CreateAppendFragment(secure_name, encrypted_data);
           
            // create/append xml file node to xml directory node
            XmlDocument doc = GetDirectoryDocument(secure_sub_dir_name);
            XmlNode root = doc.DocumentElement;
            XmlNode file = doc.CreateElement(string.Empty, "file", string.Empty);

            // APPEND name
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = name;
            file.AppendChild(name_node);

            // APPEND signature
            byte[] sha256 = Utility.CryptoFunctions.SHA256(data);
            XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
            signature_node.InnerText = Convert.ToBase64String(sha256);
            file.AppendChild(signature_node);

            // APPEND dates
            DateTime dt = DateTime.Now;
            // created
            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(created_node);
            // modified
            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(modified_node);

            root.AppendChild(file);
            // BKP hack, could just load string but there is an issue here
            doc.Save("tmp.xml");

            // delete old dir file
            cloud.Delete(secure_sub_dir_name);
            // create new ROOT/dir
            // BKP hack, could just load string but there is an issue here
            data = File.ReadAllBytes("tmp.xml");
            encrypted_data = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_sub_dir_name, encrypted_data);
        }

        //combine - CreateFile & CreateDirectory
        public void CreateName(string name, byte[] data)
        {
        }

        public XmlDocument GetDirectoryDocument(string name)
        {
            byte[] encrypted_data = cloud.Read(name, 0, 0);
            byte[] data = CryptoFunctions.DecryptAES(key, encrypted_data, iv);
            
            string xml = Encoding.UTF8.GetString(data);
            xml = RemoveByteOrderMarkUTF8(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        public void Delete(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // delete file 
            string secure_name = GetSecureName(name);

            //only delete id directory is empty
            if (name.EndsWith("/"))
            {
                // is this dir empty
                XmlDocument me_doc = GetDirectoryDocument(secure_name);
                XmlNodeList me_node = me_doc.SelectNodes("/doc/file | /doc/directory");
                if (me_node.Count > 0)
                {
                    throw new CloudException("Directory is not empty.");
                }
            }
            
            cloud.Delete(secure_name);
            // decrypt xml dir file
            string dir_name = CloudPath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);

            string xpath = string.Format("/root/file[name = \"{0}\"] | /root/directory[name = \"{0}\"]", name);
            XmlNode n = doc.SelectSingleNode(xpath);
            n.ParentNode.RemoveChild(n);

            // delete old dir file
            cloud.Delete(secure_dir_name);

            // create new dir file
            string xml = doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] crypt = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_dir_name, crypt);
        }

        /// <summary>
        /// read / download / copy file to client directory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public byte[] Read(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            string secure_name = GetSecureName(name);
            int LEN = (int)cloud.GetLength(secure_name);

            byte[] data_chunk = null;
            byte[] encrypted_data = new byte[LEN];

            int offset = 0; 
            while ((offset + CHUNK_SIZE) <= LEN)
            {
                data_chunk = cloud.Read(secure_name, offset, CHUNK_SIZE);
                Array.Copy(data_chunk, 0, encrypted_data, offset, CHUNK_SIZE);
                offset += CHUNK_SIZE;
            }

            int left_over = (LEN - offset);
            if (left_over > 0)
            {
                data_chunk = cloud.Read(secure_name, offset, left_over);
                Array.Copy(data_chunk, 0, encrypted_data, offset, left_over);
            }

            return CryptoFunctions.DecryptAES(key, encrypted_data, iv); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public long GetLength(string name)
        {
            string secure_name = GetSecureName(name);
            return cloud.GetLength(secure_name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return cloud.GetCount();
        }
    }
}
