using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashingAlgorithms
{
    public class Hash
    {
        public const uint SHA1_BIT_SIZE = 512;
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

            //padding to 512 bit block length
            uint num_pad_bytes = (uint)(data.Length % SHA1_BLOCK_SIZE);
            //byte bit = 0x80;
            //ushort len_bytes = 0;


            return null;
        }

        public static byte[] PadMessage(byte[] msg)
        {
            int len = msg.Length;
            len += (64 - (msg.Length % 64));
            byte[] buffer = new byte[len];
            Array.Copy(msg, buffer, msg.Length);
            byte[] len_bytes = BitConverter.GetBytes((long)(msg.Length * 8));
            // to big endian
            if(BitConverter.IsLittleEndian)
                Array.Reverse(len_bytes);
            // copy len bytes
            Array.Copy(len_bytes, 0, buffer, len - 8, 8);
            // copy 0x80 "10000000" to end of message
            buffer[msg.Length] = 0x80; // add a one bit to end of message
            return buffer; 
        }

        static uint[] W = null;
        public static uint[] GetWs(byte[] block)
        {
            if (block.Length != 512)
            {
                throw new Exception("block not 512");
            }

            int current_i = 0;
            // divide block into 16 words
            uint[] W = new uint[16];
            for (int i = 0; i < 4; ++i)
            {
                if ((i % 4) != 0)
                {
                    byte[] next_word = new byte[4];
                    W[current_i++] = BitConverter.ToUInt32(block, i - 3);
                }
            }

            return W;

        }

        public static void ProcessMessageBlock(byte[] block)
        {

            W = GetWs(block);

        }

        public static uint F0(uint B, uint C, uint D)
        {
            // (B AND C) OR ((NOT B) AND D)

            uint z = ( B & C ) | ( (~B) & D );
            return z;
        }

        public static void F20(uint B, uint C, uint D)
        {
            // B XOR C XOR D
        }

        public static void F40(uint B, uint C, uint D)
        {
            // (B AND C) OR (B AND D) OR (C AND D)
        }

        public static void F60(uint B, uint C, uint D)
        {
            // B XOR C XOR D
        }

        public static uint CircularShift(uint x, int n)
        {
            uint z = (x << n) | (x >> (32 - n));
            return z;
        }

        public static string FromBytesToHex(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
