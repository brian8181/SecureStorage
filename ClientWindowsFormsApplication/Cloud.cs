using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;
using Utility.IO;

namespace ClientWindowsFormsApplication
{
    public class Cloud
    {
        private ServiceReference.IService cloud = new ServiceReference.ServiceClient();
        XmlDocument root = new XmlDocument();
        private byte[] key = null;
        private byte[] iv  = null;


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

        public void LoadKey(string path)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            key = new byte[32];
            iv = new byte[16];

            Array.Copy(key_iv, key, 32);
            Array.Copy(key_iv, 32, iv, 0, 16);
        }


        /// <summary>
        /// set up a directorey based off local dir, include all from above
        /// </summary>
        public void InitializeFileSystem(string path)
        {
            string work_dir = Properties.Settings.Default.working_dir;
            LoadKey(Properties.Settings.Default.key_path + "\\key");
            
            XmlDocument doc = new XmlDocument();    
            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement decel = doc.DocumentElement;
            XmlNode node = doc.InsertBefore(xmlDeclaration, decel);
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            FileInfo[] in_files = DirectoryExt.GetFileInfos(path, "*");
            foreach (FileInfo file in in_files)
            {
                XmlElement file_node = doc.CreateElement(string.Empty, "file", string.Empty);
                XmlNode name = file_node.AppendChild(doc.CreateElement(string.Empty, "name", string.Empty));
                name.InnerText = Path.GetFileName(file.FullName);
                root.AppendChild(file_node);

                byte[] data = File.ReadAllBytes(file.FullName);
                byte[] crypt = Utility.CryptoFunctions.EncryptAES(key, data, iv);

                byte[] md5 = CryptoFunctions.MD5(file.Name);

                // need a hash as file name
                string hash_name = CryptoFunctions.FromBytesToHex(md5);
                string file_path = Properties.Settings.Default.working_dir + "\\" + hash_name;
                File.WriteAllBytes(file_path, crypt);

                //ADD SALT

                HMACSHA256 hmacsha256 = new HMACSHA256(key);
                byte[] hash = hmacsha256.ComputeHash(data);
                XmlNode signature = file_node.AppendChild(doc.CreateElement(string.Empty, "signature", string.Empty));
                signature.InnerText = Convert.ToBase64String(hash);
                root.AppendChild(file_node);

                DateTime dt = DateTime.Now;

                XmlNode created = file_node.AppendChild(doc.CreateElement(string.Empty, "created", string.Empty));
                created.InnerText = dt.ToFileTime().ToString();
                root.AppendChild(file_node);

                XmlNode modified = file_node.AppendChild(doc.CreateElement(string.Empty, "modified", string.Empty));
                modified.InnerText = dt.ToFileTimeUtc().ToString();
                root.AppendChild(file_node);
            }

            // save tmp location
            doc.Save("c:\\tmp\\root3.xml");

            //  get file & encrypt
            byte[] data2 = File.ReadAllBytes("c:\\tmp\\root.xml");
            string test = ASCIIEncoding.ASCII.GetString(data2);
            byte[] crypt2 = Utility.CryptoFunctions.EncryptAES(key, data2, iv);
            // save encryptede contents as ROOT 
            File.WriteAllBytes("c:\\tmp\\fs\\ROOT", crypt2);
        }

        public XmlNodeList GetDirectories()
        {
            // decrypt
            byte[] bts = cloud.Read("ROOT");
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            root = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            root.Load("tmp.xml");
            XmlNodeList nodes = root.SelectNodes("/root/file");

            return nodes;
        }

        public void Create(string name, byte[] data)
        {
            byte[] md5 = CryptoFunctions.MD5(name);
            // need a hash as file name
            string hash_name = CryptoFunctions.FromBytesToHex(md5);
            cloud.Create(hash_name, data);

            // add node to ROOT

            // decrypt
            byte[] bts = cloud.Read("ROOT");
            data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument root = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            root.Load("tmp.xml");

            XmlNode node = root.FirstChild;
            string s = node.Name;
            
                

        }



        public void Delete(string name)
        {
            // delete file 
            byte[] md5 = CryptoFunctions.MD5(name);
            string hash_name = CryptoFunctions.FromBytesToHex(md5);
            cloud.Delete(hash_name);
            
            // decrypt
            byte[] bts = cloud.Read("ROOT");
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument root = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            root.Load("tmp.xml");

            //XmlNode n = FindFileNode(name);
            XmlNode n = root.SelectSingleNode("/root/file[name = \"" + name + "\"]");
            n.ParentNode.RemoveChild(n);
            root.Save("tmp.xml");

            // delete old dir file
            cloud.Delete("ROOT");
            // create new ROOT/dir
            data = File.ReadAllBytes("tmp.xml");
            byte[] crypt = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.Create("ROOT", crypt);
        }

        //public XmlNode CreateFileNode()
        //{
        //    XmlDocument doc = new XmlDocument();    
        //    doc.Load("tmp.xml");
        //    XmlNode root = doc.NextSibling;

        //    XmlElement file_node = doc.CreateElement(string.Empty, "file", string.Empty);
        //    XmlNode name = file_node.AppendChild(doc.CreateElement(string.Empty, "name", string.Empty));
        //    name.InnerText = Path.GetFileName(file.FullName);
        //    root.AppendChild(file_node);

        //    byte[] data = File.ReadAllBytes(file.FullName);
        //    byte[] crypt = Utility.CryptoFunctions.EncryptAES(key, data, iv);

        //    byte[] md5 = CryptoFunctions.MD5(file.Name);

        //    // need a hash as file name
        //    string hash_name = CryptoFunctions.FromBytesToHex(md5);
        //    string file_path = Properties.Settings.Default.working_dir + "\\" + hash_name;
        //    File.WriteAllBytes(file_path, crypt);

        //    //ADD SALT

        //    HMACSHA256 hmacsha256 = new HMACSHA256(key);
        //    byte[] hash = hmacsha256.ComputeHash(data);
        //    XmlNode signature = file_node.AppendChild(doc.CreateElement(string.Empty, "signature", string.Empty));
        //    signature.InnerText = Convert.ToBase64String(hash);
        //    root.AppendChild(file_node);

        //    DateTime dt = DateTime.Now;

        //    XmlNode created = file_node.AppendChild(doc.CreateElement(string.Empty, "created", string.Empty));
        //    created.InnerText = dt.ToFileTime().ToString();
        //    root.AppendChild(file_node);

        //    XmlNode modified = file_node.AppendChild(doc.CreateElement(string.Empty, "modified", string.Empty));
        //    modified.InnerText = dt.ToFileTimeUtc().ToString();
        //    root.AppendChild(file_node);
        //}


        /// <summary>
        /// get all file nodes
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNodeList GetFileNodes(out XmlDocument doc)
        {
            // decrypt
            byte[] bts = cloud.Read("ROOT");
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");
            return doc.SelectNodes("/root/file");
        }

        /// <summary>
        /// find a file node by name
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNode FindFileNode(string name)
        {
            // decrypt
            byte[] bts = cloud.Read("ROOT");
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            XmlDocument doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");
            return doc.SelectSingleNode("/root/file[name = \"" + name + "\"]");
        }
    }
}
