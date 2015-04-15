using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherProcess
{
    class Program
    {
        static string lock_file = "c:\\tmp\\file_lock_testing\\lock";
        static void Main(string[] args)
        {
            try
            {
                File.OpenRead(lock_file);
                File.Delete(lock_file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
