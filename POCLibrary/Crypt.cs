using System;
using Utility;

namespace POCLibrary
{
    class Crypt
    {
        public byte[] Encrypt(byte[] key, byte[] data)
        {
            return CryptoFunctions.Encrypt(key, data);
        }

        public byte[] Decrypt(byte[] key, byte[] data)
        {
            return CryptoFunctions.Decrypt(key, data);
        }
    }
}
