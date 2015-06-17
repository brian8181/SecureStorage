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
        [Test]
        public void Sha1()
        {
            string text = "abc";
            byte[] data = Encoding.ASCII.GetBytes(text);
            byte[] hash = HashingAlgorithms.Hash.Sha1(data);
                       
            
            // assert false
            //Assert.IsTrue(false);
            Assert.Inconclusive();
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
    }
}
