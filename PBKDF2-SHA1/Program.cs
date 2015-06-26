using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBKDF2_SHA1
{
    class Program
    {
        static void Main(string[] args)
        {
            string hash = Utility.PasswordHash.CreateHash("abc");
            byte[] key = PBKDF2_SHA1.DeriveKeyFunction.DeriveKey("abc");
        }
    }
}
