using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptographyLib;

namespace KeyStorage
{
    /// <summary>
    /// KeyStore
    /// </summary> 
    public class KeyStore
    {
        private const byte key_size = 32;
        private readonly string PATH = string.Empty;
        private readonly string PASSWORD = string.Empty;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="password"></param>
        public KeyStore(string path, string password)
        {
            this.PATH = path;
            this.PASSWORD = password;
        }
        
        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public byte[] this[uint i]
        {
            get 
            {
                byte[] wrapped_keys = File.ReadAllBytes(PATH);
                byte[] iter_bytes = new byte[4];
                byte[] salt = new byte[32];
                                      
                Array.Copy(wrapped_keys, 0, iter_bytes, 0, 4);
                Array.Copy(wrapped_keys, 4, salt, 0, 32);
                int iter = BitConverter.ToInt32(iter_bytes, 0);

                byte[] outter_key = DeriveKeyFunction.PBKDF2(PASSWORD, salt, iter, 32);
                                
                byte[] enc_keys = new byte[wrapped_keys.Length - (4 + 32)];
                Array.Copy(wrapped_keys, (4 + 32), enc_keys, 0, enc_keys.Length);

                Cryptography<AesCryptoServiceProvider> aes = new Cryptography<AesCryptoServiceProvider>(outter_key);
                byte[] denc_all_keys = aes.Decrypt(enc_keys);
                
                byte[] key = new byte[32];
                uint idx = i * 32;
                Array.Copy(denc_all_keys, idx, key, 0, 32);

                return key; 
            } 
        }

        /// <summary>
        /// CreateStore
        /// </summary>
        /// <param name="path">path to store</param>
        /// <param name="passowrd">password</param>
        /// <param name="key">keys</param>
        public static void CreateStore(string path, string passowrd, params byte[][] key)
        {
            byte[] key_data = new byte[key.Length * key_size];
            
            // pack all keys in an byte[]
            for (int i = 0; i < key.Length; ++i)
                Array.Copy(key[i], 0, key_data, i * 32, (int)key_size); 

            // enc with aes
            byte[] dk = DeriveKeyFunction.DeriveKey(passowrd) ;
            byte[] iter_bytes = new byte[4];
            byte[] salt = new byte[32];
            byte[] kek = new byte[32];
            
            // copy values
            Array.Copy(dk, 0, iter_bytes, 0, 4);
            Array.Copy(dk, 4, salt, 0, 32);
            Array.Copy(dk, 4 + 32, kek, 0, 32);
            int iters = BitConverter.ToInt32(iter_bytes, 0);

            Cryptography<AesCryptoServiceProvider> aes = new Cryptography<AesCryptoServiceProvider>(kek);
            //Cryptography<Aes> aes = new Cryptography<Aes>(kek);
            byte[] enc_all_keys = aes.Encrypt(key_data);

            byte[] iters_salt_enc_all_keys = new byte[enc_all_keys.Length + 4 + 32];
            // write salt and iters to enc_all_keys
            Array.Copy(iter_bytes, 0, iters_salt_enc_all_keys, 0, 4);
            Array.Copy(salt, 0, iters_salt_enc_all_keys, 4, 32);
            Array.Copy(enc_all_keys, 0, iters_salt_enc_all_keys, 4 + 32, enc_all_keys.Length);

            if (File.Exists(path))
                File.Delete(path);
             
            // save
            File.WriteAllBytes(path, iters_salt_enc_all_keys);
        }
    }
}
