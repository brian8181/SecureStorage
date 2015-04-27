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
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement decel = doc.DocumentElement;
            XmlNode node = doc.InsertBefore(xmlDeclaration, decel);

            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

                       
            XmlElement file = doc.CreateElement(string.Empty, "file", string.Empty);
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            file.AppendChild(name_node);

            root.AppendChild(file);
            doc.Save(@"c:\tmp\testing.xml");


            // open and add file
            doc = new XmlDocument();
            doc.Load(@"c:\tmp\testing.xml"); 
            root = doc.DocumentElement;
            
            XmlNode new_node = doc.CreateElement(string.Empty, "file", string.Empty);
            name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            new_node.AppendChild(name_node);


            root.AppendChild(new_node);

            doc.Save(@"c:\tmp\testing.xml");
 
        }
    }
}
