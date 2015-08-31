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
        private AES aes = new AES( File.ReadAllBytes(Global.TestFolder + "key") );
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
        public void InitializeBuildDirectoryStructure()
        {
            SecureStorage store = new SecureStorage(new LocalStorage(path), aes, 1000);
            store.Initialize();

            //todo build stucture

            Assert.Inconclusive();
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
           
            SecureStorage store = new SecureStorage(new LocalStorage(path), aes, 1000);
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
            string unit_test_folder = Global.TestFolder + "CreateKeyLoadUse";
            if (!Directory.Exists(unit_test_folder))
                Directory.CreateDirectory(Global.TestFolder + "CreateKeyLoadUse");
            string key_path = unit_test_folder + "\\key";

            SecureStorageUtility.GererateWriteKey(unit_test_folder + "\\key", AES.DEFAULT_KEY_SIZE);
            SecureStorage store = new SecureStorage(new LocalStorage("test_tmp"), new AES(File.ReadAllBytes(key_path)), 1000);

            //BKP todo...

            //Assert.Fail(); // todo finish test case
            Assert.Inconclusive();
        } 
    }
}
