using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClientConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceReference.IService svr = new ServiceReference.ServiceClient();
            string s = svr.GetData(5);
            Console.WriteLine(s);
            Console.WriteLine(svr.Hello());
            Console.Read();
        }

        public byte[] GetDirectory(string path)
        {
            //byte[] bts = File.ReadAllBytes(path);
            //byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            //File.WriteAllBytes("tmp.xml", data);

            ////string xml = ASCIIEncoding.ASCII.GetString(data);

            //XmlDocument doc = new XmlDocument();
            ////doc.LoadXml(xml);
            //doc.Load("tmp.xml");
            //XmlNodeList nodes = doc.SelectNodes("/root/file[name = \"Chrysanthemum.jpg\"]");


            //foreach (XmlNode n in nodes)
            //{
            //    XmlNode name_node = n.FirstChild;
            //    string name = name_node.InnerText;


            //    string xml = n.OuterXml;
            //    XmlNodeList l = n.SelectNodes("file/signature");

            //    //XmlNode sig_node = n.FirstChild.NextSibling;
            //    //string sig = sig_node.InnerText;
            //    string sig = n["signature"].InnerText;

            //}

            //return bts;

            return null;
        }
    }
}
