﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SecureStorageLib
{
    /// <summary>
    /// AES implemenation of ICypto inteface
    /// </summary>
    public class AES : ICrypto
    {
        private byte[] key = null;
        private byte[] iv = null;
        public const int KEY_SIZE = 32;
        public const int IV_SIZE = 16;

        public AES(byte[] key, byte[] iv)
        {
            if (iv.Length != IV_SIZE && key.Length != KEY_SIZE)
                throw new SecureStorageCryptoException("Wrong key/iv size.");
            this.iv = iv;
            this.key = key;
        }

        public byte[] Key
        {
            get
            {
                return key;
            }
        }

        public byte[] IV
        {
            get
            {
                return iv;
            }
       }

        public int KeySize
        {
            get
            {
                return KEY_SIZE;
            }
        }

        public int IVSize
        {
            get
            {
                return KEY_SIZE;
            }
        }

        /// <summary>
        /// encrypt data
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        public byte[] Encrypt(byte[] data)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                csp.Key = key;
                csp.IV = iv;

                // Get an encryptor.
                ICryptoTransform encryptor = csp.CreateEncryptor(key, iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Write all data to the crypto stream and flush it.
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// decrypt data
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        public byte[] Decrypt(byte[] data)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                csp.Key = key;
                csp.IV = iv;

                //Get a decryptor that uses the same key and IV as the encryptor.
                ICryptoTransform decryptor = csp.CreateDecryptor(key, iv);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {

                        // read it into buffer
                        List<byte> buffer = new List<byte>();
                        int b = cs.ReadByte();
                        while (b != -1)
                        {
                            buffer.Add((byte)b);
                            b = cs.ReadByte();
                        }

                        return buffer.ToArray();
                    }
                }
            }
        }
    }
}