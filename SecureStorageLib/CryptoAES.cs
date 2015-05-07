using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    class CryptoAES : ICrypto
    {
        #region ICrypto Members

        public byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
        {
            throw new NotImplementedException();
        }

        public string FromBytesToHex(byte[] array)
        {
            throw new NotImplementedException();
        }

        public byte[] SHA256(byte[] data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
