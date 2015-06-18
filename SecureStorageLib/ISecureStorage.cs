using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    /// <summary>
    /// an interface for secure storage
    /// </summary>
    public interface ISecureStorage
    {
        //BKP todo .. define what ISecureStorage does?
        //     1. builds a secure storage system from 2 intefaces IStorgae, ICrypto

        IStorage Store { get; }
        //ICrypto Crypto { get; }
        string CurrentDirectory { get; set; }

        /// <summary>
        /// initialize root directory
        /// </summary>
        void Initialize();
        void CreateEmptyFile(string name, int len, bool random = true);
        void CreateFile(string name, byte[] data);  
        void CreateDirectory(string name);
        void DeleteFile(string name);
        void DeleteDirectory(string name);
        byte[] Read(string name); // read file
        //void Move(string name); // move file
        void Copy(string src_name, string dst_name); // copy file
        string GetDescriptor(string name);
        //string[] GetNames(string names); // get files / dirs
        //string[] GetFiles(string names); // get files
        //string[] GetDirectories(string names); // get dirs
        //long GetFileLength(string name);
    }   
}
 