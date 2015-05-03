using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Utility;
using Utility.IO;

namespace ClientWindowsFormsApplication
{
    public class ClientCloud
    {
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
        /// create a key and write it to specified path
        /// </summary>
        /// <param name="path">path to write the key</param>
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
        /// <param name="path"></param>
        public void LoadKey(string path)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            key = new byte[32];
            iv = new byte[16];

            Array.Copy(key_iv, key, 32);
            Array.Copy(key_iv, 32, iv, 0, 16);
        }

        //TODO move to GUI, all path should be in cloud format in first place
        /// <summary>
        /// gets name / path used for cloud, aka removes local root & adjust slashes
        /// </summary>
        /// <param name="path">name / ptah to convert</param>
        /// <returns>cloud name / path</returns>
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
            return CreateDirectoryByPath(ROOT_FILE_NAME);
        }

        /// <summary>
        /// internal function, abstarcts Creating direcoties at specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CreateDirectoryByPath(string path)
        {
            // create a file system on serverw
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // create xml file for root directory
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            // create empty root 
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            string secure_named_root = GetSecureName(path);
            doc.Save(secure_named_root);

            // encrypt xml file
            byte[] xml_file_data = File.ReadAllBytes(secure_named_root);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);

            // send/store it using secure name
            if (cloud.Exists(secure_named_root) != true)
                return cloud.CreateAppend(secure_named_root, encrypted_xml_file_data);

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
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
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
        /// utility removes byte order mark from xml strings
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string RemoveByteOrderMark(byte[] xml)
        {
            string xml_string = UTF8Encoding.UTF8.GetString(xml);
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml_string.StartsWith(_byteOrderMarkUtf8))
                xml_string = xml_string.Remove(0, _byteOrderMarkUtf8.Length);
            return xml_string;
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
            string xml = RemoveByteOrderMark(data);
            // save doc
            doc.LoadXml(xml);

            XmlNodeList nodes = doc.SelectNodes("/root/file | /root/directory");

            return nodes;
        }

        private const int SALT_LEN = 32;
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
             // write chunks
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

            // assert path/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            if (cloud.Exists(name) != false)
                throw new CloudException("Error creating file. (file exists");

            // assert path/directory exists
            string sub_dir_name = CloudPath.GetDirectory(name);
            string secure_sub_dir_name = GetSecureName(sub_dir_name);
            if (cloud.Exists(secure_sub_dir_name) != true)
                throw new CloudException("Directory does not exists.");

            // decrypt, directory file, write to tmp
            byte[] bts = cloud.Read(secure_sub_dir_name, 0, 0);
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            // BKP hack, could just load string but there is an issue here
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");

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
            data = File.ReadAllBytes("tmp.xml");
            byte[] encrypted_data = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_sub_dir_name, encrypted_data);

            //..
            // craete new directory file 
            CreateDirectoryByPath(name);

        }

        public void CreateFile(string name, byte[] data)
        {
            // assert key is loaded
            if (KeyLoaded != true)
                throw new CloudException("Key not loaded.");

            // assert path/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            if (cloud.Exists(name) != false)
                throw new CloudException("Error creating file. (file exists");

            // assert path/directory exists
            string sub_dir_name = CloudPath.GetDirectory(name);
            string secure_sub_dir_name = GetSecureName(sub_dir_name);
            if (cloud.Exists(secure_sub_dir_name) != true)
                throw new CloudException("Directory does not exists.");
            



            // encrypt file to upload
            byte[] encrypted_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            // upload file
            CreateAppendFragment(secure_name, encrypted_data);
           
            // create/append xml file node to xml directory node
            
            // decrypt, directory file, write to tmp
            byte[] bts = cloud.Read(secure_sub_dir_name, 0, 0);
            data = CryptoFunctions.DecryptAES(key, bts, iv);
            // BKP hack, could just load string but there is an issue here
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");

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
            //steps
            //1. check dir exsist
            //2. add to dir_node to dir file
            //3. create this dir file (empty for now)
            //* data is null if it is a directory

            // if key not loaded
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // if already exists
            string cloud_name = name = GetCloudPath(name);
            if (cloud.Exists(cloud_name) != true)
                throw new Exception("Error creating file. (file exists)");
            
            // if sub dir not exists
            string sub_dir_name = CloudPath.GetDirectory("a/b/");
            if (cloud.Exists(sub_dir_name) != true)
                throw new Exception("Directory does not exists.");

            bool isDirectory = cloud_name.EndsWith("/");

            // add dir to dir

            // create dir file
            XmlDocument doc = new XmlDocument();
            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            XmlNode type_node = null;

            if (isDirectory != true)
            {
                type_node = doc.CreateElement(string.Empty, "file", string.Empty);
            }
            else
            {
                // create a file node
                type_node = doc.CreateElement(string.Empty, "directory", string.Empty);
            }

            //APPEND name
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = cloud_name;
            type_node.AppendChild(name_node);

            //APPEND dates
            DateTime dt = DateTime.Now;
            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            type_node.AppendChild(created_node);

            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            type_node.AppendChild(modified_node);

            root.AppendChild(type_node);
            doc.Save("tmp.xml");
            

            // delete old dir file
            //cloud.Delete(secure_root_name);
            //// create new ROOT/dir
            //data = File.ReadAllBytes("tmp.xml");
            //encrypted_data = CryptoFunctions.EncryptAES(key, data, iv);
            //cloud.CreateAppend(secure_root_name, encrypted_data);

            if (isDirectory == true)
            {
                // create this dir file
            }
        }

        public void Delete(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // delete file 
            string hash_name = GetSecureName(name);
            cloud.Delete(hash_name);
            
            // decrypt root dir file
            string secure_root_name = GetSecureName(ROOT_FILE_NAME);
            byte[] bts = cloud.Read(secure_root_name, 0, 0);
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument root = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            root.Load("tmp.xml");

            XmlNode n = root.SelectSingleNode("/root/file[name = \"" + name + "\"]");
            n.ParentNode.RemoveChild(n);
            root.Save("tmp.xml");

            // delete old dir file
            cloud.Delete(secure_root_name);
            // create new ROOT/dir
            data = File.ReadAllBytes("tmp.xml");
            byte[] crypt = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_root_name, crypt);
        }

        /// <summary>
        /// read / download / copy file to client directory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public byte[] Read(string name, bool chunk)
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
