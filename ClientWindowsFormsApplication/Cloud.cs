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

        // todo initial svr directory directly
        public void Initialize(string in_path, string out_path)
        {
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

                //APPEND path
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

            doc.Save(@"ROOT.xml");

            byte[] xml_file_data = File.ReadAllBytes("ROOT.xml");
            byte[] encrypted_xml_file_data = CryptoFunctions.EncryptAES(key, xml_file_data, iv);
            File.WriteAllBytes(out_path + "\\" + "ROOT", encrypted_xml_file_data);
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

        private string GetSecureName(string name)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] path_bytes = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(path_bytes);
            return CryptoFunctions.FromBytesToHex(hash);
        }

        public void Create(string path, byte[] data)
        {
            byte[] crypt = Utility.CryptoFunctions.EncryptAES(key, data, iv);

            // the hash is insecure dictionay attack is possible, use HMAC 
            string secure_name = GetSecureName(path);
            cloud.Create(secure_name, data); 

            // add node to ROOT

            // decrypt
            byte[] bts = cloud.Read("ROOT");
            data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument doc = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            doc.Load("tmp.xml");

            XmlNode root = doc.DocumentElement;
            XmlNode file = doc.CreateElement(string.Empty, "file", string.Empty);

            //APPEND path
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = Path.GetFileName(path);
            file.AppendChild(name_node);

            //APPEND signature
            byte[] sha256 = Utility.CryptoFunctions.SHA256(data);
            XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
            signature_node.InnerText = Convert.ToBase64String(sha256);
            file.AppendChild(signature_node);

            //APPEND dates
            DateTime dt = DateTime.Now;

            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(created_node);

            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(modified_node);
            
            root.AppendChild(file);
            doc.Save("tmp.xml");


            // delete old dir file
            cloud.Delete("ROOT");
            // create new ROOT/dir
            data = File.ReadAllBytes("tmp.xml");
            crypt = CryptoFunctions.EncryptAES(key, data, iv);
            cloud.Create("ROOT", crypt);
                

        }
        
        public void Delete(string name)
        {
            // delete file 
            string hash_name = GetSecureName(name);
            cloud.Delete(hash_name);
            
            // decrypt
            byte[] bts = cloud.Read("ROOT");
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            // do not need a a member var
            XmlDocument root = new XmlDocument();
            // BKP hack, could just load string but there is an issue here
            root.Load("tmp.xml");

            //XmlNode n = FindFileNode(path);
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

        public void Read(string path)
        {
        }







        ///// <summary>
        ///// get all file nodes
        ///// </summary>
        ///// <param path="doc"></param>
        ///// <returns></returns>
        //public XmlNodeList GetFileNodes(out XmlDocument doc)
        //{
        //    // decrypt
        //    byte[] bts = cloud.Read("ROOT");
        //    byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
        //    File.WriteAllBytes("tmp.xml", data);

        //    doc = new XmlDocument();
        //    // BKP hack, could just load string but there is an issue here
        //    doc.Load("tmp.xml");
        //    return doc.SelectNodes("/root/file");
        //}

        ///// <summary>
        ///// find a file node by path
        ///// </summary>
        ///// <param path="nodes"></param>
        ///// <param path="path"></param>
        ///// <returns></returns>
        //public XmlNode FindFileNode(string name)
        //{
        //    // decrypt
        //    byte[] bts = cloud.Read("ROOT");
        //    byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
        //    File.WriteAllBytes("tmp.xml", data);

        //    XmlDocument doc = new XmlDocument();
        //    // BKP hack, could just load string but there is an issue here
        //    doc.Load("tmp.xml");
        //    return doc.SelectSingleNode("/root/file[path = \"" + name + "\"]");
        //}
    }
}
