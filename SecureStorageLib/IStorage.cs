using System;
using System.IO;

namespace SecureStorageLib
{
    
    /// <summary>
    /// high level interface to storage
    /// </summary>
    /// 
    public interface IStorage
    {
        void Lock(string name);
        void Unlock(string name);
        void Create(string name, byte[] data, FileMode mode = FileMode.Create);
        void CreateEmpty(string name, int len, bool random = false);
        void Copy(string src_name, string dst_name);
        void Move(string src_name, string dst_name);
        byte[] Read(string name, int offset, int len);
        void Delete(string name);
        int GetLength(string name);
        bool Exists(string name);
        void DeleteAll();
    }
}
