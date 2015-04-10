using System;
using System.Text;
using System.IO;
using Utility;

namespace ConsolePOCs
{
    class Program
    {
        static void Main(string[] args)
        {
            // gen keys
            RSA.KeyPair keys = RSA.GenerateKeys();
            // save keys

            File.WriteAllBytes("c:\\tmp\\privatekey.xml", ASCIIEncoding.ASCII.GetBytes(keys.PrivateKey));
            File.WriteAllBytes("c:\\tmp\\publickey.xml", ASCIIEncoding.ASCII.GetBytes(keys.PublicKey));
        }
    }
}
