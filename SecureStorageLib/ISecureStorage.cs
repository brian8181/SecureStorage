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
        /// <summary>
        /// Store
        /// </summary>
        IStorage Store { get; }

        /// <summary>
        /// Cryptography
        /// </summary>
        ICryptography Cryptography { get; }

        /// <summary>
        /// CurrentDirectory
        /// </summary>
        string CurrentDirectory { get; set; }

        /// <summary>
        /// Initialize
        /// </summary>
        void Initialize();

        /// <summary>
        /// CreateEmptyFile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="len"></param>
        /// <param name="random"></param>
        void CreateEmptyFile(string name, int len, bool random = true);

        /// <summary>
        /// CreateFile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        void CreateFile(string name, byte[] data);  

        /// <summary>
        /// CreateDirectory
        /// </summary>
        /// <param name="name"></param>
        void CreateDirectory(string name);

        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="name"></param>
        void DeleteFile(string name);

        /// <summary>
        /// DeleteDirectory
        /// </summary>
        /// <param name="name"></param>
        void DeleteDirectory(string name);

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        byte[] Read(string name); // read file

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="src_name"></param>
        /// <param name="dst_name"></param>
        void Move(string src_name, string dst_name); // move file

        /// <summary>
        /// Copy: copy file
        /// </summary>
        /// <param name="src_name"></param>
        /// <param name="dst_name"></param>
        void Copy(string src_name, string dst_name);

        /// <summary>
        /// file that describes a directory
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //string GetDescriptor(string name);
        
        /// <summary>
        /// GetNames: get files &  directory nodes
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        XmlNodeList GetNames(string names); 

        /// <summary>
        /// GetFiles: get file nodes
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        XmlNodeList GetFiles(string names);


         //Maybe something like this
        //public class FileAttributes
        //{
        //}
             
        //FileAttributes[] GetFilesAttributes(string names);

        /// <summary>
        /// GetDirectories: get directory nodes
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        XmlNodeList GetDirectories(string names); 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //XmlDocument GetDirectoryDocument(string name);
    }   
}
 