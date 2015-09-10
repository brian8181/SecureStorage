using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStorage
{
    public interface IKeyStorage
    {
        string Name { get; }

        KeyStore CreateStore(string store_name, string path);
        KeyStore LoadStore(string store_name);
        //void AddKey(string store_name, string store_password, string key_name, byte[] key); 
       
    }
}
