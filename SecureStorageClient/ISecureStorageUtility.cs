using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageClient
{
    interface ISecureStorageUtility
    {
        void LoadKey(string path, int key_size, int iv_size, out byte[] key, out byte[] iv);
    }
}
