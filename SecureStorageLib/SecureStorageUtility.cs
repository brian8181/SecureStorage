using System;
using System.IO;
using System.Security.Cryptography;

namespace SecureStorageLib
{
    public static class SecureStorageUtility
    {
        // BKP const?
        //private readonly int KEY_SIZE = 32;
        //private readonly int IV_SIZE = 16;

        /// <summary>
        /// loads key from file into variables (key & iv)
        /// </summary>
        /// <param name="name"></param>
        

        //..
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key_size"></param>
        /// <param name="iv_size"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public static void LoadKey(string path, int key_size, int iv_size, out byte[] key, out byte[] iv)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            iv = new byte[iv_size];
            key = new byte[key_size];

            Array.Copy(key_iv, key, 32);
            Array.Copy(key_iv, 32, iv, 0, 16);
        }

        /// <summary>
        /// create a key and write it to specified name
        /// </summary>
        /// <param name="name">name to write the key</param>
        public static void CreateKey(string path, int key_size = 0, int iv_size = 0)
        {
            //BKP todo why managed?
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] key = new byte[aes.Key.Length + aes.IV.Length];

            Array.Copy(aes.Key, key, aes.Key.Length);
            Array.Copy(aes.IV, 0, key, aes.Key.Length, aes.IV.Length);

            File.WriteAllBytes(path, key);
        }
    }
}
