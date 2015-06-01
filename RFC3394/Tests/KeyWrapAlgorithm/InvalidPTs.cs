
using System;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class KWA_InvalidPTs
   {
      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void WrapKey_NullPT1()
      {
         KeyWrapAlgorithm.WrapKey(ValidKEK, null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void WrapKey_NullPT2()
      {
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(ValidKEK);
         kwa.WrapKey(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_EmptyPT1()
      {
         byte[] pt = new byte[0];
         KeyWrapAlgorithm.WrapKey(ValidKEK, pt);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_EmptyPT2()
      {
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(ValidKEK);
         byte[] pt = new byte[0];
         kwa.WrapKey(pt);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_ShortPT1()
      {
         byte[] pt = new byte[8];
         KeyWrapAlgorithm.WrapKey(ValidKEK, pt);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_ShortPT2()
      {
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(ValidKEK);
         byte[] pt = new byte[8];
         kwa.WrapKey(pt);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void WrapKey_BadMultiplePT1()
      {
         byte[] pt = new byte[23];
         KeyWrapAlgorithm.WrapKey(ValidKEK, pt);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void WrapKey_BadMultiplePT2()
      {
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(ValidKEK);
         byte[] pt = new byte[23];
         kwa.WrapKey(pt);
      }

      byte[] ValidKEK
      {
         get { return new byte[16]; }
      }
   }
}
