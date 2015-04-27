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

        public void Create(string path, byte[] data)
        {
            byte[] crypt = Utility.CryptoFunctions.EncryptAES(key, data, iv);
            byte[] md5 = CryptoFunctions.MD5(path);
            // need a hash as file path

            // the hash is insecure dictionay attack is possible, use HMAC 
            string hash_name = CryptoFunctions.FromBytesToHex(md5);
            cloud.Create(hash_name, data); 

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
