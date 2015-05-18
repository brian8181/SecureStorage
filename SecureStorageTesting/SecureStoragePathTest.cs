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
        }
    }
}
