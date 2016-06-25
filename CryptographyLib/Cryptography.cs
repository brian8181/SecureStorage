using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLib
{
    /// <summary>
    /// Cryptography
    /// </summary>
    public /*abstract*/ class Cryptography<T> : ICryptography
        where T : SymmetricAlgorithm, new()
    {
        //private SymmetricAlgorithm csp = null;
        private const int DEFAULT_KEY_SIZE = 32;
        private const int DEFAULT_IV_SIZE = 16;
        private readonly byte[] key = null;
        private readonly int key_size = DEFAULT_KEY_SIZE;

        //private readonly uint[] legal_key_sizes;
        
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
        public int KeySize 
        {
            get { return key_size; }
        }
        
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>byte[]</returns>
        //public abstract byte[] Encrypt(byte[] data);


        /// <summary>
        /// encrypt data
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        public byte[] Encrypt(byte[] data)
        {
            using (T csp = new T())
            {
                // load key & iv
                csp.Key = key;
                csp.GenerateIV();
                // this vs gen random bytes ??? which is better, I want random why not use CSPRNG, RNGCryptoServiceProvider
                byte[] iv = csp.IV;
                int len = data.Length;

                ICryptoTransform encryptor = csp.CreateEncryptor(key, iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Write all data to the crypto stream and flush it.
                        cs.Write(data, 0, len);
                        cs.FlushFinalBlock();

                        // write iv to output
                        long ms_len = ms.Length;
                        ms.Write(iv, 0, DEFAULT_IV_SIZE);
                        byte[] output = ms.ToArray();
                        return output;
                    }
                }
            }
        }
        
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>byte[]</returns>
        //public abstract byte[] Decrypt(byte[] data);

        /// <summary>
        /// decrypt data
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        public byte[] Decrypt(byte[] data)
        {
            using (T csp = new T())
            {
                // load key & iv
                csp.Key = key;
                byte[] iv = new byte[DEFAULT_IV_SIZE];

                // get random iv from end of data
                long data_len = data.Length;
                long len = data_len - DEFAULT_IV_SIZE;
                Array.Copy(data, len, iv, 0, DEFAULT_IV_SIZE);
                csp.IV = iv;

                // why do i need this
                byte[] enc_data = new byte[len];
                Array.Copy(data, 0, enc_data, 0, len);

                ICryptoTransform decryptor = csp.CreateDecryptor(key, iv);
                using (MemoryStream ms = new MemoryStream(enc_data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[len];
                        int read = cs.Read(buffer, 0, (int)len);
                        if (len != read)
                        {
                            byte[] copy_buffer = new byte[read];
                            Array.Copy(buffer, copy_buffer, read);
                            buffer = copy_buffer;
                        }
                        return buffer;
                    }
                }
            }
        }

        ///// <summary>
        ///// GetSecureHash
        ///// </summary>
        ///// <param name="data">data</param>
        ///// <returns>string</returns>
        //public string GetSecureHash(string data)
        //{
        //    byte[] hash = SecureStorageUtility.HMACSHA256(data, Key);
        //    return SecureStorageUtility.FromBytesToHex(hash);
        //}

        ///// <summary>
        ///// PBKDF2
        ///// </summary>
        ///// <param name="password">password</param>
        ///// <returns>byte[]</returns>
        //public static byte[] PBKDF2(string password)
        //{
        //    return DeriveKeyFunction.DeriveKey(password);
         
    }
}
