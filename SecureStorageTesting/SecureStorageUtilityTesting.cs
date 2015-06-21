using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    [TestFixture]
    public class SecureStorageUtilityTesting
    {
        byte[] key = null;
        byte[] iv = null;

        [SetUp]
        public void Init()
        {
            SecureStorageUtility.LoadKey("C:\\tmp\\aes_key\\key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
        }

        [TearDown]
        public void Dispose()
        {
        }

        //[Test]
        //public void ReadHashFromXml()
        //{
            
        //    SecureStorageUtility.CreateKey(Global.TestFolder + "key", 32, 16);
        //    SecureStorageUtility.LoadKey(Global.TestFolder + "key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
        //    SecureStorage store = new SecureStorage(new LocalStorage(Global.TestFolder + "local"), new AES(key, iv), 1000);
        //    store.Initialize();

        //    byte[] data = File.ReadAllBytes(Global.TestFolder + "file.txt");~
        //    string name = "blah";
        //    store.CreateFile(name, data);


        //    //SecureStorageUtility.ReadHashFromXml(, name);
           
        //}

        [Test]
        public void LoadKey()
        {
            byte[] key = null;
            byte[] iv = null;

            SecureStorageUtility.LoadKey("C:\\tmp\\aes_key\\key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
          
            Assert.IsNotNull(key);
            Assert.AreEqual(AES.KEY_SIZE, key.Length);

            Assert.IsNotNull(iv);
            Assert.AreEqual(AES.IV_SIZE, iv.Length);
        }

        //public void GetSecureName(string name, byte[] key)
        //{
        //    string name = SecureStorageUtility.GetSecureName("abc", key);
        //}

        [Test]
        public void GererateKey()
        {
            byte[] key = SecureStorageUtility.GererateKey(10);
            Assert.AreEqual(10, key.Length);
        }

        [TestCase("/", "ad9ab4fe58a9eb1473c4b60dc7be1216e7f10900e8cdf6a54853f797da0485d1")]
        [TestCase("abc/", "50370210a407c8745652831cbec125e1129e6a11270749e6fbd4ed11672e792c")]
        public void HMACSHA256(string name, string secure_name)
        {
            byte[] hash = SecureStorageUtility.HMACSHA256(name, key);
            string actual = SecureStorageUtility.FromBytesToHex(hash);
            Assert.AreEqual(secure_name, actual);

        }
    }
}
 