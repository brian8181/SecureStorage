using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public class RSA /*RASWrapper*/
    {
        public class KeyPair
        {
            public KeyPair(string priKey, string pubKey)
            {
                PublicKey = pubKey;
                PrivateKey = priKey;
            }

            string publickey;

            public string PublicKey
            {
                get { return publickey; }
                set { publickey = value; }
            }
            string privatekey;

            public string PrivateKey
            {
                get { return privatekey; }
                set { privatekey = value; }
            }
        }

        private KeyPair keys;

        public RSA()
        {
        }


        public RSA(string keys_path)
        {
            string keys = UTF8Encoding.ASCII.GetString( File.ReadAllBytes(keys_path) );
            //this.keys = keys;
        }
      
        public static KeyPair GenerateKeys()
        {
            // Create a new key pair on target CSP
            CspParameters cspParams = new CspParameters();
            cspParams.ProviderType = 1; // PROV_RSA_FULL 
            //cspParams.ProviderName; // CSP name
            cspParams.Flags = CspProviderFlags.UseArchivableKey;
            cspParams.KeyNumber = (int)KeyNumber.Exchange;
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            // Export public key
            string pri = rsaProvider.ToXmlString(true);
            // Export private/public key pair 
            string pub = rsaProvider.ToXmlString(false);

            //string s = RSA.ExportPublicKeyToPEMFormat(rsaProvider);

            return new KeyPair(pri, pub);
        }

        public byte[] EncryptHybrid(byte[] data, string pubkey, bool DoOAEPPadding)
        {
            // gen aes key
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] key = aes.Key;
            byte[] iv = aes.IV;
            
            // encrypt use aes

            // Get an encryptor.
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

            // EncryptRC2 the data as an array of encrypted bytes in memory.
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            // Write all data to the crypto stream and flush it.
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            byte[] enc_data = ms.ToArray();

            /////////////////////////////////////////////////////

            // combine key and iv
            byte[] aes_key = new byte[key.Length + iv.Length];
            Array.Copy(key, aes_key, key.Length);
            Array.Copy(iv, 0, aes_key, key.Length + 1, iv.Length);

            // encrypt key using qrsa public key
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //qrsa.FromXmlString("");
            byte[] enc_key = rsa.Encrypt(aes_key, true);

            
            // append encrypted key to encypted data
            int klen = enc_key.Length;
            int dlen = enc_data.Length;
            byte[] ret_data = new byte[dlen + klen];
            Array.Copy(enc_key, ret_data, klen);
            Array.Copy(enc_data, 0, ret_data, klen + 1, dlen);
                       
            return null;

            // 2 dycrypt
            // use private qrsa key to get aes key
            // dycrypt data using aes key

        }
       
        public byte[] Encrypt(byte[] DataToEncrypt, string pubkey, bool DoOAEPPadding)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass 
                // 13 to specify a DSA container or 1 to specify 
                // an RSA container.  The default is 1.
                //CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                //cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider. 
                //Pass the CspParameters class to use the key  
                //from the key in the container.
                //RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                //if (keys == string.Empty)
                {
                    //BKP
                    RSAalg.FromXmlString(pubkey);
                }

                //Encrypt the passed byte array and specify OAEP padding.   
                //OAEP padding is only available on Microsoft Windows XP or 
                //later.   
                return RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        public byte[] Decrypt(byte[] DataToDecrypt)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                if (keys != null)
                {
                    RSA.FromXmlString(keys.PrivateKey);
                }
                //else
                //{
                //    RSA.FromXmlString(privatekey);
                //}

                return null;
            }

        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs 
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }



        public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  
                // key from RSAParameters.  
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(Key);

                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider 
                // to specify the use of SHA1 for hashing. 
                return RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(Key);

                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider 
                // to specify the use of SHA1 for hashing. 
                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public static String ExportPublicKeyToPEMFormat(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);

                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");

                return outputStream.ToString();

            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }
    }
}
