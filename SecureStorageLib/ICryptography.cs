using System;

namespace SecureStorageLib
{
    /// <summary>
    /// ICryptography: defines cryptographic interface
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

        //byte[] Encrypt(byte[] data, bool ramdom_iv = false, bool append_end = true);

        /// <summary>
        /// Decrypt: decrypt data
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        byte[] Decrypt(byte[] data);
    }
}
