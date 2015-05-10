using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    /// <summary>
    /// create a file split into n[1] block with random n[2] offset where offset does not increase total blocks
    /// </summary>
    public class SecureData
    {
        private readonly int BLOCK_SIZE = 0;
       // private readonly int OFFSET = 0;

        public SecureData(int block_size, byte[] data)
        {
            BLOCK_SIZE = block_size;
        }

        public byte[] Data
        {
            get
            {
                return null;
            }
        }

    }
}
