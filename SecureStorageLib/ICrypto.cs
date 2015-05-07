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

        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        byte[] SHA256(byte[] data);
        byte[] HMACSHA256(string name);
        string FromBytesToHex(byte[] array);
    }
}
