using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SecureStorageLib;

namespace SecureStorageTesting
{
    [TestFixture]
    public class SecureStorageUtilityTesting
    {
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
    }
}
 