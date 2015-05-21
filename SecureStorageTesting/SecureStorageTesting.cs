using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    [TestFixture]
    public class SecureStorageTesting
    {
        byte[] key = null;
        byte[] iv = null;

        [SetUp]
        public void Init()
        {
            string cur_dir = Directory.GetCurrentDirectory();
            Directory.CreateDirectory("test_tmp");
        }

        [TearDown]
        public void Dispose()
        {
            Directory.Delete("test_tmp");
        }

        [Test]
        public void Initialize()
        {
            SecureStorageUtility.LoadKey("", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            SecureStorage store = new SecureStorage(new LocalStorage(""), new AES(key, iv), 1000);
            store.Initialize();
            Assert.Fail(); // todo finish test case
        } 
    }
}
