using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;

namespace AesEncryptDirectory
{
    public class FilesXML
    {
        XmlDocument doc = null;
        byte[] key = null;
        byte[] iv = null;

        public FilesXML(string path)
        {
           Load(path);
        }

        public void Load(string path)
        {
            byte[] bts = File.ReadAllBytes(path);
            byte[] data = CryptoFunctions.DecryptAES(key, bts, iv);
            
            doc = new XmlDocument();
            doc.LoadXml(ASCIIEncoding.ASCII.GetString(data));
        }

        public string[] GetFiles()
        {
            return null;
        }

        public string[] GetDirectories()
        {
            return null;
        }
    }
}
