using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace FileLocking_ProofOfConcept
{
    class Program
    {
        static string lock_file = "c:\\tmp\\file_lock_testing\\lock";
        static string read_file = "c:\\tmp\\file_lock_testing\\read";


        static void Main(string[] args)
        {
            
            FileInfo fi = new FileInfo(lock_file);
            try
            {
                fi.OpenRead();
                //fi.OpenWrite();
            }
            catch (Exception e)
            {
                //file lock not obtained
                Console.WriteLine(e.Message);
            }

            Thread t = new Thread(new ThreadStart(DeleteThread));
            //t.Start();
           

        }

        static void DeleteThread()
        {
            File.Delete(lock_file);
        }


    }
}
