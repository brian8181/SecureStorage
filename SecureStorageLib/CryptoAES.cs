using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public class CryptoAES : ICrypto
    {
        #region ICrypto Members

        private byte[] key = null;
        private byte[] iv = null;

        public CryptoAES(byte[] key, byte[] iv)
        {
            //BKP todo: check key & iv len
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

        public byte[] SHA256(byte[] data)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] result = sha256.ComputeHash(data);
            return result;
        }

        public string FromBytesToHex(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public byte[] HMACSHA256(string name)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(Key);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(name);
            byte[] hash = hmacsha256.ComputeHash(data);
            return hash;
        }
        #endregion
    }
}
