using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertSigningTest
{
    class Program
    {
        static byte[] Sign(string text, string certSubject)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            RSACryptoServiceProvider csp = null;
            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.Subject.Contains(certSubject))
                {
                    // We found it. 
                    // Get its associated CSP and private key
                    csp = (RSACryptoServiceProvider)cert.PrivateKey;
                }
            }
            if (csp == null)
            {
                throw new Exception("No valid cert was found");
            }

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = sha1.ComputeHash(data);

            byte[] ret = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            foreach(byte b in ret)
                Console.Write(b.ToString("x"));
            Console.WriteLine();
            // Sign the hash
            return ret;        
        }

        static bool Verify(string text, byte[] signature, string certPath)
        {
            // Load the certificate we'll use to verify the signature from a file 
            X509Certificate2 cert = new X509Certificate2(certPath);
            // Note: 
            // If we want to use the client cert in an ASP.NET app, we may use something like this instead:
            // X509Certificate2 cert = new X509Certificate2(Request.ClientCertificate.Certificate);

            // Get its associated CSP and public key
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = sha1.ComputeHash(data);

            // Verify the signature with the hash
            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }

        static void Main(string[] args)
        {
            // Usage sample
            try
            {
                string text = "testing, testing, testing";
                // Sign text
                byte[] signature = Sign(text, "CN=Test");

                string cert = Utility.Globals.ProcessDirectory() + @"\..\..\Test.cer";
                // Verify signature. Test.cer corresponds to "cn=Test"
                if (Verify(text, signature, cert))
                {
                    Console.WriteLine("Signature verified");
                }
                else
                {
                    Console.WriteLine("ERROR: Signature not valid!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
            Console.ReadKey();
        }
    }
    
}
