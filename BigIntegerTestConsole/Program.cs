using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BigIntegerTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //QuickRSA rsa = new QuickRSA("C:\\tmp\\384\\pri.key");
            //rsa.LoadKey("C:\\tmp\\384\\pri.key");


            byte[] d = new byte[2];
            d[0] = 0xff;
            d[1] = 0x00;
            BigInteger b1 = new BigInteger(d);
            BigInteger b2 = new BigInteger(2);

            BigInteger b3 = BigInteger.Add(b1, b2);

            byte[] bts = b3.ToByteArray();

            ushort s = BitConverter.ToUInt16(bts, 0);

            Console.WriteLine(s);

            // wOadP/0FrR+s0va1C6HXUOtRyUaOGC0k9Rtk9YihAc9fkEss6LfaH5VZNbAhCavy
            // sz0Y+OQYdAPGUTXPRnUMttDPC75xdup/j6AjG7mzAQPAdkq19NaGyTCEr7UxFeXR

            byte[] pa = Convert.FromBase64String(@"3Bu6B/X95jRhENRmjWgZKzS1AlKjW3Ib");
            byte[] qa = Convert.FromBase64String(@"0HdLhYxKDMnmypECTY5Q6/cHZPr0t6aD");
            BigInteger p = new BigInteger(pa);
            BigInteger q = new BigInteger(qa);

            BigInteger n = BigInteger.Multiply(p, q);

            byte[] na = n.ToByteArray();
            string str = Convert.ToBase64String(na);
            Console.WriteLine(str);
            Console.Read();
        }
    }
}
