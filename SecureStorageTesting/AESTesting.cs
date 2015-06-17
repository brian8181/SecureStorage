using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    public class AESTesting
    {
        private byte[] key = null;
        private byte[] iv = null;

        [Test]
        public void Encrypt()
        {
            string key_path = Global.TestFolder + "key";
            string file_path = Global.TestFolder + "file.txt";
            SecureStorageUtility.LoadKey(key_path, AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            byte[] data = File.ReadAllBytes(file_path);

            AES aes = new AES(key, iv);
            byte[] enc_data = aes.Encrypt(data);

            int data_len = data.Length;
            int expected_enc_length = (int)Math.Ceiling((double)(data_len / 16d)) * 16;
            // output len should equal input @ 16 byte blocks
            Assert.AreEqual(data_len, expected_enc_length);
        }
    }
}
