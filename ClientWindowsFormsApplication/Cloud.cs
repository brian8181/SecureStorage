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
                
        public bool KeyLoaded
        {
            get
            {
                if(key == null || iv ==  null)
                    return false;
                return true;
            }
        }

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

        // todo initial svr directory directly
        public void Initialize(string in_path, string out_path)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            in_path = in_path.TrimEnd('\\');
            out_path = out_path.TrimEnd('\\');

            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement decel = doc.DocumentElement;
            XmlNode node = doc.InsertBefore(xmlDeclaration, decel);

            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            FileInfo[] in_files = DirectoryExt.GetFileInfos(in_path, "*");
            foreach (FileInfo file in in_files)
            {
                byte[] data = File.ReadAllBytes(file.FullName);
                //todo, write file to disk
                string secure_name = GetSecureName(file.Name);
                byte[] secure_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);
                File.WriteAllBytes(out_path + "\\" + secure_name, secure_data);
                                
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

                //APPEND salt / IV
                //XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                //name_node.InnerText = file.Name;
                //file_node.AppendChild(name_node);

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

            string sercure_named_root = GetSecureName(ROOT_FILE_NAME);

            doc.Save(ROOT_FILE_NAME);

            byte[] xml_file_data = File.ReadAllBytes(ROOT_FILE_NAME);
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);
            File.WriteAllBytes(out_path + "\\" + sercure_named_root, encrypted_xml_file_data);
       
        }
        
        public XmlNodeList GetDirectories()
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // decrypt
            string secure_name = GetSecureName(ROOT_FILE_NAME);
            byte[] encrypted_data = cloud.Read(secure_name);

            byte[] data = CryptoFunctions.DecryptAES(key, encrypted_data, iv);
            File.WriteAllBytes("tmp.xml", data);

            XmlDocument doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");
            XmlNodeList nodes = doc.SelectNodes("/root/file");

            return nodes;
        }

        //private string GetSecureName(string name, bool salted)
        //{
        //    if (KeyLoaded != true)
        //        throw new Exception("key not loaded");

        //    if (salted)
        //    {
        //        byte[] salt = CryptoFunctions.GenerateRandomBytes(4);

        //        HMACSHA256 hmacsha256 = new HMACSHA256(key);
        //        byte[] path_bytes = ASCIIEncoding.ASCII.GetBytes(name);
        //        byte[] secure_bytes = new byte[path_bytes.Length + 4];

        //        Array.Copy(salt, secure_bytes, 4);
        //        Array.Copy(path_bytes, 0, secure_bytes, 4, path_bytes.Length);

        //        byte[] hash = hmacsha256.ComputeHash(secure_bytes);

        //        return CryptoFunctions.FromBytesToHex(hash);
        //    }

        //    return null;

        //}

        private string GetSecureName(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] path_bytes = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(path_bytes);
            return CryptoFunctions.FromBytesToHex(hash);
        }

        private int CHUNK_SIZE = 1000;
      
        /// <summary>
        /// create / or append to file (creates one file from all parts)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encrypted_data"></param>
        private void AppendData(string name, byte[] data)
        {
             // write chunks
            long LEN = data.Length;
            byte[] data_chunk = null; 

            long idx = 0; // reset index
            while ((idx + CHUNK_SIZE) <= LEN)
            {
                data_chunk = new byte[CHUNK_SIZE];
                Array.Copy(data, idx, data_chunk, 0, CHUNK_SIZE);

                cloud.AppendData(name, data_chunk);
                idx += CHUNK_SIZE;
            }

            long left_over = (LEN - idx);
            if (left_over > 0)
            {
                data_chunk = new byte[left_over];
                Array.Copy(data, idx, data_chunk, 0, left_over);

                cloud.AppendData(name, data_chunk);
            }
        }
                
        public void Create(string name, byte[] data, bool chunk)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // the hash is insecure dictionay attack is possible, use HMAC 
            string secure_name = GetSecureName(name);
            byte[] encrypted_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            AppendData(secure_name, encrypted_data);
           
            // APPEND FILE NODE to ROOT
            // decrypt
            string secure_root_name = GetSecureName(ROOT_FILE_NAME);
            byte[] bts = cloud.Read(secure_root_name);
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
            cloud.Create(secure_root_name, encrypted_data);
        }

        public void Create(string name, byte[] data)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // the hash is insecure dictionay attack is possible, use HMAC 
            string secure_name = GetSecureName(name);
            byte[] encrypted_data = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            // split encrypted_data into chunks
            cloud.Create(secure_name, encrypted_data); 
            
            // APPEND FILE NODE to ROOT
            // decrypt
            string secure_root_name = GetSecureName(ROOT_FILE_NAME);
            byte[] bts = cloud.Read(secure_root_name);
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
            cloud.Create(secure_root_name, encrypted_data);
        }
        
        public void Delete(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // delete file 
            string hash_name = GetSecureName(name);
            cloud.Delete(hash_name);
            
            // decrypt
            string secure_root_name = GetSecureName(ROOT_FILE_NAME);
            byte[] bts = cloud.Read(secure_root_name);
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
            cloud.Create(secure_root_name, crypt);
        }

        public byte[] Read(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            string secure_name = GetSecureName(name);
            byte[] encrypted_data = cloud.Read(secure_name);
            return CryptoFunctions.DecryptAES(key, encrypted_data, iv);
        }

        public byte[] Read(string name, bool chunk)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            string secure_name = GetSecureName(name);
            int LEN = (int)cloud.GetLength(secure_name);

            //todo read encrypted_data in chunks
            // write chunks
            //long LEN = encrypted_data.Length;
            byte[] data_chunk = null;
            byte[] encrypted_data = new byte[LEN];

            int offset = 0; // reset index
            while ((offset + CHUNK_SIZE) <= LEN)
            {
                data_chunk = cloud.ReadData(secure_name, offset, CHUNK_SIZE);
                Array.Copy(data_chunk, 0, encrypted_data, offset, CHUNK_SIZE);
                offset += CHUNK_SIZE;
            }

            int left_over = (LEN - offset);
            if (left_over > 0)
            {
                data_chunk = cloud.ReadData(secure_name, offset, left_over);
                Array.Copy(data_chunk, 0, encrypted_data, offset, left_over);
            }

            return CryptoFunctions.DecryptAES(key, encrypted_data, iv); ;
        }

        public byte[] ReadData(string name)
        {
           
            return null;
        }

        public long GetLength(string name)
        {
            string secure_name = GetSecureName(name);
            return cloud.GetLength(secure_name);
        }

        public int GetCount()
        {
            return cloud.GetCount();
        }
    }
}
