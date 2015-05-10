using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpanPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            int ticks_per_ms = 1000000 / 100;
            int ticks_per_second = ticks_per_ms * 1000;

            DateTime start_time = DateTime.Now;
            TimeSpan ts = new TimeSpan(ticks_per_second * 120);
            DateTime end_time = start_time + ts;

            DateTime now = DateTime.Now;

            while (now < end_time)
            {
                now = DateTime.Now;
            }
            
            Console.WriteLine(now.Ticks);
            Console.Read();
        }
    }
}
