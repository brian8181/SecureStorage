using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public class SecureStorageException : Exception
    {
        public SecureStorageException()
            : base("Unknown SecureStorageException")
        {
        }
        public SecureStorageException(string msg)
            : base(msg)
        {
        }
    }
}
