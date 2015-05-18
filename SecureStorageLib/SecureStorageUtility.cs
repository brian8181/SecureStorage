using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace SecureStorageLib
{
    public static class SecureStorageUtility
    {
        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private static string GetSecureName(string name, byte[] key)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return FromBytesToHex(hash);
        }

        public static byte[] HMACSHA256(string name, byte[] key)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return hash;
        }

        public static byte[] SHA256(byte[] data)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] result = sha256.ComputeHash(data);
            return result;
        }

        /// <summary>
        /// generate key/salt or any random value of arbitrary length
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] GererateKey(int len)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[len];
                rng.GetBytes(key);
                return key;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] GenerateRandomBytes(int len)
        {
            return GererateKey(len);
        }

        public static string FromBytesToHex(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static void LoadKey(string path, int key_size, int iv_size, out byte[] key, out byte[] iv)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            iv = new byte[iv_size];
            key = new byte[key_size];

            Array.Copy(key_iv, key, key_size);
            Array.Copy(key_iv, key_size, iv, 0, iv_size);
        }

        /// <summary>
        /// create a key and write it to specified name
        /// </summary>
        /// <param name="name">name to write the key</param>
        public static void CreateKey(string path, int key_size, int iv_size)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                csp.GenerateKey();
                csp.GenerateIV();

                byte[] key = new byte[csp.Key.Length + csp.IV.Length];

                Array.Copy(csp.Key, key, csp.Key.Length);
                Array.Copy(csp.IV, 0, key, csp.Key.Length, csp.IV.Length);

                File.WriteAllBytes(path, key);
            }
        }

        /// <summary>
        /// initialize/create a directory based on input directory
        /// </summary>
        /// <param name="in_path"></param>
        /// <param name="out_path"></param>
        public static void InitializeLocalRoot(string input_dir, string output_dir, byte[] key, byte[] iv)
        {
            DirectoryInfo di= new DirectoryInfo(input_dir);
            InitializeLocal(di, output_dir, key, iv);
        }

        /// <summary>
        /// (ONLY USED BY InitializeLocal) gets name / name used for cloud, aka removes local root & adjust slashes
        /// </summary>
        /// <param name="name">name / name to convert</param>
        /// <returns>cloud name / name</returns>
        private static string GetCloudPath(string path)
        {
            path = path.Remove(0, 3); // remove drive letter
            path = path.Replace('\\', '/');
            path = path.TrimStart('/'); // may trim start ?
            return path + "/";
        }

        private static void InitializeLocal(DirectoryInfo dir, string output_dir, byte[] key, byte[] iv)
        {
            string cloud_dir_name = GetCloudPath(dir.FullName);
            string secure_dir_name = GetSecureName(cloud_dir_name, key);

            XmlDocument doc = new XmlDocument();
            //xml declaration is recommended, but not mandatory
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            AES aes = new AES(key, iv);

            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo file in fis)
            {
                // todo write file to output
                string full_name = cloud_dir_name + file.Name; //??
                full_name = full_name.TrimStart('/');

                byte[] data = File.ReadAllBytes(file.FullName);
                //todo, write file to disk
                string secure_name = GetSecureName(file.Name, key);
                byte[] secure_data = aes.Encrypt(data);
                File.WriteAllBytes(output_dir + "\\" + secure_name, secure_data);

                // create a file node
                XmlNode file_node = doc.CreateElement(string.Empty, "file", string.Empty);
                //APPEND name
                XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
                name_node.InnerText = full_name;
                file_node.AppendChild(name_node);

                //APPEND signature
                byte[] sha256 = SHA256(data);
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

                InitializeLocal(sub_dir, output_dir, key, iv);
            }

            doc.Save(secure_dir_name);

            // write directory file to output
            byte[] xml_file_data = File.ReadAllBytes(secure_dir_name);
            byte[] encrypted_xml_file_data = aes.Encrypt(xml_file_data);
            File.WriteAllBytes(output_dir + "\\" + secure_dir_name, encrypted_xml_file_data);
        }
    }
}
