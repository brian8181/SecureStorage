using System;
using System.ServiceModel;

namespace CyptoCloud_WS
{
    [ServiceContract]
    public interface ILowLevel
    {
        // efficient n lenght file creation, create name with random, or zero data
        [OperationContract]
        void CreateEmpty(string name, int len, bool random = false);

        [OperationContract]
        void Create(string name, byte[] data);

        //void Create(string name, byte[] data, bool dir);

        [OperationContract]
        byte[] Read(string name);
        
        [OperationContract]
        void Write(string name, int start, byte[] data);

        [OperationContract]
        void Delete(string name);

        // allow to break data into samller pieces to transfer
        [OperationContract]
        void Append(string name, byte[] data);

        // move file
        [OperationContract]
        void Move(string src, string dst);

        //  copy file
        [OperationContract]
        void Copy(string src, string dst);

        // ramdom access functions

        [OperationContract]
        byte[] ReadData(string name, int start, int lenght);

        // src & dst can be same
        [OperationContract]
        void MoveData(string src, int src_idx, string dst, int dst_idx, int len);

        // src & dst can be same
        [OperationContract]
        void CopyData(string src, int src_idx, string dst, int dst_idx, int len);


        //long GetUsage();

        // get cout all files
        //[OperationContract]
        //int GetCount();
        
        //return subset of file list
        //string[] GetFiles(int idx, int len);

        // get list of all files in dir, used for 
        //[OperationContract]
        //string[] GetAll();

        // get list of all files in dir, used for 
        //[OperationContract]
        //void DeleteAll();
    }
}
