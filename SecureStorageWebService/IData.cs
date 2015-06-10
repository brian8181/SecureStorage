using System;
using System.ServiceModel;

namespace SecureStorageWebService
{
    /// <summary>
    /// IData
    /// </summary>
    [ServiceContract]
    public interface IData
    {
        /// <summary>
        /// CreateEmpty: efficient n len file creation, create name with random, or zero data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="len"></param>
        /// <param name="random"></param>
        [OperationContract]
        void CreateEmpty(string name, int len, bool random = false);

        /// <summary>
        /// CreateAppend: allow to break data into smaller pieces to transfer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        [OperationContract]
        void CreateAppend(string name, byte[] data);

        /// <summary>
        /// CreateReplace: allow to break data into smaller pieces to transfer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        [OperationContract]
        void CreateReplace(string name, byte[] data);

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="name"></param>
        /// <param name="offset"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] Read(string name, int offset, int lenght);

        /// <summary>
        /// Copy: copy object of src_name to dst_name
        /// </summary>
        /// <param name="src_name">the source object</param>
        /// <param name="dst_name">the destination object</param>
        [OperationContract]
        void Copy(string src_name, string dst_name);
        
        /// <summary>
        /// deletes a object by name
        /// </summary>
        /// <param name="name">name of object</param>
        [OperationContract]
        void Delete(string name);

        /// <summary>
        /// gets length of a file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        long GetLength(string name);

        /// <summary>
        /// get count of all files & directories
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetCount();

        /// <summary>
        /// check for exsistance of object by name
        /// </summary>
        /// <param name="name">the name of object</param>
        /// <returns>true if exists false othewise</returns>
        [OperationContract]
        bool Exists(string name);

        /// <summary>
        /// get all file names
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [OperationContract]
        string[] GetNames(int idx, int len);

        /// <summary>
        /// gets a files and directories
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string[] GetAllNames();
               
        /// <summary>
        /// delete all files in working directory
        /// </summary>
        [OperationContract]
        void DeleteAll();

        /// <summary>
        /// gets sha256 of a files
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SHA256(string name);
    }
}
