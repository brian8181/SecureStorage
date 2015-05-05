using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Create_N_LEN_File
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s =  "ABC";
            int max = (int)Math.Floor(Math.Pow(2, 16) / 2); 
            
            StreamWriter sw = new StreamWriter("c:\\tmp\\file.txt");
    
            for (int i = 0; i < max; ++i) 
            {
                int num = i % 4;
                //Console.WriteLine(num);
                sw.Write(num.ToString());
            }

        }
    }
}
