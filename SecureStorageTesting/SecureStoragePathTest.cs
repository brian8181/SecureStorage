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

      
        [TestCase("C:\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("M:\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("\\ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("ABC\\EFG\\", "ABC/EFG/")]
        [TestCase("ABC\\EFG", "ABC/EFG/")]
        [TestCase("C:\\ABC\\EFG\\HIJ", "ABC/EFG/HIJ/")]
        public void GetStoragePath(string input, string output)
        {
            string result = SecureStorageLib.StoragePath.GetStoragePath(input);
            Assert.AreEqual(output, result);
        }

        [TestCase("a/b/c", "a/b/")]
        [TestCase("a/b/c/", "a/b/")]
        [TestCase("a/", "/")]
        public void GetDirectory(string path, string output)
        {
            string result = SecureStorageLib.StoragePath.GetDirectory(path);
            Assert.AreEqual(output, result);
        }

        [TestCase("a/b/c", "c")]
        [TestCase("a/b/c/", null)]
        public void GetShortName(string path, string output)
        {
            string result = SecureStorageLib.StoragePath.GetShortName(path);
            Assert.AreEqual(output, result);
        }

        [TestCase("a/b/c/", new string[] {"a/b/", "a/"})]
        public void GetSubDirectories(string path, string[] output)
        {
            string[] result = SecureStorageLib.StoragePath.GetSubDirectories(path);

            Assert.AreEqual(output.Length, result.Length);
            for (int i = 0; i < result.Length; ++i)
            {
                Assert.AreEqual(output[i], result[i]);
            }
        }
    }
}
