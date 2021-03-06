﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SecureStorageLib;
using System.Security.Cryptography;

namespace SecureStorageTesting
{
    [TestFixture]
    public class AESTesting
    {

       private readonly string KEY_PATH = Global.TestFolder + "key";
        
        [SetUp]
        public void Init()
        {
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

            AES aes = new AES( File.ReadAllBytes(KEY_PATH) );
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

            AES aes = new AES(File.ReadAllBytes(KEY_PATH));
            byte[] enc_data = aes.Encrypt(data);

            int data_len = enc_data.Length - AES.DEFAULT_IV_SIZE; // subtract IV length that is appended
            Assert.AreEqual(expected, data_len);
        }

        [Test]
        public void EncryptDecrypt()
        {
            //string str_data = "you and I have been through that and it's not our fate";
            byte[] data = Encoding.ASCII.GetBytes(Global.STRING_TEST_DATA);

            AES aes = new AES(File.ReadAllBytes(KEY_PATH));
            byte[] enc_data = aes.Encrypt(data);

            byte[] denc_data = aes.Decrypt(enc_data);
            string actual = Encoding.ASCII.GetString(denc_data);

            Assert.AreEqual(Global.STRING_TEST_DATA, actual);
        }

        [Test]
        public void EncryptDecrypt_RamdomIV()
        {
            byte[] data = Encoding.ASCII.GetBytes(Global.STRING_TEST_DATA);

            AES aes1 = new AES(File.ReadAllBytes(KEY_PATH));
            byte[] enc_data = aes1.Encrypt(data);

            AES aes2 = new AES(File.ReadAllBytes(KEY_PATH));
            byte[] denc_data = aes2.Decrypt(enc_data);
            string actual = Encoding.ASCII.GetString(denc_data);

            Assert.AreEqual(data.Length, denc_data.Length);
            Assert.AreEqual(Global.STRING_TEST_DATA, actual);
        }

        [Test]
        public void EncryptDecrypt_RamdomIV_Generic()
        {
            byte[] data = Encoding.ASCII.GetBytes(Global.STRING_TEST_DATA);
            byte[] key = File.ReadAllBytes(KEY_PATH);

            CryptographyLib.Cryptography<AesCryptoServiceProvider> aes1 = 
                new CryptographyLib.Cryptography<AesCryptoServiceProvider>(key);

            // test encyrpt by ICryptography interface
            byte[] enc_data = Encrypt(aes1, data);
                   
            CryptographyLib.Cryptography<AesCryptoServiceProvider> aes2 =
               new CryptographyLib.Cryptography<AesCryptoServiceProvider>(key);

            // test encyrpt by ICryptography interface
            byte[] denc_data = Decrypt(aes1, enc_data);
            string actual = Encoding.ASCII.GetString(denc_data);

            Assert.AreEqual(data.Length, denc_data.Length);
            Assert.AreEqual(Global.STRING_TEST_DATA, actual);
        }

        public byte[] Encrypt(CryptographyLib.ICryptography crypt, byte[] data)
        {
            return crypt.Encrypt(data);
        }

        public byte[] Decrypt(CryptographyLib.ICryptography crypt, byte[] data)
        {
            return crypt.Decrypt(data);
        }
    }
}
