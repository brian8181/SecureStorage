using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AesEncryptDirectory
{
    public class CloudFile : ICloudFile
    {
        public CloudFile(string path)
        {

        }

        #region ICloudFile Members

        public string[] GetDirectory(string path)
        {
            byte[] bts = File.ReadAllBytes(path);
            return null;
        }

        public bool ValidateSig(byte[] file, byte[] sig)
        {
            throw new NotImplementedException();
        }

        public void CreateDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public void Create(string path, byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Read(string path)
        {
            throw new NotImplementedException();
        }

        public void Update(string path, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public void Copy(string src, string dst)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
