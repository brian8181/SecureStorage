using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecureStorageLib
{
    /// <summary>
    /// an interface for secure storage
    /// </summary>
    public interface ISecureStorage
    {
        //IStorage Store { get; }
        //ICryptography Cryptography { get; }
        string CurrentDirectory { get; set; }

        void Initialize();
        void CreateEmptyFile(string name, int len, bool random = true);
        void CreateFile(string name, byte[] data);  
        void CreateDirectory(string name);
        void DeleteFile(string name);
        void DeleteDirectory(string name);
        byte[] Read(string name); // read file
        void Move(string src_name, string dst_name); // move file
        void Copy(string src_name, string dst_name); // copy file
        //string GetDescriptor(string name);
        //XmlDocument GetDirectoryDocument(string name);
        XmlNodeList GetNames(string names); // get files &  dirs
        XmlNodeList GetFiles(string names); // get files
        XmlNodeList GetDirectories(string names); // get dirs
    }   
}
 