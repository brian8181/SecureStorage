using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Utility.IO;
using System.Security.Cryptography;
using System.Xml;
using Utility;

namespace AesEncryptDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            //string proc_dir = Utility.Globals.ProcessDirectory();

            //string input = "c:\\tmp\\infiles";
            //string output = "c:\\tmp\\outfiles";

            //AesManaged aes = new AesManaged();
            //aes.GenerateKey();
            //aes.GenerateIV();

            //string[] files = Utility.IO.DirectoryExt.GetFiles(input, "*");

            //foreach (string file in files)
            //{
            //    byte[] data = File.ReadAllBytes(file);

            //    byte[] crypt = Utility.CryptoFunctions.EncryptAES(aes.Key, data, aes.IV);
            //    string path = "c:\\tmp\\outfiles\\" + Path.GetFileName(file);
            //    File.WriteAllBytes(path, crypt);


            //    // maybe hash file before encryption?
            //    HMACSHA256 hmacsha256 = new HMACSHA256(aes.Key);
            //    byte[] hash = hmacsha256.ComputeHash(data);
                
            //    File.WriteAllBytes("c:\\tmp\\outfiles\\_dir\\" + Path.GetFileName(file) + ".sig", hash);

            //}

            //files = Utility.IO.DirectoryExt.GetFiles(output, "*.jpg");
            //foreach (string file in files)
            //{
            //    byte[] data = File.ReadAllBytes(file);

            //    byte[] crypt = Utility.CryptoFunctions.DecryptAES(aes.Key, data, aes.IV);
            //    string path = "c:\\tmp\\infiles\\" + Path.GetFileName(file);
            //    File.WriteAllBytes(path, crypt);
            //}

            // just go through all outout & test decrypt it
            //FileInfo[] fis = Utility.IO.DirectoryExt.GetFileInfos(output, "*.jpg");
            //foreach (FileInfo file in fis)
            //{
            //    byte[] data = File.ReadAllBytes(file.FullName);

            //    byte[] crypt = Utility.CryptoFunctions.DecryptAES(aes.Key, data, aes.IV);
            //    string path = "c:\\tmp\\infiles\\" + Path.GetFileName(file.FullName);
            //    File.WriteAllBytes(path, crypt);
            //}

            //CreateFakeXML();
            //CreateXML();

            //CreateKey("c:\\tmp\\aes_key\\key");
            LoadKey("c:\\tmp\\aes_key\\key");
            //InitializeFileSystem("c:\\tmp\\infiles");
            GetDir("c:\\tmp\\fs\\ROOT");
            XmlNode node = GetFile("c:\\tmp\\fs\\ROOT", "Chrysanthemum.jpg");
           
            

        }

        public static void CreateXML()
        {
            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            XmlNode node = doc.InsertBefore(xmlDeclaration, root);
            XmlElement el1 = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(el1);
            

            //string[] files = Utility.IO.DirectoryExt.GetFiles("c:\\tmp\\infiles", "*");

            //foreach (string file in files)
            //{
            //    XmlElement file_node = doc.CreateElement(string.Empty, "file", string.Empty);
            //    XmlNode name = file_node.AppendChild( doc.CreateElement(string.Empty, "name", string.Empty) );
            //    name.InnerText = Path.GetFileName(file);
            //    el1.AppendChild(file_node);
            //}

            long idx = 0;

            FileInfo[] fis = Utility.IO.DirectoryExt.GetFileInfos("c:\\tmp\\infiles", "*");
            foreach (FileInfo file in fis)
            {

                XmlElement file_node = doc.CreateElement(string.Empty, "file", string.Empty);
                XmlNode name = file_node.AppendChild(doc.CreateElement(string.Empty, "name", string.Empty));
                name.InnerText = Path.GetFileName(file.FullName);
                el1.AppendChild(file_node);

                
                //XmlNode index = file_node.AppendChild(doc.CreateElement(string.Empty, "index", string.Empty));
                //index.InnerText = idx.ToString();
                //el1.AppendChild(file_node);
                
                //XmlNode length = file_node.AppendChild(doc.CreateElement(string.Empty, "length", string.Empty));
                //length.InnerText = file.Length.ToString();
                //el1.AppendChild(file_node);

                //idx += file.Length;


                //XmlNode sig = file_node.AppendChild(doc.CreateElement(string.Empty, "signature", string.Empty));
                //sig.InnerText = 
                //el1.AppendChild(file_node);

            }

            doc.Save("c:\\tmp\\outfiles\\_dir\\root.xml");
        }

        public static void CreateKey(string path)
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] key = new byte[aes.Key.Length + aes.IV.Length];

            Array.Copy(aes.Key, key, aes.Key.Length);
            Array.Copy(aes.IV, 0, key, aes.Key.Length, aes.IV.Length);

            File.WriteAllBytes(path, key);
        }

        static byte[] key = null;
        static byte[] iv = null;

        public static void LoadKey(string path)
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
        public static void InitializeFileSystem(string path)
        {
            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement decel = doc.DocumentElement;
            XmlNode node = doc.InsertBefore(xmlDeclaration, decel);
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            //AesManaged aes = new AesManaged();
            //aes.GenerateKey();
            //aes.GenerateIV();
            
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
                string file_path = "c:\\tmp\\fs\\" + hash_name;
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
            doc.Save("c:\\tmp\\root.xml");
            
            //  get file & encrypt
            byte[] data2 = File.ReadAllBytes("c:\\tmp\\root.xml");
            string test = ASCIIEncoding.ASCII.GetString(data2);
            byte[] crypt2 = Utility.CryptoFunctions.EncryptAES(key, data2, iv);
            // save encryptede contents as ROOT 
            File.WriteAllBytes("c:\\tmp\\fs\\ROOT", crypt2);


            //testing
            string s = GetHashSaltFileName("ROOT");
            bool v = Utility.PasswordHash.ValidatePassword("ROOT", s);
        }

        public static string GetHashSaltFileName(string name)
        {
            return Utility.PasswordHash.CreateHash(name);
        }

       // static ICloudFile cf = new CloudFile("c:\\tmp\\fs\\ROOT");

        /// <summary>
        /// return dir file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] GetDir(string path)
        {
            byte[] bts = File.ReadAllBytes(path);
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            //string xml = ASCIIEncoding.ASCII.GetString(data);

            XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml);
            doc.Load("tmp.xml");
            XmlNodeList nodes = doc.SelectNodes("/root/file[name = \"Chrysanthemum.jpg\"]");


            foreach (XmlNode n in nodes)
            {
                XmlNode name_node = n.FirstChild;
                string name = name_node.InnerText;


                string xml = n.OuterXml;
                XmlNodeList l = n.SelectNodes("file/signature");

                //XmlNode sig_node = n.FirstChild.NextSibling;
                //string sig = sig_node.InnerText;
                string sig  = n["signature"].InnerText;
                
            }



            return null;
        }

        public class FileDesc
        {
            string name;
            string signature;
            byte[] data;
            DateTime created;
            DateTime modified;
        }


        public static FileDesc[] GetDirectory(string path, bool recursive = false)
        {
            return null;
        }

        /// <summary>
        /// get & decrypt file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XmlNode GetFile(string path, string name)
        {
            byte[] bts = File.ReadAllBytes(path);
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            File.WriteAllBytes("tmp.xml", data);

            //string xml = ASCIIEncoding.ASCII.GetString(data);

            XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml);
            doc.Load("tmp.xml");
            XmlNodeList nodes = doc.SelectNodes("/root/file[name = \"" + name + "\"]");
                        

            return nodes[0];
        }

        //THE BIG THERE IO FUNCTIONS, can do anything with just these 3 
        public static void Create(string path, byte[] data, string sigature)
        {
            //
        }

        public static void Create(FileDesc file_desc)
        {

        }

        public static byte[] Read(string path)
        {
            return null;
        }

        public static void Delete(string path)
        {
        }

        //AGGREGATE FUNCTIONS, use big 3
        public static void Copy(string src, string dst)
        {
            //read file
            //create new copy
        }

        public static void Move(string src, string dst)
        {
        }

        public static void Update(string path, byte[] data)
        {
            //copy file
            //delete old file
            //crate file
        }



    }
}
