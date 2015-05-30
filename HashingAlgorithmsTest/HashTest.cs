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
        public void Sha1()
        {
            string sha1 = "";
            byte[] hash = HashingAlgorithms.Hash.Sha1(null);


        }
    }
}
