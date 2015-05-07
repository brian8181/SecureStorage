using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    interface ICrypto
    {
        byte[] Encrypt(byte[] key, byte[] iv, byte[] data);
        byte[] Decrypt(byte[] key, byte[] iv, byte[] data);
        string FromBytesToHex(byte[] array);
        byte[] SHA256(byte[] data);
    }
}
