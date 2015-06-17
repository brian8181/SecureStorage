using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    [TestFixture]
    public class SecureStorageTesting
    {
        byte[] key = null;
        byte[] iv = null;
        private string path = Global.TestFolder + "tmp\\";

        [SetUp]
        public void Init()
        {

            string cur_dir = Directory.GetCurrentDirectory();
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }

        [TearDown]
        public void Dispose()
        {
            
        }

        [Test]
        public void Initialize()
        {
        }

        [Test]
        public void AppendNameXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\test\\dir.xml");
            //BKP todo...
            string xml = doc.OuterXml;
            Assert.IsFalse( string.IsNullOrWhiteSpace(xml) );
        }

        [Test]
        public void CreateReadFile()
        {
            SecureStorageUtility.CreateKey(path + "key", 32, 16);
            SecureStorageUtility.LoadKey(path + "key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            SecureStorage store = new SecureStorage(new LocalStorage(path), new AES(key, iv), 1000);
            store.Initialize();
            

            //string file_name = "file.txt";
            //byte[] data = File.ReadAllBytes(path + file_name);
            
            //store.CreateFile(file_name, data);
            //byte[] o_data = store.Read(file_name);

            //Assert.AreEqual(data.Length, o_data.Length);

            //for (int i = 0; i < data.Length; ++i)
            //{
            //    Assert.AreEqual(data[i], o_data[i]);
            //}

            //Assert.IsTrue(false);
            Assert.Inconclusive();
        }

        [Test]
        public void CreateKeyLoadUse()
        {
            SecureStorageUtility.CreateKey("test_tmp/key", 32, 16);
            SecureStorageUtility.LoadKey("test_tmp/key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            SecureStorage store = new SecureStorage(new LocalStorage("test_tmp"), new AES(key, iv), 1000);

            //BKP todo...

            //Assert.Fail(); // todo finish test case
            Assert.Inconclusive();
        } 
    }
}
