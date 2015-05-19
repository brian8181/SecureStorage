using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SecureStorageTesting
{
    [TestFixture]
    public class SecureStoragePathTest
    {
        [Test]
        public void IsDirectoryTest()
        {
            string name = "a/";
            bool result = SecureStorageLib.StoragePath.IsDirectory(name);
            Assert.True(result);

            name = "a";
            result = SecureStorageLib.StoragePath.IsDirectory(name);
            Assert.False(result);
        }

        [Test]
        public void GetStoragePath()
        {
            string path = string.Empty;
            string store_path = string.Empty;

            path = "C:\\ABC\\EFG\\";
            store_path = SecureStorageLib.StoragePath.GetStoragePath(path);
            Assert.AreEqual(store_path, "ABC/EFG/");
             
            path = "ABC\\EFG\\";
            store_path = SecureStorageLib.StoragePath.GetStoragePath(path);
            Assert.AreEqual(store_path, "ABC/EFG/");

            path = "C:\\ABC\\EFG\\HIJ";
            store_path = SecureStorageLib.StoragePath.GetStoragePath(path);
            Assert.AreEqual(store_path, "ABC/EFG/HIJ/");
        }

        [TestCase("C:\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("M:\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("ABC\\EFG", "ABC/EFG/")]
        public void GetStoragePath2(string input, string output)
        {
            string result = SecureStorageLib.StoragePath.GetStoragePath(input);
            Assert.AreEqual(result, output);
        }
    }
}
