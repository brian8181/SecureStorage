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
        private const string ROOT_FILE_NAME = "ROOT.xml";
        private const int CHUNK_SIZE = 1000;
                               
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

        public void Initialize()
        {
            // create a file system on server
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            //in_path = in_path.TrimEnd('\\');
            //out_path = out_path.TrimEnd('\\');

            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);

            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);
            doc.Save(ROOT_FILE_NAME);

            string secure_named_root = GetSecureName(ROOT_FILE_NAME);
            byte[] xml_file_data = File.ReadAllBytes(ROOT_FILE_NAME);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);
            cloud.CreateAppend(secure_named_root, encrypted_xml_file_data);
        }


        /// <summary>
        /// initialize/create a directory based on input directory
        /// </summary>
        /// <param name="in_path"></param>
        /// <param name="out_path"></param>
        public void InitializeLocal(string in_path, string out_path)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            in_path = in_path.TrimEnd('\\');
            out_path = out_path.TrimEnd('\\');

            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);

            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            DirectoryInfo di = new DirectoryInfo(in_path);
            InitializeLocal(di, doc, root, out_path);

            doc.Save(ROOT_FILE_NAME);

            string secure_named_root = GetSecureName(ROOT_FILE_NAME);
            byte[] xml_file_data = File.ReadAllBytes(ROOT_FILE_NAME);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);
            File.WriteAllBytes(out_path + "\\" + secure_named_root, encrypted_xml_file_data);
        }

        private void InitializeLocal(DirectoryInfo dir, XmlDocument doc, XmlNode root, string out_path)
        {
            out_path = out_path.TrimEnd('\\');

            // get all files
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo file in fis)
            {
                byte[] data = File.ReadAllBytes(file.FullName);
                //todo, write file to disk
                string secure_name = GetSecureName(file.Name);
                byte[] secure_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);
                File.WriteAllBytes(out_path + "\\" + secure_name, secure_data);

                // create a file node
                XmlNode file_node = doc.CreateElement(string.Empty, "file", string.Empty);

                //APPEND name
                XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                name_node.InnerText = file.Name;
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
            
            //// get all dirs
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                string dir_name = Path.GetFileName(di.FullName) + "/";
                string secure_dir_name = GetSecureName(dir_name);

                // create a file node
                XmlNode dir_node = doc.CreateElement(string.Empty, "directory", string.Empty);

                //APPEND name
                XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                name_node.InnerText = dir_name;
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

                //todo.. create dir xml
                XmlDocument sub_dir_doc = new XmlDocument();
                //xml declaration is recommended, but not mandatory
                XmlDeclaration sub_decel = sub_dir_doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                sub_dir_doc.InsertBefore(sub_decel, sub_dir_doc.DocumentElement);
                XmlNode new_root_node = sub_dir_doc.AppendChild(sub_dir_doc.CreateElement(string.Empty, "root", string.Empty));
                // save tmp
                sub_dir_doc.Save(secure_dir_name);

                ////encrypt & write file
                byte[] xml_sub_dir_data = File.ReadAllBytes(secure_dir_name);
                byte[] encrypted_xml_sub_dir_data = CryptoFunctions.EncryptAES(key, xml_sub_dir_data, iv);
                File.WriteAllBytes(out_path + "\\" + secure_dir_name, encrypted_xml_sub_dir_data);

                //// add to xmldoc
                InitializeLocal(di, sub_dir_doc, new_root_node, out_path);

                // save all
                sub_dir_doc.Save(secure_dir_name);
            }
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
        public XmlNodeList GetFiles(string dir_name)
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
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml.StartsWith(_byteOrderMarkUtf8))
                xml = xml.Remove(0, _byteOrderMarkUtf8.Length);
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
            byte[] path_bytes = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(path_bytes);
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
                
        public void Create(string name, byte[] data)
        {
            if (KeyLoaded != true)
            {
                StdMsgBox.Error("Key not loaded.");
                return;
            }

            // the hash is insecure dictionay attack is possible, use HMAC 
            string secure_name = GetSecureName(name);
            if (cloud.Exists(name) != false)
            {
                StdMsgBox.Error("Error creating file. (file exists)");
                return;
            }

            // encrypt file to upload
            byte[] encrypted_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            // upload file
            CreateAppendFragment(secure_name, encrypted_data);
           
            // APPEND FILE NODE to ROOT
            // decrypt
            string secure_root_name = GetSecureName(ROOT_FILE_NAME);
            byte[] bts = cloud.Read(secure_root_name, 0, 0);
            data = CryptoFunctions.DecryptAES(key, bts, iv);
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
            doc.Save("tmp.xml");

            // delete old dir file
            cloud.Delete(secure_root_name);
            // create new ROOT/dir
            data = File.ReadAllBytes("tmp.xml");
            encrypted_data = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.CreateAppend(secure_root_name, encrypted_data);
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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] Read(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            string secure_name = GetSecureName(name);
            byte[] encrypted_data = cloud.Read(secure_name, 0, 0);
            return CryptoFunctions.DecryptAES(key, encrypted_data, iv);
        }

        /// <summary>
        /// 
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
