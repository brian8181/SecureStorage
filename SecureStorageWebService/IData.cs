using System;
using System.ServiceModel;

namespace SecureStorageWebService
{
    [ServiceContract]
    public interface IData
    {
        // efficient n len file creation, create name with random, or zero data
        [OperationContract]
        void CreateEmpty(string name, int len, bool random = false);

        // allow to break data into smaller pieces to transfer
        [OperationContract]
        void CreateAppend(string name, byte[] data);

        // allow to break data into smaller pieces to transfer
        [OperationContract]
        void CreateReplace(string name, byte[] data);

        [OperationContract]
        byte[] Read(string name, int offset, int lenght);

        /// <summary>
        /// copy object of src_name to dst_name
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

        //get len of file
        [OperationContract]
        long GetLength(string name);

        // get count all files
        [OperationContract]
        int GetCount();

        /// <summary>
        /// check for exsistance of object by name
        /// </summary>
        /// <param name="name">the name of object</param>
        /// <returns>true if exists false othewise</returns>
        [OperationContract]
        bool Exists(string name);

        //return subset of file list
        [OperationContract]
        string[] GetNames(int idx, int len);

        //return subset of file list
        [OperationContract]
        string[] GetAllNames();
               
        // get list of all files in dir, used for 
        [OperationContract]
        void DeleteAll();

        //get sha256 of file data
        [OperationContract]
        byte[] SHA256(string name);
    }
}
