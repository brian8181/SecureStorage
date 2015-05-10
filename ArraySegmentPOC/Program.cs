using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArraySegmentPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] a = new byte[10];

            for (int i = 0; i < 10; ++i)
            {
                a[i] = (byte)i;
            }

            ArraySegment<byte> s1 = new ArraySegment<byte>(a, 0, 5);
            ArraySegment<byte> s2 = new ArraySegment<byte>(a, 5, 5);

            IList<byte> l = (IList<byte>)s2;
            foreach (byte b in l)
            {
                Console.WriteLine(b);
            }
            //Foo((byte[])s2);

           
        }

        public void Foo(byte[] a)
        {
        }
    }
}
