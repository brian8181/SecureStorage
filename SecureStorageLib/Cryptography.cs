using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecureStorageLib
{
    public abstract class Cryptography : ICryptography
    {
        byte[] key = null;

        public Cryptography(string key_path)
        {
            key = File.ReadAllBytes(key_path);
        }

        /// <summary>
        /// key
        /// </summary>
        public byte[] Key
        {
            get
            {
                return key;
            }
        }
        
        public abstract int KeySize { get; }

        public abstract byte[] Encrypt(byte[] data);
        
        public abstract byte[] Decrypt(byte[] data);

        public string GetSecureHash(string data)
        {
            byte[] hash = SecureStorageUtility.HMACSHA256(data, Key);
            return SecureStorageUtility.FromBytesToHex(hash);
        }
    }
}
