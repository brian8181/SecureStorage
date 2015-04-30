using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAppendFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //int x = 11;
            //int y = 20;
            //int z = y / x;
           
            string src = "c:\\tmp\\infiles\\Desert.jpg";
            string dst = "c:\\tmp\\test_append.jpg";
            string dst2 = "c:\\tmp\\test_append2.jpg";
            File.Delete(dst);
            File.Delete(dst2);
            byte[] data = File.ReadAllBytes(src);

            const int CHUNK = 100;
            int LEN = data.Length;
            int idx = 0;
            
            FileStream fs = new FileStream(dst, FileMode.Create);

            // copy all chuncks
            while ((idx + CHUNK) <= LEN)
            {
                //FileStream fs = new FileStream(dst, FileMode.Create);
                fs.Write(data, idx, CHUNK);
                idx += CHUNK;
                //fs.Close();
            }

            int left_over = (LEN - idx);
            // get last bit
            if (left_over > 0)
            {
                fs.Write(data, idx, left_over);
            }

            fs.Close();
                        
            //CLIENT SIDE
            // open/close each time ...
            idx = 0; // reset index
            while ((idx + CHUNK) <= LEN)
            {
                WriteChunk(dst2, idx, data, CHUNK);
                idx += CHUNK;
            }

            left_over = (LEN - idx);
            if (left_over > 0)
            {
                WriteChunk(dst2, idx, data, left_over);
            }

            

        }

        //SERVER SIDE
        public static void WriteChunk(string dst, int offset, byte[] data, int count)
        {
            using (FileStream fs = new FileStream(dst, FileMode.Create | FileMode.Append))
            {
                fs.Write(data, offset, count);
            }
   
        }

        //public static byte[] GetSHA256(string name)
        //{
            
        //}
    }
}
