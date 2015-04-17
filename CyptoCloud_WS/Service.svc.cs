using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using Utility;

namespace CyptoCloud_WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region IService Members


        public string Hello()
        {
            return "Hello World!";
        }

        #endregion

        #region IService Members


        public byte[] GetDirectory(string path)
        {
            byte[] bts = File.ReadAllBytes(path);
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

            return bts;
        }

        #endregion



        #region IService Members


        string[] IService.GetDirectory(string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
