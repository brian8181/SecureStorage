using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;

namespace ClientWindowsFormsApplication
{
    public class CloudUtility
    {
        // BKP const?
        private readonly int KEY_SIZE = 32;
        private readonly int IV_SIZE = 16;

        private byte[] key = null;
        private byte[] iv = null;

        public byte[] Key
        {
            get { return key; }
            set { key = value; }
        }
        public byte[] IV
        {
            get { return iv; }
            set { iv = value; }
        }

        public CloudUtility(int key_size, int iv_size)
        {
            KEY_SIZE = key_size;
            IV_SIZE = iv_size;
        }

        /// <summary>
        /// loads key from file into variables (key & iv)
        /// </summary>
        /// <param name="name"></param>
        public void LoadKey(string path)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            iv = new byte[IV_SIZE];
            key = new byte[KEY_SIZE];

            Array.Copy(key_iv, key, 32);
            Array.Copy(key_iv, 32, iv, 0, 16);
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

        //BKP copied from cloud!
        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private string GetSecureName(string name)
        {
            //if (KeyLoaded != true)
            //    throw new Exception("key not loaded");

            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return CryptoFunctions.FromBytesToHex(hash);
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

        /// <summary>
        /// (ONLY USED BY InitializeLocal) gets name / name used for cloud, aka removes local root & adjust slashes
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
    }
}
