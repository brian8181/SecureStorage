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
    }
}
 