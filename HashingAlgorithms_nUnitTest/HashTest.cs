using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace HashingAlgorithms_nUnitTest
{
    [TestFixture]
    public class HashTest
    {
        public void Sha1Padding()
        {
            // 01100001 01100010 01100011 01100100 01100101 1 = 0x6162636465

            // 01100001 01100010 01100011 01100100 01100101


            // 01100001 01100010 01100011 01100100 = 0x61626364
            uint i1 = 0x61626364;


            // 01100101 00000000 00000000 00000000 = 0x65000000
            // 01100101 10000000 00000000 00000000 = 0x65800000
            
        }
    }
}
