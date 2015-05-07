using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using SecureStorageLib;

namespace SecureStorageLib
{
    /// <summary>
    /// manages a secure storage store
    /// </summary>
    public class SecureStorage 
    {
        private ISecureStorage store = null;
        private ICrypto crypto = null;
        private readonly int FRAGMENT_SIZE = 0xFFFF;
        private const string ROOT_FILE_NAME = "/";
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="store">an ISecureStorage implementation</param>
        /// <param name="key">an encryption key</param>
        /// <param name="iv">an encryption iv</param>
        /// <param name="fragment_size">max message size before fragmentation occurs</param>
        public SecureStorage(ISecureStorage store, ICrypto crypto, int fragment_size = 0xFFFF)
        {
            this.store = store;
            this.crypto = crypto;
            FRAGMENT_SIZE = fragment_size;
        }
                
        /// <summary>
        /// true if key has been loaded, otherwise false
        /// </summary>
        public bool KeyLoaded
        {
            get
            {
                if (crypto.Key == null || crypto.IV == null)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// inititalize/create an empty root attempt to send / store
        /// </summary>
        /// <returns>returns true if successful, otherwise false</returns>
        public void Initialize()
        {
            store.DeleteAll();
            CreateDirectoryXml(ROOT_FILE_NAME);
        }

        /// <summary>
        /// internal function, abstarcts Creating direcoties at specified name
        /// </summary>
        /// <param name="name">name of directory</param>
        /// <returns></returns>
        private void CreateDirectoryXml(string name)
        {
            // create a file system on server
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

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

            // send/store it using secure name
            if (store.Exists(secure_named_dir) != true)
                store.Create(secure_named_dir, encrypted_xml_file_data, FileMode.Append);
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
            store.CreateEmpty(secure_name, len, random);
        }

        /// <summary>
        /// get the length of an object
        /// </summary>
        /// <param name="name">the object name</param>
        /// <returns>length in bytes</returns>
        public long GetLength(string name)
        {
            string secure_name = GetSecureName(name);
            return store.GetLength(secure_name);
        }

        /// <summary>
        /// utility removes UTF-8 byte order mark from xml string
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
        /// get files in encrypted directory (ROOT)
        /// </summary>
        /// <returns>files as xml</returns>
        public XmlNodeList GetFiles()
        {
            return GetFiles(ROOT_FILE_NAME);
        }

        /// <summary>
        /// get file in encrypted directory
        /// </summary>
        /// <param name="dir_name">directory name</param>
        /// <returns>files as xml</returns>
        public XmlNodeList GetFiles(string dir_name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // decrypt
            string secure_name = GetSecureName(dir_name);
            byte[] encrypted_data = store.Read(secure_name, 0, 0);
            byte[] data = crypto.Decrypt(encrypted_data);

            XmlDocument doc = new XmlDocument();
            // encode to string & remove: byte order mark (BOM)
            string xml = UTF8Encoding.UTF8.GetString(data);
            xml = RemoveByteOrderMarkUTF8(xml);
            // save doc
            doc.LoadXml(xml);

            XmlNodeList nodes = doc.SelectNodes("/root/file | /root/directory");

            return nodes;
        }

        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private string GetSecureName(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            byte[] hash = crypto.HMACSHA256(name);
            return crypto.FromBytesToHex(hash);
        }
        
        /// <summary>
        /// create / or append to file (creates one file from all parts)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encrypted_data"></param>
        private void CreateAppendFragment(string name, byte[] data)
        {
            // write all fragments
            long LEN = data.Length;
            byte[] data_chunk = null; 

            long idx = 0; // reset index
            while ((idx + FRAGMENT_SIZE) <= LEN)
            {
                data_chunk = new byte[FRAGMENT_SIZE];
                Array.Copy(data, idx, data_chunk, 0, FRAGMENT_SIZE);

                store.Create(name, data_chunk, FileMode.Append);
                idx += FRAGMENT_SIZE;
            }

            long left_over = (LEN - idx);
            if (left_over > 0)
            {
                data_chunk = new byte[left_over];
                Array.Copy(data, idx, data_chunk, 0, left_over);

                store.Create(name, data_chunk, FileMode.Append);
            }
        }
        
        /// <summary>
        /// create a file or directory
        /// </summary>
        /// <param name="name">name of object</param>
        /// <param name="data">file data, is null if directory</param>
        public void CreateName(string name, byte[] data)
        {
            // trim root slash from name
            name = name.TrimStart('/');

            // assert key is loaded
            if (KeyLoaded != true)
                throw new SecureStorageException("Key not loaded.");

            // assert name/file/dir/name does not exists
            string secure_name = GetSecureName(name);
            if (store.Exists(name) != false)
                throw new SecureStorageException("Error creating file. (file exists");

            // assert name/directory exists
            string sub_dir_name = StoragePath.GetDirectory(name);
            string secure_sub_dir_name = GetSecureName(sub_dir_name);
            if (store.Exists(secure_sub_dir_name) != true)
                throw new SecureStorageException("Directory does not exists.");

            bool is_directory = name.EndsWith("/");

            byte[] encrypted_data = null;
            if (is_directory != true)
            {
                // encrypt file to upload
                encrypted_data = crypto.Encrypt(data);
                // upload file
                CreateAppendFragment(secure_name, encrypted_data);
            }

            // create/append xml file node to xml directory node
            XmlDocument doc = GetDirectoryDocument(secure_sub_dir_name);
            XmlNode root = doc.DocumentElement;

            XmlNode file = null;
            if (is_directory != true)
            {
                file = doc.CreateElement(string.Empty, "file", string.Empty);
            }
            else
            {
                file = doc.CreateElement(string.Empty, "directory", string.Empty);
            }

            // name
            XmlNode name_node = doc.CreateElement(string.Empty, "name", string.Empty);
            name_node.InnerText = name;
            file.AppendChild(name_node);

            if (is_directory != true)
            {
                // signature
                byte[] sha256 = crypto.SHA256(data);
                XmlNode signature_node = doc.CreateElement(string.Empty, "signature", string.Empty);
                signature_node.InnerText = Convert.ToBase64String(sha256);
                file.AppendChild(signature_node);
            }

            // dates
            DateTime dt = DateTime.Now;
            // created
            XmlNode created_node = doc.CreateElement(string.Empty, "created", string.Empty);
            created_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(created_node);
            // modified
            XmlNode modified_node = doc.CreateElement(string.Empty, "modified", string.Empty);
            modified_node.InnerText = dt.ToFileTime().ToString();
            file.AppendChild(modified_node);
            root.AppendChild(file);
           
            // delete old dir file
            store.Delete(secure_sub_dir_name);
          
            string xml = doc.OuterXml;
            data = Encoding.UTF8.GetBytes(xml);
            encrypted_data = crypto.Encrypt(data);
            store.Create(secure_sub_dir_name, encrypted_data, FileMode.Append);

            if (is_directory == true)
            {
                // create new directory file 
                CreateDirectoryXml(name);
            }
        }

        /// <summary>
        /// gets directory as an XmlDocument
        /// </summary>
        /// <param name="name">name of directory</param>
        /// <returns>the name/doc as XmlDocument</returns>
        private XmlDocument GetDirectoryDocument(string name)
        {
            byte[] encrypted_data = store.Read(name, 0, 0);
            byte[] data = crypto.Decrypt(encrypted_data);
            
            string xml = Encoding.UTF8.GetString(data);
            xml = RemoveByteOrderMarkUTF8(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        /// <summary>
        /// delete a named object
        /// </summary>
        /// <param name="name">name of the object</param>
        public void Delete(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            // delete file 
            string secure_name = GetSecureName(name);

            //only delete id directory is empty
            if (name.EndsWith("/"))
            {
                // is this dir empty
                XmlDocument me_doc = GetDirectoryDocument(secure_name);
                XmlNodeList me_node = me_doc.SelectNodes("/root/file | /root/directory");
                if (me_node.Count > 0)
                {
                    throw new SecureStorageException("Directory is not empty.");
                }
            }
            
            store.Delete(secure_name);
            // decrypt xml dir file
            string dir_name = StoragePath.GetDirectory(name);
            string secure_dir_name = GetSecureName(dir_name);
            XmlDocument doc = GetDirectoryDocument(secure_dir_name);

            string xpath = string.Format("/root/file[name = \"{0}\"] | /root/directory[name = \"{0}\"]", name);
            XmlNode n = doc.SelectSingleNode(xpath);
            n.ParentNode.RemoveChild(n);

            // delete old dir file
            store.Delete(secure_dir_name);

            // create new dir file
            string xml = doc.OuterXml;
            byte[] data = Encoding.UTF8.GetBytes(xml);
            byte[] crypt = crypto.Encrypt(data);
            store.Create(secure_dir_name, crypt, FileMode.Append);
        }

        /// <summary>
        /// read / download / copy file to client directory
        /// </summary>
        /// <param name="name">name of object (file)</param>
        /// <returns></returns>
        public byte[] Read(string name)
        {
            if (KeyLoaded != true)
                throw new Exception("key not loaded");

            bool is_directory = name.EndsWith("/");
            if(is_directory)
                throw new SecureStorageException("Error, is a directory.");

            string secure_name = GetSecureName(name);
            int LEN = (int)store.GetLength(secure_name);

            byte[] data_chunk = null;
            byte[] encrypted_data = new byte[LEN];

            int offset = 0; 
            while ((offset + FRAGMENT_SIZE) <= LEN)
            {
                data_chunk = store.Read(secure_name, offset, FRAGMENT_SIZE);
                Array.Copy(data_chunk, 0, encrypted_data, offset, FRAGMENT_SIZE);
                offset += FRAGMENT_SIZE;
            }

            int left_over = (LEN - offset);
            if (left_over > 0)
            {
                data_chunk = store.Read(secure_name, offset, left_over);
                Array.Copy(data_chunk, 0, encrypted_data, offset, left_over);
            }

            return crypto.Decrypt(encrypted_data); ;
        }
    }
}
