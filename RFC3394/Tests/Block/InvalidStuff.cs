
using System;
using NUnit.Framework;

namespace RFC3394.Tests
{
   [TestFixture]
   public class Block_InvalidStuff
   {
      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Constructor_NullBytes()
      {
         byte[] test = null;
         Block b = new Block(test);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_InvalidSize1()
      {
         byte[] test = new byte[0];
         Block b = new Block(test);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_InvalidSize2()
      {
         byte[] test = new byte[7];
         Block b = new Block(test);
      }

      [Test]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Constructor_NegativeIndex()
      {
         byte[] test = new byte[8];
         Block b = new Block(test, -1);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_InsufficientBytes()
      {
         byte[] test = new byte[16];
         Block b = new Block(test, 9);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Concat_NullRight()
      {
         byte[] test = new byte[8];
         Block b = new Block(test);
         b.Concat(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void BytesToBlocks_NullBytes()
      {
         Block.BytesToBlocks(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void BytesToBlocks_InvalidSize1()
      {
         byte[] test = new byte[7];
         Block.BytesToBlocks(test);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void BytesToBlocks_InvalidSize2()
      {
         byte[] test = new byte[9];
         Block.BytesToBlocks(test);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void BlocksToBytes_NullBlocks()
      {
         Block.BlocksToBytes(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Xor_NullBlock()
      {
         Block.Xor(null, 0);
      }
   }
}
