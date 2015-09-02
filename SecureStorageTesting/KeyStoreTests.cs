using NUnit.Framework;
using SecureStorageLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureStorageTesting
{
    [TestFixture]
    public class KeyStoreTests
    {
        [Test]
        public void CreateKeyStore()
        {
            string test_path = Global.TestFolder + "CreateKeyStore_key";
            byte[] key1 = SecureStorageUtility.GererateKey(32);
            byte[] key2 = SecureStorageUtility.GererateKey(32);
            byte[] key3 = SecureStorageUtility.GererateKey(32);

            KeyStore.CreateStore(test_path, "abc", key1, key2, key3);

            KeyStore store = new KeyStore(test_path, "abc");
            byte[] key11 = store[0];
            byte[] key22 = store[1];
            byte[] key33 = store[2];

            CollectionAssert.AreEqual(key1, key11);

            CollectionAssert.AreNotEqual(key2, key11);
            CollectionAssert.AreEqual(key2, key22);

            CollectionAssert.AreNotEqual(key3, key22);
            CollectionAssert.AreEqual(key3, key33);
        }
    }
}
