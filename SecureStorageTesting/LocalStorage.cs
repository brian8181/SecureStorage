using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureStorageLib;

namespace SecureStorageTesting
{
    public class LocalStorage : IStorage
    {
        public LocalStorage(string path)
        {
        }

        #region IStorage Members

        public void Create(string name, byte[] data, System.IO.FileMode mode = FileMode.Create)
        {
            throw new NotImplementedException();
        }

        public void CreateEmpty(string name, int len, bool random = false)
        {
            throw new NotImplementedException();
        }

        public byte[] Read(string name, int offset, int len)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public int GetLength(string name)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
