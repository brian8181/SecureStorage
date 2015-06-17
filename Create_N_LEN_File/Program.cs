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
            max = 512;
            StreamWriter sw = new StreamWriter("c:\\tmp\\file2.txt");
    
            for (int i = 0; i < max; ++i) 
            {
                int num = i % 4;
                //Console.WriteLine(num);
                sw.Write(num.ToString());
            }
            
            sw.Flush();
            sw.Close();

            byte[] bts = File.ReadAllBytes("c:\\tmp\\file2.txt");
        }

        public void Write_N_As(int n)
        {

        }
    }
}
