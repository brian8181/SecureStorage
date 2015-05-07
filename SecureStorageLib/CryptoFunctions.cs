using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace SecureStorageLib
{
    public class CryptoFunctions
    {
        public static byte[] Encrypt(byte[] key, byte[] data, byte[] iv)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;
                // Get an encryptor.
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

                // Write all data to the crypto stream and flush it.
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();

                cs.Close();
                ms.Close();

                return ms.ToArray();
            }
        }

        public static byte[] Decrypt(byte[] key, byte[] data, byte[] iv)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                //Get a decryptor that uses the same key and IV as the encryptor.
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                MemoryStream ms = new MemoryStream(data);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

                // read it into buffer
                List<byte> buffer = new List<byte>();
                int b = cs.ReadByte();
                while (b != -1)
                {
                    buffer.Add((byte)b);
                    b = cs.ReadByte();
                }

                cs.Close();
                ms.Close();
                
                return buffer.ToArray();
            }
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

        public static byte[] SHA256(byte[] data)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] result = sha256.ComputeHash(data);
            return result;
        }
    }
}
