using System;
using System.IO;
using SecureStorageLib;

namespace ClientWindowsFormsApplication
{
    public class WCFStorage : IStorage
    {
        private ServiceReference.IService cloud = new ServiceReference.ServiceClient();
        
        #region IStorage Members

        public void Lock(string name)
        {
            throw new NotImplementedException();
        }

        public void Unlock(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, byte[] data, System.IO.FileMode mode = FileMode.Create)
        {
            cloud.CreateAppend(name, data);
        }

        public byte[] Read(string name, int offset, int len)
        {
            return cloud.Read(name, offset, len);
        }

        public void Delete(string name)
        {
            cloud.Delete(name);
        }

        public int GetLength(string name)
        {
            return (int)cloud.GetLength(name);
        }

        public bool Exists(string name)
        {
            return cloud.Exists(name);
        }

        public void DeleteAll()
        {
            cloud.DeleteAll();
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
