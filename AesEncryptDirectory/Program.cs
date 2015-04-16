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
            InitializeFileSystem("c:\\tmp\\infiles");

        }

        //public static void CreateFakeXML()
        //{
        //    XmlDocument doc = new XmlDocument();

        //    //(1) the xml declaration is recommended, but not mandatory
        //    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //    XmlElement root = doc.DocumentElement;
        //    doc.InsertBefore(xmlDeclaration, root);

        //    //(2) string.Empty makes cleaner code
        //    XmlElement element1 = doc.CreateElement(string.Empty, "body", string.Empty);
        //    doc.AppendChild(element1);

        //    XmlElement element2 = doc.CreateElement(string.Empty, "level1", string.Empty);
        //    element1.AppendChild(element2);

        //    XmlElement element3 = doc.CreateElement(string.Empty, "level2", string.Empty);
        //    XmlText text1 = doc.CreateTextNode("text");
        //    element3.AppendChild(text1);
        //    element2.AppendChild(element3);

        //    XmlElement element4 = doc.CreateElement(string.Empty, "level2", string.Empty);
        //    XmlText text2 = doc.CreateTextNode("other text");
        //    element4.AppendChild(text2);
        //    element2.AppendChild(element4);

        //    doc.Save("c:\\tmp\\document.xml");
        //}

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

        byte[] key = null;
        byte[] iv = null;

        public static void LoadKey(string path)
        {
            byte[] key_iv = File.ReadAllBytes(path);
            //
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

            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();
            
            FileInfo[] in_files = DirectoryExt.GetFileInfos(path, "*");
            foreach (FileInfo file in in_files)
            {
                XmlElement file_node = doc.CreateElement(string.Empty, "file", string.Empty);
                XmlNode name = file_node.AppendChild(doc.CreateElement(string.Empty, "name", string.Empty));
                name.InnerText = Path.GetFileName(file.FullName);
                root.AppendChild(file_node);

                byte[] data = File.ReadAllBytes(file.FullName);
                byte[] crypt = Utility.CryptoFunctions.EncryptAES(aes.Key, data, aes.IV);

                byte[] md5 = CryptoFunctions.MD5(file.Name);
                
                // need a hash as file name
                string hash_name = CryptoFunctions.FromBytesToHex(md5);
                string file_path = "c:\\tmp\\fs\\" + hash_name;
                File.WriteAllBytes(file_path, crypt);

                //ADD SALT
                
                HMACSHA256 hmacsha256 = new HMACSHA256(aes.Key);
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
            byte[] crypt2 = Utility.CryptoFunctions.EncryptAES(aes.Key, data2, aes.IV);
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

        public static byte[] GetDir(string path)
        {
            return null;
        }

        public static byte[] GetFile(string name)
        {
            return null;
        }

    }
}
