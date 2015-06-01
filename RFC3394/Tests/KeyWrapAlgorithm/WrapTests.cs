
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class KWA_WrapTests
   {
      private void NonStaticWrap(string kek, string pt, string ct, string test)
      {
         byte[] key = SoapHexBinary.Parse(kek).Value;
         byte[] input = SoapHexBinary.Parse(pt).Value;

         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(key);
         byte[] output = kwa.WrapKey(input);

         ct = ct.Replace(" ", "");
         Assert.AreEqual(ct, new SoapHexBinary(output).ToString(), test);
      }

      private void StaticWrap(string kek, string pt, string ct, string test)
      {
         byte[] key = SoapHexBinary.Parse(kek).Value;
         byte[] input = SoapHexBinary.Parse(pt).Value;
         byte[] output = KeyWrapAlgorithm.WrapKey(key, input);

         ct = ct.Replace(" ", "");
         Assert.AreEqual(ct, new SoapHexBinary(output).ToString(), test);
      }

      private void NonStaticUnwrap(string kek, string ct, string pt,
            string test)
      {
         byte[] key = SoapHexBinary.Parse(kek).Value;
         byte[] input = SoapHexBinary.Parse(ct).Value;

         KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(key);
         byte[] output = kwa.UnwrapKey(input);

         pt = pt.Replace(" ", "");
         Assert.AreEqual(pt, new SoapHexBinary(output).ToString(), test);
      }

      private void StaticUnwrap(string kek, string ct, string pt,
            string test)
      {
         byte[] key = SoapHexBinary.Parse(kek).Value;
         byte[] input = SoapHexBinary.Parse(ct).Value;
         byte[] output = KeyWrapAlgorithm.UnwrapKey(key, input);

         pt = pt.Replace(" ", "");
         Assert.AreEqual(pt, new SoapHexBinary(output).ToString(), test);
      }

      #region Section 4.1

      [Test]
      public void Wrap_128key_128kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5";

         NonStaticWrap(kek, pt, ct, "Wrap_128key_128kek_NonStatic");
      }

      [Test]
      public void Wrap_128key_128kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5";

         StaticWrap(kek, pt, ct, "Wrap_128key_128kek_Static");
      }

      [Test]
      public void Unwrap_128key_128kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F";
         string ct = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5";
         string pt = "00112233445566778899AABBCCDDEEFF";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_128key_128kek_NonStatic");
      }

      [Test]
      public void Unwrap_128key_128kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F";
         string ct = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5";
         string pt = "00112233445566778899AABBCCDDEEFF";

         StaticUnwrap(kek, ct, pt, "Unwrap_128key_128kek_Static");
      }

      #endregion

      #region Section 4.2

      [Test]
      public void Wrap_128key_192kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D";

         NonStaticWrap(kek, pt, ct, "Wrap_128key_192kek_NonStatic");
      }

      [Test]
      public void Wrap_128key_192kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D";

         StaticWrap(kek, pt, ct, "Wrap_128key_192kek_Static");
      }

      [Test]
      public void Unwrap_128key_192kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string ct = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D";
         string pt = "00112233445566778899AABBCCDDEEFF";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_128key_192kek_NonStatic");
      }

      [Test]
      public void Unwrap_128key_192kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string ct = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D";
         string pt = "00112233445566778899AABBCCDDEEFF";

         StaticUnwrap(kek, ct, pt, "Unwrap_128key_192kek_Static");
      }

      #endregion

      #region Section 4.3

      [Test]
      public void Wrap_128key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7";

         NonStaticWrap(kek, pt, ct, "Wrap_128key_256kek_NonStatic");
      }

      [Test]
      public void Wrap_128key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF";
         string ct = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7";

         StaticWrap(kek, pt, ct, "Wrap_128key_256kek_Static");
      }

      [Test]
      public void Unwrap_128key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7";
         string pt = "00112233445566778899AABBCCDDEEFF";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_128key_256kek_NonStatic");
      }

      [Test]
      public void Unwrap_128key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7";
         string pt = "00112233445566778899AABBCCDDEEFF";

         StaticUnwrap(kek, ct, pt, "Unwrap_128key_256kek_Static");
      }

      #endregion

      #region Section 4.4

      [Test]
      public void Wrap_192key_192kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";
         string ct = "031D33264E15D332 68F24EC260743EDC E1C6C7DDEE725A93 6BA814915C6762D2";

         NonStaticWrap(kek, pt, ct, "Wrap_192key_192kek_NonStatic");
      }

      [Test]
      public void Wrap_192key_192kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";
         string ct = "031D33264E15D332 68F24EC260743EDC E1C6C7DDEE725A93 6BA814915C6762D2";

         StaticWrap(kek, pt, ct, "Wrap_192key_192kek_Static");
      }

      [Test]
      public void Unwrap_192key_192kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string ct = "031D33264E15D332 68F24EC260743EDC E1C6C7DDEE725A93 6BA814915C6762D2";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_192key_192kek_NonStatic");
      }

      [Test]
      public void Unwrap_192key_192kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F1011121314151617";
         string ct = "031D33264E15D332 68F24EC260743EDC E1C6C7DDEE725A93 6BA814915C6762D2";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";

         StaticUnwrap(kek, ct, pt, "Unwrap_192key_192kek_Static");
      }

      #endregion

      #region Section 4.5

      [Test]
      public void Wrap_192key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";
         string ct = "A8F9BC1612C68B3F F6E6F4FBE30E71E4 769C8B80A32CB895 8CD5D17D6B254DA1";

         NonStaticWrap(kek, pt, ct, "Wrap_192key_256kek_NonStatic");
      }

      [Test]
      public void Wrap_192key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";
         string ct = "A8F9BC1612C68B3F F6E6F4FBE30E71E4 769C8B80A32CB895 8CD5D17D6B254DA1";

         StaticWrap(kek, pt, ct, "Wrap_192key_256kek_Static");
      }

      [Test]
      public void Unwrap_192key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "A8F9BC1612C68B3F F6E6F4FBE30E71E4 769C8B80A32CB895 8CD5D17D6B254DA1";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_192key_256kek_NonStatic");
      }

      [Test]
      public void Unwrap_192key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "A8F9BC1612C68B3F F6E6F4FBE30E71E4 769C8B80A32CB895 8CD5D17D6B254DA1";
         string pt = "00112233445566778899AABBCCDDEEFF0001020304050607";

         StaticUnwrap(kek, ct, pt, "Unwrap_192key_256kek_Static");
      }

      #endregion

      #region Section 4.6

      [Test]
      public void Wrap_256key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F";
         string ct = "28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326 CBC7F0E71A99F43B FB988B9B7A02DD21";

         NonStaticWrap(kek, pt, ct, "Wrap_256key_256kek_NonStatic");
      }

      [Test]
      public void Wrap_256key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string pt = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F";
         string ct = "28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326 CBC7F0E71A99F43B FB988B9B7A02DD21";

         StaticWrap(kek, pt, ct, "Wrap_256key_256kek_Static");
      }

      [Test]
      public void Unwrap_256key_256kek_NonStatic()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326 CBC7F0E71A99F43B FB988B9B7A02DD21";
         string pt = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F";

         NonStaticUnwrap(kek, ct, pt, "Unwrap_256key_256kek_NonStatic");
      }

      [Test]
      public void Unwrap_256key_256kek_Static()
      {
         string kek = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
         string ct = "28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326 CBC7F0E71A99F43B FB988B9B7A02DD21";
         string pt = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F";

         StaticUnwrap(kek, ct, pt, "Unwrap_256key_256kek_Static");
      }

      #endregion
   }
}
