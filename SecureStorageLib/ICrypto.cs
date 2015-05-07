using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public interface ICrypto
    {
        //byte[] Key
        //{
        //    get;
        //    set;
        //}

        //byte[] IV
        //{
        //     get;
        //     set;
        //}

        byte[] Encrypt(byte[] key, byte[] iv, byte[] data);
        byte[] Decrypt(byte[] key, byte[] iv, byte[] data);
        string FromBytesToHex(byte[] array);
        byte[] SHA256(byte[] data);
    }
}
