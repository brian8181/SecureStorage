﻿using System;
using System.ServiceModel;

namespace CyptoCloud_WS
{
    [ServiceContract]
    public interface IData
    {
        // efficient n len file creation, create name with random, or zero data
        [OperationContract]
        void CreateEmpty(string name, int len, bool random = false);

        //[Obsolete("Use AppendData to create files")]
        //[OperationContract]
        //void Create(string name, byte[] data);

        //// allow to break data into samller pieces to transfer
        [OperationContract]
        bool CreateAppend(string name, byte[] data);

        [OperationContract]
        byte[] Read(string name, int offset, int lenght);
     
        [OperationContract]
        void Delete(string name);

        //[OperationContract]
        //void Write(string name, int offset, byte[] data);

        //get len of file
        [OperationContract]
        long GetLength(string name);

        // get count all files
        [OperationContract]
        int GetCount();

        [OperationContract]
        bool Exists(string name);

        //return subset of file list
        [OperationContract]
        string[] GetNames(int idx, int len);

        ////return subset of file list
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