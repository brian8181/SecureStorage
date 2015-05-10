using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    public class RC2 : Crypto
    {
        public RC2(byte[] iv, byte[] key)
            : base(iv, key)
        {
        }


        public override int IVSize
        {
            //bkp MSDN says: iv size is block size divided 8
            get { return 0; } //BKP
        }

        //public override int BlockSize
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public override int KeySize
        {
            get { throw new NotImplementedException(); }
        }

        public override byte[] Encrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Decrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] SHA256(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] HMACSHA256(string data)
        {
            throw new NotImplementedException();
        }

        public override string FromBytesToHex(byte[] array)
        {
            throw new NotImplementedException();
        }

      
    }
}
