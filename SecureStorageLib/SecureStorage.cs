using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using SecureStorageLib;

namespace SecureStorageLib
{
    /// <summary>
    /// manages a secure storage Store
    /// </summary>
    public class SecureStorage : ISecureStorage
    {
        private IStorage store = null;
        private ICryptography crypto = null;
        private string current_directory = "/";
        private readonly int FRAGMENT_SIZE = 0;
        private const string ROOT_FILE_NAME = "/";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="Store">an ISecureStorage implementation</param>
        /// <param name="key">an encryption key</param>
        /// <param name="iv">an encryption iv</param>
        /// <param name="fragment_size">max message size before fragmentation occurs</param>
        public SecureStorage(IStorage store, ICryptography crypto, int fragment_size = 0)
        {
            this.store = store;
            this.crypto = crypto;
            FRAGMENT_SIZE = fragment_size;
            CurrentDirectory = "/"; 
        }

        /// <summary>
        /// CurrentDirectory 
        /// </summary>
        public string CurrentDirectory 
        { 
            get{ return current_directory; } 
            set{ current_directory = value; }
        }


        /// <summary>
        /// Store
        /// </summary>
        public IStorage Store
        {
            get { return store; }
        }

        /// <summary>
        /// Cryptography
        /// </summary>
        public ICryptography Cryptography 
        { 
            get{ return crypto; }
        }

        /// <summary>
        /// inititalize/create an empty root attempt to send / Store
        /// </summary>
        /// <returns>returns true if successful, otherwise false</returns>
        public void Initialize()
        {
            Store.DeleteAll();
            CreateDescriptorFile(ROOT_FILE_NAME);
        }

        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private string GetSecureName(string name)
        {
            byte[] hash = SecureStorageUtility.HMACSHA256(name, crypto.Key);
            return SecureStorageUtility.FromBytesToHex(hash);
        }

        /// <summary>
        /// gets directory as an XmlDocument
        /// </summary>
        /// <param name="name">name of directory</param>
        /// <returns>the name/doc as XmlDocument</returns>
        private XmlDocument GetDirectoryDocument(string name)
        {
            // posssible too big a file!, use this.Read()
            byte[] encrypted_data = Store.Read(name, 0, 0);

            byte[] data = crypto.Decrypt(encrypted_data);

            string xml = Encoding.UTF8.GetString(data);
            xml = RemoveByteOrderMarkUTF8(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        //BKP new, STILL USING "GetDirectoryDocument"
        //public string GetDescriptor(string name)
        //{
        //    byte[] encrypted_data = Store.Read(name, 0, 0);
        //    byte[] data = crypto.Decrypt(encrypted_data);

        //    string xml = Encoding.UTF8.GetString(data);
        //    xml = RemoveByteOrderMarkUTF8(xml);

        //    return xml;
        //}

        /// <summary>
        /// internal function, abstarcts Creating direcoties at specified name
        /// </summary>
        /// <param name="name">name of directory</param>
        /// <returns></returns>
        private void CreateDescriptorFile(string name)
        {
            // create xml file for root directory
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decel = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.InsertBefore(decel, doc.DocumentElement);
            // create empty doc 
            XmlElement root = doc.CreateElement(string.Empty, "root", string.Empty);
            doc.AppendChild(root);

            string secure_named_dir = GetSecureName(name);
            doc.Save(secure_named_dir);

            // encrypt xml file
            byte[] xml_file_data = File.ReadAllBytes(secure_named_dir);
            byte[] encrypted_xml_file_data = crypto.Encrypt(xml_file_data);

            // send/Store it using secure name
            if (Store.Exists(secure_named_dir) != true)
                Store.Create(secure_named_dir, encrypted_xml_file_data, FileMode.Append);
        }

        /// <summary>
        /// GetNames: get files in encrypted directory
        /// </summary>
        /// <param name="dir_name">directory name</param>
        /// <returns>files as xml</returns>
        public XmlNodeList GetNames(string dir_name)
        {
            // decrypt
            string secure_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_name);
            XmlNodeList nodes = doc.SelectNodes("/root/file | /root/directory");
            return nodes;
        }

        /// <summary>
        /// GetFiles: get files in encrypted directory
        /// </summary>
        /// <param name="dir_name">directory name</param>
        /// <returns>files as xml</returns>
        public XmlNodeList GetFiles(string dir_name)
        {
            // decrypt
            string secure_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_name);
            XmlNodeList nodes = doc.SelectNodes("/root/file");
            return nodes;
        }

        /// <summary>
        /// GetDirectories
        /// </summary>
        /// <param name="dir_name"></param>
        /// <returns></returns>
        public XmlNodeList GetDirectories(string dir_name)
        {
            // decrypt
            string secure_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_name);
            XmlNodeList nodes = doc.SelectNodes("/root/directory");
            return nodes;
        }
              

        /// <summary>
        /// RemoveByteOrderMarkUTF8: utility removes UTF-8 byte order mark from xml string
        /// </summary>
        /// <returns>string, without byte order mark</returns>
        private string RemoveByteOrderMarkUTF8(string xml)
        {
            string byte_order_mark = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml.StartsWith(byte_order_mark))
                xml = xml.Remove(0, byte_order_mark.Length);
            return xml;
        }

        /// <summary>
        /// create an empty file, fill with random or zeroed data
        /// </summary>
        /// <param name="name">name of file</param>
        /// <param name="len">length of ile</param>
        /// <param name="random">if true fills with random otherwise zeros</param>
        public void CreateEmptyFile(string name, int len, bool random = true)
        {
            string secure_name = GetSecureName(name);
            Store.CreateEmpty(secure_name, len, random);
        }
        
        private bool AssertNameExists(string name)
        {
            // assert name/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            return Store.Exists(name);
        }

        /// <summary>
        /// CreateFile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void CreateFile(string name, byte[] data)
        {
            // get all names
            string secure_name = GetSecureName(name);
            string dir_name = StoragePath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);

            // encrypt file to upload
            byte[] encrypted_data = crypto.Encrypt(data);
            // upload file
            CreateAppendFragment(secure_name, encrypted_data);

            // create/append xml file node to xml directory node
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);
            string hash = null;
            byte[] sha256 = SecureStorageUtility.SHA256(data);
            hash = Convert.ToBase64String(sha256);
            AppendFileXml(doc, name, hash);

            // delete old dir file
            Store.Delete(secure_dir_name);
            // create new dir file
            string xml = doc.OuterXml;
            data = Encoding.UTF8.GetBytes(xml);
            encrypted_data = crypto.Encrypt(data);
            Store.Create(secure_dir_name, encrypted_data, FileMode.Append);
        }

        /// <summary>
        /// CreateDirectory
        /// </summary>
        /// <param name="name"></param>
        public void CreateDirectory(string name)
        {
            // get all names
            string secure_name = GetSecureName(name);
            string dir_name = StoragePath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);

            // create descriptor file
            CreateDescriptorFile(name);

            // create/append xml file node to xml directory node
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);
            AppendDirectoryXml(doc, name);

            // delete old dir file
            Store.Delete(secure_dir_name);
            // create new dir file
            string xml = doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] encrypted_data = crypto.Encrypt(data);
            Store.Create(secure_dir_name, encrypted_data, FileMode.Append);
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="src_name"></param>
        /// <param name="dst_name"></param>
        public void Copy(string src_name, string dst_name)
        {
             // get all names
            string secure_src_name = GetSecureName(src_name);
            string dir_src_name = StoragePath.GetDirectory(src_name);
            string secure_dir_src_name = GetSecureName(dir_src_name);

            string secure_dst_name = GetSecureName(dst_name);
            string dir_dst_name = StoragePath.GetDirectory(dst_name);
            string secure_dir_dst_name = GetSecureName(dir_dst_name);

            // create copy on storage
            Store.Copy(secure_src_name, secure_dst_name);

            // create/append xml file node to xml directory node
            XmlDocument src_doc = GetDirectoryDocument(secure_dir_src_name);
            string hash = ReadFileSignatureXml(src_doc, src_name); // get hash from doc

            XmlDocument dst_doc = GetDirectoryDocument(secure_dir_dst_name);
            AppendFileXml(dst_doc, dst_name, hash);
            // delete old dir file
            Store.Delete(secure_dir_dst_name);
            // create new dir file
            string xml = dst_doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] encrypted_data = crypto.Encrypt(data);
            Store.Create(secure_dir_dst_name, encrypted_data, FileMode.Append);
        }


        /// <summary>
        /// Move
        /// </summary>
        /// <param name="src_name"></param>
        /// <param name="dst_name"></param>
        public void Move(string src_name, string dst_name)
        {
            // get all names
            string secure_src_name = GetSecureName(src_name);
            string dir_src_name = StoragePath.GetDirectory(src_name);
            string secure_dir_src_name = GetSecureName(dir_src_name);

            string secure_dst_name = GetSecureName(dst_name);
            string dir_dst_name = StoragePath.GetDirectory(dst_name);
            string secure_dir_dst_name = GetSecureName(dir_dst_name);

            // create copy on storage
            Store.Move(secure_src_name, secure_dst_name);

            // create/append xml file node to xml directory node
            XmlDocument src_doc = GetDirectoryDocument(secure_dir_src_name);
            string hash = ReadFileSignatureXml(src_doc, src_name); // get hash from doc

            RemoveNameXml(src_doc, src_name);
            // delete old dir file
            Store.Delete(secure_dir_src_name);
            // create new dir file
            string xml = src_doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] encrypted_data = crypto.Encrypt(data);
            Store.Create(secure_dir_src_name, encrypted_data, FileMode.Append);

            XmlDocument dst_doc = GetDirectoryDocument(secure_dir_dst_name);
            AppendFileXml(dst_doc, dst_name, hash);
            // delete old dir file
            Store.Delete(secure_dir_dst_name);
            // create new dir file
            xml = dst_doc.OuterXml;
            data = Encoding.UTF8.GetBytes(xml);
            encrypted_data = crypto.Encrypt(data);
            Store.Create(secure_dir_dst_name, encrypted_data, FileMode.Append);
        }

        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="name"></param>
        public void DeleteFile(string name)
        {
            string secure_name = GetSecureName(name);
            Store.Delete(secure_name);

            // decrypt xml dir file
            string dir_name = StoragePath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);
            RemoveNameXml(doc, name);

            // delete old dir file
            Store.Delete(secure_dir_name);
            // create new dir file
            string xml = doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] crypt = crypto.Encrypt(data);
            Store.Create(secure_dir_name, crypt, FileMode.Append);
        }

        /// <summary>
        /// DeleteDirectory
        /// </summary>
        /// <param name="name"></param>
        public void DeleteDirectory(string name)
        {
            // delete file 
            string secure_name = GetSecureName(name);

            // is this dir empty
            XmlDocument me_doc = GetDirectoryDocument(secure_name);
            XmlNodeList me_node = me_doc.SelectNodes("/root/file | /root/directory");
            if (me_node.Count > 0)
            {
                //BKP delete all
                throw new SecureStorageException("Directory is not empty.");
            }

            Store.Delete(secure_name);

            // decrypt xml dir file
            string dir_name = StoragePath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);
            RemoveNameXml(doc, name);

            // delete old dir file
            Store.Delete(secure_dir_name);
            // create new dir file
            string xml = doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] crypt = crypto.Encrypt(data);
            Store.Create(secure_dir_name, crypt, FileMode.Append);
        }

        /// <summary>
        /// Read: read / download / copy file to client directory
        /// </summary>
        /// <param name="name">name of object (file)</param>
        /// <returns></returns>
        public byte[] Read(string name)
        {
            string secure_name = GetSecureName(name);
            int LEN = (int)Store.GetLength(secure_name);
            byte[] encrypted_data = new byte[LEN];

            if (FRAGMENT_SIZE <= 0 || FRAGMENT_SIZE >= LEN)
            {
                // no fragmentation
                encrypted_data = Store.Read(secure_name, 0, LEN);
            }
            else
            {
                byte[] data_chunk = null;
                int offset = 0;
                while ((offset + FRAGMENT_SIZE) <= LEN)
                {
                    data_chunk = Store.Read(secure_name, offset, FRAGMENT_SIZE);
                    Array.Copy(data_chunk, 0, encrypted_data, offset, FRAGMENT_SIZE);
                    offset += FRAGMENT_SIZE;
                }

                int left_over = (LEN - offset);
                if (left_over > 0)
                {
                    data_chunk = Store.Read(secure_name, offset, left_over);
                    Array.Copy(data_chunk, 0, encrypted_data, offset, left_over);
                }
            }
            return crypto.Decrypt(encrypted_data); ;
        }
        
        /// <summary>
        /// ReadFileSignatureXml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ReadFileSignatureXml(XmlDocument doc, string name)
        {
            return SecureStorageUtility.ReadFileSignatureXml(doc, name);
        }

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

        /// <summary>
        /// AppendFileXml: helper function, appends a file to xml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <param name="hash"></param>
        private void AppendFileXml(XmlDocument doc, string name, string hash)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = doc.CreateElement(string.Empty, "file", string.Empty);
            AppendCommonNodesXml(doc, node, name);

            // signature
            XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
            signature_node.InnerText = hash;
            node.AppendChild(signature_node);

            root.AppendChild(node);
        }

        /// <summary>
        /// AppendDirectoryXml: helper function, appends a directory to xml
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        private void AppendDirectoryXml(XmlDocument doc, string name)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = doc.CreateElement(string.Empty, "directory", string.Empty); ;
            AppendCommonNodesXml(doc, node, name);

            root.AppendChild(node);
        }

        /// <summary>
        /// AppendCommonNodesXml: helper function, appends nodes common to both files & directories
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="node"></param>
        /// <param name="name"></param>
        private void AppendCommonNodesXml(XmlDocument doc, XmlNode node, string name)
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
        /// create / or append to file (creates one file from all parts)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encrypted_data"></param>
        private void CreateAppendFragment(string name, byte[] data)
        {
            // write all fragments
            int LEN = data.Length;
            if (FRAGMENT_SIZE <= 0 || FRAGMENT_SIZE >= LEN)
            {
                // no fragmentaion
                Store.Create(name, data, FileMode.Append);
            }
            else
            {
                byte[] data_chunk = null;
                int idx = 0; // reset index

                while ((idx + FRAGMENT_SIZE) <= LEN)
                {
                    data_chunk = new byte[FRAGMENT_SIZE];
                    Array.Copy(data, idx, data_chunk, 0, FRAGMENT_SIZE);

                    Store.Create(name, data_chunk, FileMode.Append);
                    idx += FRAGMENT_SIZE;
                }

                int left_over = (LEN - idx);
                if (left_over > 0)
                {
                    data_chunk = new byte[left_over];
                    Array.Copy(data, idx, data_chunk, 0, left_over);

                    Store.Create(name, data_chunk, FileMode.Append);
                }
            }
        }
    }
}