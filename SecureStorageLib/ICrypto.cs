using System;

namespace SecureStorageLib
{
    /// <summary>
    /// defines cryptographic inteface
    /// </summary>
    public interface ICrypto
    {

        /// <summary>
        /// key
        /// </summary>
        byte[] Key
        {
            get;
        }
        
        /// <summary>
        /// initialization vector
        /// </summary>
        byte[] IV
        {
            get;
        }
        
        /// <summary>
        /// size of the key
        /// </summary>
        int KeySize
        {
            get;
        }
        
        /// <summary>
        /// size of initialization vector
        /// </summary>
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
