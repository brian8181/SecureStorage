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
        /// KeySize: size of the key
        /// </summary>
        int KeySize
        {
            get;
        }
        
        /// <summary>
        /// IVSize: size of initialization vector
        /// </summary>
        //int IVSize
        //{
        //    get;
        //}

        //byte[] GetSecureHash(string data);

        /// <summary>
        /// Encrypt: encrypt data with internal key & appends a random IV
        /// </summary>
        /// <param name="data">decrypted bytes</param>
        /// <returns>ecrypted bytes</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypt: decrypt data with internal key & appended ramdom IV
        /// </summary>
        /// <param name="data">ecrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        byte[] Decrypt(byte[] data);
    }
}
