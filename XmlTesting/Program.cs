using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            //XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //XmlElement decel = doc.DocumentElement;
            //XmlNode node = doc.InsertBefore(xmlDeclaration, decel);

            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);
            root.InnerText = "test";

            doc.Save(@"c:\tmp\testing.xml");
        }
    }
}
