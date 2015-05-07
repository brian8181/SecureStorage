using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public interface ICrypto
    {
        byte[] Key
        {
            get;
        }

        byte[] IV
        {
            get;
        }

        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        string FromBytesToHex(byte[] array);
        byte[] SHA256(byte[] data);
        byte[] HMACSHA256(string name);
    }
}
