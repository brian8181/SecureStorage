using System;

namespace SecureStorageLib
{
    /// <summary>
    /// defines cryptographic inteface
    /// </summary>
    public interface ICrypto
    {
        byte[] Key
        {
            get;
        }

        byte[] IV
        {
            get;
        }

        int KeySize
        {
            get;
        }

        int IVSize
        {
            get;
        }

        /// <summary>
        /// encrypt data
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        byte[] Encrypt(byte[] data);
        /// <summary>
        /// decrypt data
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        byte[] Decrypt(byte[] data);
    }
}
