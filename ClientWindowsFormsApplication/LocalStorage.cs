using System;
using System.IO;
using SecureStorageLib;


namespace ClientWindowsFormsApplication
{
    class LocalStorage : ISecureStorage
    {
        #region IStorage Members

        public void Lock(string name)
        {
            throw new NotImplementedException();
        }

        public void Unlock(string name)
        {
            throw new NotImplementedException();
        }

        public bool Create(string name, byte[] data, System.IO.FileMode mode = FileMode.Create)
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

        public void CreateEmpty(string name, int len, bool random = false)
        {
            throw new NotImplementedException();
        }

        public void Copy(string src_name, string dst_name)
        {
            throw new NotImplementedException();
        }

        public void Move(string src_name, string dst_name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
