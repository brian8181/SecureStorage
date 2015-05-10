using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public class SecureStorageCryptoException : SecureStorageException
    {
        public SecureStorageCryptoException() 
            : base("Unknown SecureStroageCryptoException")
        {
        }
        public SecureStorageCryptoException(string msg)
            : base(msg)
        {
        }
    }
}
