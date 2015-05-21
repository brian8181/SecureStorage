using System;
using System.IO;

namespace SecureStorageLib
{

    /// <summary>
    /// high level interface to storage
    /// </summary>
    public interface IStorage
    {
        void Create(string name, byte[] data, FileMode mode = FileMode.Create);
        void CreateEmpty(string name, int len, bool random = false);
        byte[] Read(string name, int offset, int len);
        void Delete(string name);
        int GetLength(string name);
        bool Exists(string name);
        void DeleteAll();
    }
}
