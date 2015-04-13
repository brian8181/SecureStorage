﻿using System;
using System.Text;
using System.IO;
using Utility;
using System.Security.Cryptography;

namespace ConsolePOCs
{
    class Program
    {
        static void Main(string[] args)
        {
            //// gen keys
            //QuickRSA.KeyPair keys = QuickRSA.GenerateKeyPair();
            //// save keys
            //File.WriteAllBytes("c:\\tmp\\privatekey2048.xml", ASCIIEncoding.ASCII.GetBytes(keys.PrivateKey));
            //File.WriteAllBytes("c:\\tmp\\publickey2048.xml", ASCIIEncoding.ASCII.GetBytes(keys.PublicKey));

            QuickRSA.CreateKeyPair("c:\\tmp\\keys");
         
            QuickRSA rsa = new QuickRSA();
            rsa.LoadKey(UTF32Encoding.ASCII.GetString(File.ReadAllBytes("c:\\tmp\\privatekey2048.xml")));

            ////NO GO! -- byte[] data = File.ReadAllBytes("C:\\tmp\\infiles\\Desert.jpg");
            byte[] data = ASCIIEncoding.ASCII.GetBytes("Hello World!");
            byte[] enc_data = rsa.Encrypt(data);
            
            byte[] denc_data = rsa.Decrypt(enc_data);
            string s = ASCIIEncoding.ASCII.GetString(denc_data);



            byte[] signed_data = rsa.SignData(enc_data);
            bool v = rsa.VerifyData(enc_data, signed_data);

            /////////////// TEST AES_RSA_HYBRID ///////////////////
            QuickRSA rsa1 = new QuickRSA();
            rsa1.LoadKey(UTF32Encoding.ASCII.GetString(File.ReadAllBytes("c:\\tmp\\privatekey2048.xml")));
            byte[] data3 = File.ReadAllBytes("C:\\tmp\\infiles\\Desert.jpg");
            byte[] rsa_aes_data = rsa1.Encrypt_RSA_AES(data3);
            

            QuickRSA rsa2 = new QuickRSA();
            rsa2.LoadKey(UTF32Encoding.ASCII.GetString(File.ReadAllBytes("c:\\tmp\\privatekey2048.xml")));
            byte[] final = rsa2.Decrypt_RSA_AES(rsa_aes_data);
            //string s2 = ASCIIEncoding.ASCII.GetString(final);
            File.WriteAllBytes("c:\\tmp\\data66.jpg", final);
            


            ////////////////////////////////////////////////////////




            //// testing
            ////byte[] key = new byte[32];
            //byte[] key = CryptoFunctions.GererateKey(32);

            byte[] k2 = CryptoFunctions.GererateKey(32);
            byte[] iv = CryptoFunctions.GererateKey(16);

            ////test2
            byte[] data2 = File.ReadAllBytes("C:\\tmp\\infiles\\Desert.jpg");
            byte[] aes_enc_data = CryptoFunctions.EncryptAES(k2, data2, iv);
            CryptoFunctions.DecryptAES(k2, aes_enc_data, iv);

        }

        private static byte[] GK()
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.GenerateKey();
            return aes.Key;
        }

        private static readonly byte[] Salt =
                                            new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };

        private static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, Salt, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
