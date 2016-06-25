using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KeyStorage
{
    public class DeriveKeyFunction
    {
        // The following constants may be changed without breaking existing hashes.
        public const int SALT_BYTE_SIZE = 32;
        public const int HASH_BYTE_SIZE = 32;
        public const int PBKDF2_ITERATIONS = 1000;
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password with iters & salt prepended</returns>
        public static byte[] DeriveKey(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] key = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            //need salt & iterations
            byte[] iter_bytes = BitConverter.GetBytes(PBKDF2_ITERATIONS);
            uint len = (uint)(iter_bytes.Length + 64u);
            byte[] full_key = new byte[len];

            Array.Copy(iter_bytes, 0, full_key, 0, 4);
            Array.Copy(salt, 0, full_key, 4, 32);
            Array.Copy(key, 0, full_key, 36, 32);
            
            return full_key;
        }
        
        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        public static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
