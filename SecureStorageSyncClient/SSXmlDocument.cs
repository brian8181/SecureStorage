using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecureStorageSyncClient
{
    /// <summary>
    /// SecureStorage Xml Docuemnt
    /// </summary>
    public class SSXmlDocument
    {
        /// <summary>
        /// internal function, abstracts creating directories at specified name
        /// </summary>
        /// <param name="name">name of directory</param>
        /// <returns></returns>
        public void Create(string name)
        {
            // create xml file for root directory
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            // create empty doc 
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            //string secure_named_dir = GetSecureName(name);
            doc.Save(name);

            // encrypt xml file
            //byte[] xml_file_data = File.ReadAllBytes(secure_named_dir);
            //byte[] encrypted_xml_file_data = crypto.Encrypt(xml_file_data);

            // send/Store it using secure name 
            //if (Store.Exists(secure_named_dir) != true)
            //    Store.Create(secure_named_dir, encrypted_xml_file_data, FileMode.Append);
        }

        /// <summary>
        /// AppendFileXml: helper function, appends a file to xml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <param name="hash"></param>
        private void AppendFile(XmlDocument doc, string name, string hash)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = doc.CreateElement(string.Empty, "file", string.Empty);
            AppendCommonNodes(doc, node, name);

            // signature
            XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
            signature_node.InnerText = hash;
            node.AppendChild(signature_node);

            root.AppendChild(node);
        }

        /// <summary>
        /// AppendDirectory: helper function, appends a directory to xml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        private void AppendDirectory(XmlDocument doc, string name)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = doc.CreateElement(string.Empty, "directory", string.Empty); ;
            AppendCommonNodes(doc, node, name);

            root.AppendChild(node);
        }

        /// <summary>
        /// AppendCommonNodesXml: helper function, appends nodes common to both files & directories
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="node"></param>
        /// <param name="name"></param>
        private void AppendCommonNodes(XmlDocument doc, XmlNode node, string name)
        {
            // name
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = name;
            node.AppendChild(name_node);
            // date time 
            DateTime dt = DateTime.Now;
            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            node.AppendChild(created_node);
            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            node.AppendChild(modified_node);
        }

        /// <summary>
        /// ReadFileSignatureXml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        //private string ReadFileSignatureXml(XmlDocument doc, string name)
        //{
        //    return SecureStorageUtility.ReadFileSignatureXml(doc, name);
        //}

        /// <summary>
        /// RemoveNameXml: helper function, removes a object by name from xml document
        /// </summary>
        /// <param name="doc">the xml document</param>
        /// <param name="name">the object name</param>
        private void RemoveNameXml(XmlDocument doc, string name)
        {
            string xpath = string.Format("/root/file[name = \"{0}\"] | /root/directory[name = \"{0}\"]", name);
            XmlNode n = doc.SelectSingleNode(xpath);
            n.ParentNode.RemoveChild(n);
        }

        ///// <summary>
        ///// GetNames: get files & directories in encrypted directory
        ///// </summary>
        ///// <param name="dir_name">directory name</param>
        ///// <returns>files as xml</returns>
        //public XmlNodeList GetNames(string dir_name)
        //{
        //    string secure_name = GetSecureName(dir_name);
        //    XmlDocument doc = GetDirectoryDocument(secure_name);
        //    XmlNodeList nodes = doc.SelectNodes("/root/file | /root/directory");
        //    return nodes;
        //}

        ///// <summary>
        ///// GetFiles: get files in encrypted directory
        ///// </summary>
        ///// <param name="dir_name">directory name</param>
        ///// <returns>files as xml</returns>
        //public XmlNodeList GetFiles(string dir_name)
        //{
        //    string secure_name = GetSecureName(dir_name);
        //    XmlDocument doc = GetDirectoryDocument(secure_name);
        //    XmlNodeList nodes = doc.SelectNodes("/root/file");
        //    return nodes;
        //}

        ///// <summary>
        ///// GetDirectories
        ///// </summary>
        ///// <param name="dir_name"></param>
        ///// <returns></returns>
        //public XmlNodeList GetDirectories(string dir_name)
        //{
        //    string secure_name = GetSecureName(dir_name);
        //    XmlDocument doc = GetDirectoryDocument(secure_name);
        //    XmlNodeList nodes = doc.SelectNodes("/root/directory");
        //    return nodes;
        //}


        /// <summary>
        /// RemoveByteOrderMarkUTF8: utility removes UTF-8 byte order mark from xml string
        /// </summary>
        /// <returns>string, without byte order mark</returns>
        private string RemoveByteOrderMarkUTF8(string xml)
        {
            bool bom_present = true;
            byte[] utf8_bom = new byte[3] { 0xEF, 0xBB, 0xBF };
            byte[] bts = Encoding.UTF8.GetBytes(xml);

            // check for BOM
            for (int i = 0; i < 3; ++i)
            {
                if (utf8_bom[i] != bts[i])
                {
                    bom_present = false;
                    break;
                }
            }

            // remove BOM
            int len = bts.Length;
            byte[] no_bom_bts = new byte[len - 3];
            if (bom_present)
            {
                Array.Copy(bts, 3, no_bom_bts, 0, len - 3);
                xml = Encoding.UTF8.GetString(no_bom_bts);
            }

            return xml;
        }
    }
}
