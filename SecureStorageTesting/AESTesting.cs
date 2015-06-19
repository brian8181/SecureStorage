using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    public class AESTesting
    {
        private byte[] key = null;
        private byte[] iv = null;

        [SetUp]
        public void Init()
        {
            string key_path = Global.TestFolder + "key";
            SecureStorageUtility.LoadKey(key_path, AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
        }

        [TearDown]
        public void Dispose()
        {
        }

        [Test]
        public void EncryptLengthCheckOriginal()
        {
            string file_path = Global.TestFolder + "file.txt";
            byte[] data = File.ReadAllBytes(file_path);

            AES aes = new AES(key, iv);
            byte[] enc_data = aes.Encrypt(data);

            int data_len = data.Length;
            int expected_enc_length = (int)Math.Ceiling((double)(data_len / 16d)) * 16;
            // output len should equal input @ 16 byte blocks
            Assert.AreEqual(data_len, expected_enc_length);
        }

        [TestCase("1", 16)]
        [TestCase("12", 16)]
        [TestCase("123", 16)]
        [TestCase("123-123-123-123", 16)] // 15 bytes
        [TestCase("123-123-123-123-", 32)] // why 32 its only 16 bytes!
        [TestCase("123-123-123-123-123-123-123-123", 32)]
        [TestCase("123-123-123-123-123-123-123-123-", 48)]
        public void EncryptLengthCheck(string s, int expected)
        {
            byte[] data = Encoding.ASCII.GetBytes(s);

            AES aes = new AES(key, iv);
            byte[] enc_data = aes.Encrypt(data);

            int data_len = enc_data.Length;
            Assert.AreEqual(expected, data_len);
        }

        [Test]
        public void EncryptDecrypt()
        {
            string str_data = "you and I have been through that and it's not our fate";
            byte[] data = Encoding.ASCII.GetBytes(str_data);

            AES aes = new AES(key, iv);
            byte[] enc_data = aes.Encrypt(data);

            byte[] denc_data = aes.Decrypt(enc_data);
            string actual = Encoding.ASCII.GetString(denc_data);

            Assert.AreEqual(str_data, actual);
        }
    }
}
