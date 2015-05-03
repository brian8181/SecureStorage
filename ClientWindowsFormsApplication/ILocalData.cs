using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    // function on local sources, bridge between local an remote sources
    interface ILocalData
    {
        //string GetCloudName(string name);
        void Initialize(string input_dir, string output_dir);
    }
}
