
using System;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class Block_ReverseBytes
   {
      [Test]
      public void EmptyArray()
      {
         byte[] bytes = new byte[0];
         Block.ReverseBytes(bytes);
      }

      [Test]
      public void OneByte()
      {
         byte[] bytes = new byte[] { 0x01 };
         Block.ReverseBytes(bytes);

         Assert.AreEqual(0x01, bytes[0], "OneByte");
      }

      [Test]
      public void TwoBytes()
      {
         byte[] bytes = new byte[] { 0x01, 0x02 };
         Block.ReverseBytes(bytes);

         Assert.AreEqual(0x02, bytes[0], "TwoBytes1");
         Assert.AreEqual(0x01, bytes[1], "TwoBytes2");
      }

      [Test]
      public void ThreeBytes()
      {
         byte[] bytes = new byte[] { 0x01, 0x02, 0x03 };
         Block.ReverseBytes(bytes);

         Assert.AreEqual(0x03, bytes[0], "ThreeBytes1");
         Assert.AreEqual(0x02, bytes[1], "ThreeBytes2");
         Assert.AreEqual(0x01, bytes[2], "ThreeBytes3");
      }

      [Test]
      public void FourBytes()
      {
         byte[] bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
         Block.ReverseBytes(bytes);

         Assert.AreEqual(0x04, bytes[0], "FourBytes1");
         Assert.AreEqual(0x03, bytes[1], "FourBytes2");
         Assert.AreEqual(0x02, bytes[2], "FourBytes3");
         Assert.AreEqual(0x01, bytes[3], "FourBytes4");
      }
   }
}
