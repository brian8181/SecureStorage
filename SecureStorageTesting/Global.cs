 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageTesting
{
    public static class Global
    {
        private const string PATH = "..\\..\\test\\";

        public static string TestFolder
        {
            get { return PATH; }
        }
    }
}
