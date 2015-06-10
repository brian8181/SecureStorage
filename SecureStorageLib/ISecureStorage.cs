using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    /// <summary>
    /// an interface for secure storeage
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
        //void CreateEmptyFile(string name, int len, bool random = true);

        //BKP new way
        void CreateFile(string name, byte[] data);
        void CreateDirectory(string name);

        //void CreateName(string name, byte[] data);

        //void DeleteName(string name);
        //void ReadName(string name);
        //void MoveName(string src_anme, string dst_name);
        //void CopyName(string src_anme, string dst_name);
        //string[] GetNames(string names);

        //long GetLength(string name); ?

        //string GetDirectoryInfo(string name);
        //BKP new name maybe?
        string GetDescriptor(string name);
    }   
}
 