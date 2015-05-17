using System;

namespace SecureStorageLib
{
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

        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
