using System;
using System.ServiceModel;

namespace CyptoCloud_WS
{
    public interface IHighLevel
    {
        [OperationContract]
        byte[] GetDirectory();
    }
}
