using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    public class SecureStorageLockExcpetion : SecureStorageException
    {
        SecureStorageLockExcpetion(string msg)
            : base(msg)
        {
        }
    }
}
