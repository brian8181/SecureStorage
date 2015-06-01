
using System;
using System.Security.Cryptography;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class KWA_IntegrityTests
   {
      [Test]
      [ExpectedException(typeof(CryptographicException))]
      public void MangledCiphertext()
      {
         string kek = "000102030405060708090A0B0C0D0E0F";
         string pt = "00112233445566778899AABBCCDDEEFF";

         byte[] key = SoapHexBinary.Parse(kek).Value;
         byte[] input = SoapHexBinary.Parse(pt).Value;
         byte[] output = KeyWrapAlgorithm.WrapKey(key, input);

         output[0] ^= 0x01;  // mangle the ciphertext

         KeyWrapAlgorithm.UnwrapKey(key, output);
      }
   }
}
