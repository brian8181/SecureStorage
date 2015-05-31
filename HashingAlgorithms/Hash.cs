using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashingAlgorithms
{
    public class Hash
    {
        public const uint SHA1_BLOCK_SIZE = 64;

        public static byte[] Sha1(byte[] data)
        {
            uint[] H = new uint[] { 
                0x67452301, 
                0xEFCDAB89, 
                0x98BADCFE, 
                0x10325476, 
                0xC3D2E1F0 
            };

            uint[] K = new uint[] 
            { 
                0x5A827999,
                0x6ED9EBA1,
                0x8F1BBCDC,
                0xCA62C1D6
                             
            };

            //padding to 5121 bit block length
            uint num_pad_bytes = (uint)(data.Length % SHA1_BLOCK_SIZE);
            byte bit = 0x80;
            ushort len_bytes = 0;


            return null;
        }
    }
}
