using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CryptoCloudLib;

namespace CyptoCloud_WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService : ITest//, ICloudFile
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string[] GetDirectory(string path);
        //bool ValidateSig(byte[] file, byte[] sig);
        //void CreateDirectory(string path);
        //// create file
        //void Create(string path, byte[] data);
        //// get file contents
        //byte[] Read(string path);
        //// use copy & delete to update a file
        ////void Update(string path, byte[] data);
        //// removes file a loction
        //void Delete(string path);
        //// create copy of file to another location
        //void Copy(string src, string dst);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
