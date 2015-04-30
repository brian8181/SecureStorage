using System;
using System.ServiceModel;

namespace CyptoCloud_WS
{
    [ServiceContract]
    public interface IData
    {
        // efficient n len file creation, create name with random, or zero data
        [OperationContract]
        void CreateEmpty(string name, int len, bool random = false);

        [Obsolete("Use AppendData to create files")]
        [OperationContract]
        void Create(string name, byte[] data);

        //void Create(string name, byte[] data, bool dir);

        [OperationContract]
        byte[] Read(string name);
     
        [OperationContract]
        void Delete(string name);
          

        // move file
        [OperationContract]
        void Move(string src, string dst);

        //  copy file
        [OperationContract]
        void Copy(string src, string dst);

        // ramdom access functions
        
        [OperationContract]
        void WriteData(string name, int offset, byte[] data);

        //// allow to break data into samller pieces to transfer
        [OperationContract]
        void AppendData(string name, byte[] data);

        [OperationContract]
        byte[] ReadData(string name, int offset, int lenght);

        // some other thoughts on function signatures
        //long ReadData(string name, int offset, int lenght, out byte[] buffer);
        //byte[] ReadData(string name, int offset, int lenght, out long read);

        //// src & dst can be same
        //[OperationContract]
        //void MoveData(string src, int src_idx, string dst, int dst_idx, int len);

        //// src & dst can be same
        //[OperationContract]
        //void CopyData(string src, int src_idx, string dst, int dst_idx, int len);


        //long GetUsage();
        [OperationContract]
        long GetLength(string name);

        // get cout all files
        [OperationContract]
        int GetCount();
        
        //return subset of file list
        [OperationContract]
        string[] GetNames(int idx, int len);

        ////return subset of file list
        [OperationContract]
        string[] GetAllNames();
               
        // get list of all files in dir, used for 
        [OperationContract]
        void DeleteAll();

        [OperationContract]
        byte[] SHA256(string name);
    }
}
