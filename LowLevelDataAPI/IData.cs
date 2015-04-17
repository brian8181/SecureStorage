using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelDataAPI
{
    interface IData
    {
        byte[] Read(long pos, string src_name, long len);
        void Write(byte[] src, long src_pos, string dest_name, long dest_pos, long len);
    }
}
