 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageTesting
{
    public static class Global
    {
        public const string STRING_TEST_DATA = "you and I have been through that and it's not our fate";
        private const string PATH = "..\\..\\test\\";

        public static string TestFolder
        {
            get { return PATH; }
        }

        
    }
}
