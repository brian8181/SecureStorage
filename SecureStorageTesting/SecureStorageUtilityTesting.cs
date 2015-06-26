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
        //byte[] iv = null;

        [SetUp]
        public void Init()
        {
            //SecureStorageUtility.LoadKey("C:\\tmp\\aes_key\\key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            key = SecureStorageUtility.LoadKey_2(Global.TestFolder + "key");
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
            //byte[] iv = null;

            key = SecureStorageUtility.LoadKey_2(Global.TestFolder + "key");
          
            Assert.IsNotNull(key);
            Assert.AreEqual(AES.KEY_SIZE, key.Length);

            //Assert.IsNotNull(iv);
            //Assert.AreEqual(AES.IV_SIZE, iv.Length);
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

        [TestCase("/", "a9c97d5c817850982cba39e31164290865c70a7c282664431ddf4048e58a47dd")]
        [TestCase("abc/", "72780de7e8c16f220ed13e5eb80fc8cb28657a327295f774c1ea3d758f8cf94a")]
        [TestCase("abc/efg/", "881bceff7c5e40a476a752a6906724077cbf72856d4928c2015be3a393c0bdad")]
        public void HMACSHA256(string name, string secure_name)
        {
            byte[] hash = SecureStorageUtility.HMACSHA256(name, key);
            string actual = SecureStorageUtility.FromBytesToHex(hash);
            Assert.AreEqual(secure_name, actual);
        }
    }
}
 