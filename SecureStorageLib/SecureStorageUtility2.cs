using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureStorageLib
{
    public static class SecureStorageUtility2
    {
        /// <summary>
        /// get a secure name
        /// </summary>
        /// <param name="name">orginal name</param>
        /// <returns>secure name based off original</returns>
        private static string GetSecureName(string name, byte[] key)
        {
            //if (KeyLoaded != true)
            //    throw new Exception("key not loaded");

            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return FromBytesToHex(hash);
        }

        public static byte[] HMACSHA256(string name, byte[] key)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return hash;
        }

        public static byte[] SHA256(byte[] data)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] result = sha256.ComputeHash(data);
            return result;
        }

        public static string FromBytesToHex(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static void LoadKey(string path, int key_size, int iv_size, out byte[] key, out byte[] iv)
        {
            byte[] key_iv = File.ReadAllBytes(path);

            iv = new byte[iv_size];
            key = new byte[key_size];

            Array.Copy(key_iv, key, key_size);
            Array.Copy(key_iv, key_size, iv, 0, iv_size);
        }
        
        /// <summary>
        /// create a key and write it to specified name
        /// </summary>
        /// <param name="name">name to write the key</param>
        public static void CreateKey(string path, int key_size, int iv_size)
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
