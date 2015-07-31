using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Utility;

namespace RSATest
{
    [TestFixture]
    public class RSATesting
    {
        [TestCase("I think therfore I am")]
        [TestCase("I think therfore I am0")]
        public void RSA_PaddingTest(string str)
        {
            QuickRSA rsa = new QuickRSA();
            QuickRSA.CreateKeyPair("..\\..\\test\\keys");
            rsa.LoadKeys("..\\..\\test\\keys\\pri.key", "..\\..\\test\\keys\\pub.key");

            byte[] data = Encoding.ASCII.GetBytes(str);
            int data_len = data.Length;
            byte[] enc_data = rsa.Encrypt(data);
            int enc_data_len = enc_data.Length;

            // is this right
            Assert.AreEqual(256, enc_data_len);
        }
    }
}
