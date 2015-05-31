using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HashingAlgorithmsTest
{
    [TestFixture]
    public class HashTest
    {
        [Test]
        public void Sha1()
        {
            string text = "abc";
            byte[] data = Encoding.ASCII.GetBytes(text);
            byte[] hash = HashingAlgorithms.Hash.Sha1(data);
            
            // assert false
            Assert.IsTrue(false);
        }
    }
}
