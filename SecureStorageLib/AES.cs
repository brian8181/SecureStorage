using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SecureStorageLib
{
    /// <summary>
    /// AES implemenation of ICypto inteface
    /// </summary>
    public class AES : ICryptography
    {
        private byte[] key = null;
        private byte[] iv = null;
        public const int KEY_SIZE = 32;
        public const int IV_SIZE = 16;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="iv">intitialzation vector</param>
        public AES(byte[] key, byte[] iv)
        {
            if (iv.Length != IV_SIZE && key.Length != KEY_SIZE)
                throw new SecureStorageCryptoException("Wrong key/iv size.");
            this.iv = iv;
            this.key = key;
        }

        ///// <summary>
        ///// intitialzation vector random gereration
        ///// </summary>
        ///// <param name="key"></param>
        //public AES(byte[] key)
        //{
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

        ///// <summary>
        ///// initialization vector
        ///// </summary>
        //public byte[] IV
        //{
        //    get
        //    {
        //        return iv;
        //    }
        //}

        /// <summary>
        /// size of the key
        /// </summary>
        public int KeySize
        {
            get
            {
                return KEY_SIZE;
            }
        }

        ///// <summary>
        ///// size of initialization vector
        ///// </summary>
        //public int IVSize
        //{
        //    get
        //    {
        //        return KEY_SIZE;
        //    }
        //}

        
        // BKP todo
        public byte[] EncryptRI(byte[] data)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                // load key & iv
                csp.Key = key;
                csp.GenerateIV();
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
                        ms.Write(iv, 0, AES.IV_SIZE);
                        byte[] output = ms.ToArray();
                        return output;
                    }
                }
            }
        }

        // BKP todo
        public byte[] DecryptRI(byte[] data)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                // load key & iv
                csp.Key = key;
                
                // get random iv from end of data
                long data_len = data.Length;
                long len = data_len - AES.IV_SIZE;
                Array.Copy(data, len, iv, 0, AES.IV_SIZE);
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

        /// <summary>
        /// encrypt data
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        public byte[] Encrypt(byte[] data)
        {
            using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
            {
                // load key & iv
                csp.Key = key;
                csp.IV = iv;
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
                // load key & iv
                csp.Key = key;
                csp.IV = iv;
                ICryptoTransform decryptor = csp.CreateDecryptor(key, iv);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        long len = data.Length;
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
    }
}
