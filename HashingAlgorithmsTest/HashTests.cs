using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashingAlgorithms;
using NUnit.Framework;

namespace HashingAlgorithmsTest
{
    [TestFixture]
    public class HashTests
    {
        [TestCase("abc", "a9993e364706816aba3e25717850c26c9cd0d89d")]
        public void Sha1(string data, string expected)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] output_bytes = HashingAlgorithms.Hash.Sha1(buffer);
            string actual = HashingAlgorithms.Hash.FromBytesToHex(output_bytes);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Load_N_Len_File()
        {
            byte[] data = File.ReadAllBytes("..\\..\\test\\300_bytes_file.txt");
            Assert.AreEqual(300, data.Length);

            data = File.ReadAllBytes("..\\..\\test\\900_bytes_file.txt");
            Assert.AreEqual(900, data.Length);

            data = File.ReadAllBytes("..\\..\\test\\512_bytes_file.txt");
            Assert.AreEqual(512, data.Length);
        }

        [TestCase(new byte[] { 0 })]
        public void Sha1Padding(byte[] data)
        {
            // 01100001 01100010 01100011 01100100 01100101 1 = 0x6162636465

            // 01100001 01100010 01100011 01100100 01100101


            // 01100001 01100010 01100011 01100100 = 0x61626364
            uint i1 = 0x61626364;
            
            // 01100101 00000000 00000000 00000000 = 0x65000000
            // 01100101 10000000 00000000 00000000 = 0x65800000

            File.ReadAllBytes("..\\..\\test\\300_bytes_file.txt");
        }

        [Test]
        public void Sha1Padding2()
        {
            byte[] Long_data = BitConverter.GetBytes(0x61626364);
            byte[] data = new byte[5];
            Array.Copy(Long_data, data, 4);
            data[4] = 0x65;
            byte[] output = Hash.PadMessage(data);

            // test output

            Assert.Inconclusive("work in progress");
        }

        [TestCase("ABCD", 0x20UL)]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXZY0123456789", 0x120UL)]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXZY0123456789", 0x120UL)]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXZYabcdefghijklmnopqrstuvwxyz0123", 0x01c0UL)]
        public void Sha1Padding(string data, ulong expected_len)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] padded_buffer = Hash.PadMessage(buffer);
            Assert.IsTrue(padded_buffer.Length == 64);
            
            byte[] len_bytes = new byte[8];

            Array.Copy(padded_buffer, padded_buffer.Length - 8, len_bytes, 0, 8);
            Array.Reverse(len_bytes);

            ulong actual_len = BitConverter.ToUInt64(len_bytes, 0);
            Assert.AreEqual(expected_len, actual_len);
        }
    }
}
