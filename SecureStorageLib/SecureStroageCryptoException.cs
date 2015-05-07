using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public class SecureStroageCryptoException : SecureStorageException
    {
        public SecureStroageCryptoException() 
            : base("Unknown SecureStroageCryptoException")
        {
        }
        public SecureStroageCryptoException(string msg)
            : base(msg)
        {
        }
    }
}
