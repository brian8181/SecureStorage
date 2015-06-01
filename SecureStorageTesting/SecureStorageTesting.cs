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

        [SetUp]
        public void Init()
        {
            string cur_dir = Directory.GetCurrentDirectory();
            Directory.CreateDirectory("test_tmp");
        }

        [TearDown]
        public void Dispose()
        {
            //Directory.Delete("test_tmp");
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
        public void CreateKeyLoadUse()
        {
            SecureStorageUtility.CreateKey("test_tmp/key", 32, 16);
            SecureStorageUtility.LoadKey("test_tmp/key", AES.KEY_SIZE, AES.IV_SIZE, out key, out iv);
            SecureStorage store = new SecureStorage(new LocalStorage("test_tmp"), new AES(key, iv), 1000);

            //BKP todo...

            Assert.Fail(); // todo finish test case
        } 
    }
}
