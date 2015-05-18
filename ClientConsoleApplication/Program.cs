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

            //10.1 * 2.099
            float f = 00.10f;
            byte[] buf = BitConverter.GetBytes(f);
            int n = 101;
            Console.WriteLine(f);
            List<char> list = new List<char>();

            foreach(byte b in buf)
            {
                for(int i = 0; i < 8; ++i)
                {
                    if ((b & (1 << i)) != 0)
                        list.Add('1');
                    else
                        list.Add('0');
                }
            }

            //for (int i = 0; i < 32; ++i)
            //{
            //    if (( ((int)f) & (1 << i)) != 0)
            //        list.Add('1');
            //    else
            //        list.Add('0');
            //}

            list.Reverse();
            foreach (char c in list)
                Console.Write(c);
            
            //ServiceReference.IService svr = new ServiceReference.ServiceClient();
            //string s = svr.GetData(5);
            //Console.WriteLine(s);
            //Console.WriteLine(svr.Hello());
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
