using System;
using System.IO;

namespace SecureStorageLib
{
    /// <summary>
    /// IStorage: high level interface to storage
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="name">name of object</param>
        /// <param name="data"></param>
        /// <param name="mode"></param>
        void Create(string name, byte[] data, FileMode mode = FileMode.Create);

        /// <summary>
        /// CreateEmpty
        /// </summary>
        /// <param name="name"></param>
        /// <param name="len"></param>
        /// <param name="random"></param>
        void CreateEmpty(string name, int len, bool random = false);

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="name">name of object</param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        byte[] Read(string name, int offset, int len);

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="src_name">name of object</param>
        /// <param name="dst_name"></param>
        void Copy(string src_name, string dst_name);


        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="src_name">name of source object</param>
        /// <param name="dst_name">name of destination object</param>
        void Move(string src_name, string dst_name);

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="name">name of object</param>
        void Delete(string name);

        /// <summary>
        /// GetLength
        /// </summary>
        /// <param name="name">name of object</param>
        /// <returns></returns>
        int GetLength(string name);

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="name">name of object</param>
        /// <returns></returns>
        bool Exists(string name);
        
        /// <summary>
        /// DeleteAll
        /// </summary>
        void DeleteAll();

    }
}
