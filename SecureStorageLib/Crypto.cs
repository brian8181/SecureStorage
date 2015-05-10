using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    //BKP is this worth it?
    public abstract class Crypto : ICrypto
    {
        protected byte[] key = null;
        protected byte[] iv = null;
        protected readonly int KEY_SIZE = 0;
        protected readonly int IV_SIZE = 0;
        
        public Crypto(byte[] iv, byte[] key)
        {
            this.key = key;
            this.iv = iv;
        }

        #region ICrypto Members

        public byte[] Key
        {
            get { return key; }
        }

        public byte[] IV
        {
            get { return iv; }
        }

        public abstract int KeySize
        {
            get;
        }

        public abstract int IVSize
        {
            get;
        }

        public abstract byte[] Encrypt(byte[] data);
        public abstract byte[] Decrypt(byte[] data);
        public abstract byte[] SHA256(byte[] data);
        public abstract byte[] HMACSHA256(string data);
        public abstract string FromBytesToHex(byte[] array);
        
        #endregion
    }
}
