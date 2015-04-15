using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Utility.IO;
using System.Security.Cryptography;
using System.Xml;

namespace AesEncryptDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string proc_dir = Utility.Globals.ProcessDirectory();

            string input = "c:\\tmp\\infiles";
            string output = "c:\\tmp\\outfiles";

            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            string[] files = Utility.IO.DirectoryExt.GetFiles(input, "*");

            foreach (string file in files)
            {
                byte[] data = File.ReadAllBytes(file);

                byte[] crypt = Utility.CryptoFunctions.EncryptAES(aes.Key, data, aes.IV);
                string path = "c:\\tmp\\outfiles\\" + Path.GetFileName(file);
                File.WriteAllBytes(path, crypt);


                // maybe hash file before encryption?
                HMACSHA256 hmacsha256 = new HMACSHA256(aes.Key);
                byte[] hash = hmacsha256.ComputeHash(data);
                
                File.WriteAllBytes("c:\\tmp\\outfiles\\_dir\\" + Path.GetFileName(file) + ".sig", hash);

            }

            //files = Utility.IO.DirectoryExt.GetFiles(output, "*.jpg");
            //foreach (string file in files)
            //{
            //    byte[] data = File.ReadAllBytes(file);

            //    byte[] crypt = Utility.CryptoFunctions.DecryptAES(aes.Key, data, aes.IV);
            //    string path = "c:\\tmp\\infiles\\" + Path.GetFileName(file);
            //    File.WriteAllBytes(path, crypt);
            //}


            FileInfo[] fis = Utility.IO.DirectoryExt.GetFileInfos(output, "*.jpg");
            foreach (FileInfo file in fis)
            {
                byte[] data = File.ReadAllBytes(file.FullName);

                byte[] crypt = Utility.CryptoFunctions.DecryptAES(aes.Key, data, aes.IV);
                string path = "c:\\tmp\\infiles\\" + Path.GetFileName(file.FullName);
                File.WriteAllBytes(path, crypt);
            }

            //CreateFakeXML();
            CreateXML();

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

                
                XmlNode index = file_node.AppendChild(doc.CreateElement(string.Empty, "index", string.Empty));
                index.InnerText = idx.ToString();
                el1.AppendChild(file_node);
                
                XmlNode length = file_node.AppendChild(doc.CreateElement(string.Empty, "length", string.Empty));
                length.InnerText = file.Length.ToString();
                el1.AppendChild(file_node);

                idx += file.Length;


                //XmlNode sig = file_node.AppendChild(doc.CreateElement(string.Empty, "signature", string.Empty));
                //sig.InnerText = 
                //el1.AppendChild(file_node);

            }

            doc.Save("c:\\tmp\\outfiles\\_dir\\root.xml");
        }

    }
}
