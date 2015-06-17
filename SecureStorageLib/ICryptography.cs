using System;

namespace SecureStorageLib
{
    /// <summary>
    /// ICryptography: defines cryptographic inteface
    /// </summary>
    public interface ICryptography
    {
        /// <summary>
        /// key
        /// </summary>
        byte[] Key
        {
            get;
        }
        
        /// <summary>
        /// IV: initialization vector
        /// </summary>
        byte[] IV
        {
            get;
        }
        
        /// <summary>
        /// KeySize: size of the key
        /// </summary>
        int KeySize
        {
            get;
        }
        
        /// <summary>
        /// IVSize: size of initialization vector
        /// </summary>
        int IVSize
        {
            get;
        }

        /// <summary>
        /// Encrypt: encrypt data
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypt: decrypt data
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        byte[] Decrypt(byte[] data);
    }
}
