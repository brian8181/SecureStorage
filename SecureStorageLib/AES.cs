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
        public const int DEFAULT_KEY_SIZE = 32;
        public const int DEFAULT_IV_SIZE = 16;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">key</param>
        public AES(byte[] key)
        {
            this.key = key;
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

        
        /// <summary>
        /// size of the key
        /// </summary>
        public int KeySize
        {
            get
            {
                return DEFAULT_KEY_SIZE;
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
                        ms.Write(iv, 0, AES.DEFAULT_IV_SIZE);
                        byte[] output = ms.ToArray();
                        return output;
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
                byte[] iv = new byte[DEFAULT_IV_SIZE];
                
                // get random iv from end of data
                long data_len = data.Length;
                long len = data_len - AES.DEFAULT_IV_SIZE;
                Array.Copy(data, len, iv, 0, AES.DEFAULT_IV_SIZE);
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
    }
}
