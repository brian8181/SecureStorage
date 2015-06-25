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
        [TestCase("/", true)]
        [TestCase("/a", false)]
        [TestCase("a/", true)]
        [TestCase("a/b", false)]
        [TestCase("a/b/", true)]
        public void IsDirectoryTest(string name, bool expected)
        {
            bool actual = SecureStorageLib.StoragePath.IsDirectory(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("space test.jpg", true)]
        [TestCase("tab  test.jpg", true)]
        [TestCase("abc.jpg", true)]
        [TestCase("1`~@#$%^&_-+=.abc", true)]
        [TestCase("abc?.jpg", false)]
        [TestCase("abc/", false)]
        [TestCase("abc\\", false)]
        [TestCase("abc|abc", false)]
        public void IsValidName(string name, bool input)
        {
            bool output = SecureStorageLib.StoragePath.IsValidName(name);
            Assert.AreEqual(output, input);
        }

        [TestCase("test.jpg", true)]
        [TestCase("a/test.jpg", true)]
        [TestCase("a/b/test.jpg", true)]
        [TestCase("a//test.jpg", false)]
        public void IsValidPath(string path, bool input)
        {
            string[] names = path.Split('/');
            // todo
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

        [TestCase("a/b", "a/")]
        //[TestCase("a//b", "a/")]
        [TestCase("foo/boo/doo/goo/too", "foo/boo/doo/goo/")]
        [TestCase("a/b/c", "a/b/")]
        [TestCase("a/b/c/", "a/b/")]
        [TestCase("a/", "/")]
        public void GetDirectory(string path, string expected)
        {
            string actual = SecureStorageLib.StoragePath.GetDirectory(path);
            Assert.AreEqual(expected, actual);
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
