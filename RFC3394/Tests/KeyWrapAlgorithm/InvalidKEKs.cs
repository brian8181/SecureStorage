
using System;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class KWA_InvalidKEKs
   {
      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Constructor_NullKEK()
      {
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_EmptyKEK()
      {
         byte[] kek = new byte[0];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_ShortKEK1()
      {
         byte[] kek = new byte[8];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_ShortKEK2()
      {
         byte[] kek = new byte[15];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_BadSizedKEK1()
      {
         byte[] kek = new byte[20];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_LongKEK1()
      {
         byte[] kek = new byte[33];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_LongKEK2()
      {
         byte[] kek = new byte[40];
         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void WrapKey_NullKEK()
      {
         KeyWrapAlgorithm.WrapKey(null, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_EmptyKEK()
      {
         byte[] kek = new byte[0];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_ShortKEK1()
      {
         byte[] kek = new byte[8];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_ShortKEK2()
      {
         byte[] kek = new byte[15];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_BadSizedKEK1()
      {
         byte[] kek = new byte[20];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_LongKEK1()
      {
         byte[] kek = new byte[33];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WrapKey_LongKEK2()
      {
         byte[] kek = new byte[40];
         KeyWrapAlgorithm.WrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void KeyUnwrap_NullKEK()
      {
         KeyWrapAlgorithm.UnwrapKey(null, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_EmptyKEK()
      {
         byte[] kek = new byte[0];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_ShortKEK1()
      {
         byte[] kek = new byte[8];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_ShortKEK2()
      {
         byte[] kek = new byte[15];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_BadSizedKEK1()
      {
         byte[] kek = new byte[20];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_LongKEK1()
      {
         byte[] kek = new byte[33];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void KeyUnwrap_LongKEK2()
      {
         byte[] kek = new byte[40];
         KeyWrapAlgorithm.UnwrapKey(kek, new byte[16]);
      }
   }
}
