using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCloudLib
{
    public class File : ICloudFile
    {

        #region ICloudFile Members

        public string[] GetDirectory(string path)
        {
            throw new NotImplementedException();
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
