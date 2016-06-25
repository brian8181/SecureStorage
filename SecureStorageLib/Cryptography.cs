using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecureStorageLib
{
    /// <summary>
    /// Cryptography
    /// </summary>
    [Obsolete("use CryptographyLib namespace!")]
    public abstract class Cryptography : CryptographyLib.ICryptography
    {
        private const int DEFAULT_KEY_SIZE = 32;
        private readonly byte[] key = null;
        private readonly int key_size = DEFAULT_KEY_SIZE;
        
        public Cryptography(byte[] key)
            : this(key, DEFAULT_KEY_SIZE)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="key_size"></param>
        public Cryptography(byte[] key, int key_size)
        {
            this.key = key;
            this.key_size = key_size;
        }

        ///// <summary>
        ///// ctor
        ///// </summary>
        ///// <param name="key_path"></param>
        //public Cryptography(string key_path)
        //{
        //    key = File.ReadAllBytes(key_path);
        //}

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
        
        /// <summary>
        /// KeySize
        /// </summary>
        public abstract int KeySize { get; }
        
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>byte[]</returns>
        public abstract byte[] Encrypt(byte[] data);
        
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>byte[]</returns>
        public abstract byte[] Decrypt(byte[] data);

        /// <summary>
        /// GetSecureHash
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>string</returns>
        public string GetSecureHash(string data)
        {
            byte[] hash = SecureStorageUtility.HMACSHA256(data, Key);
            return SecureStorageUtility.FromBytesToHex(hash);
        }

        /// <summary>
        /// PBKDF2
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>byte[]</returns>
        public static byte[] PBKDF2(string password)
        {
            return DeriveKeyFunction.DeriveKey(password);
        }
    }
}
