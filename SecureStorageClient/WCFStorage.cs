using System;
using System.IO;
using SecureStorageLib;

namespace SecureStorageClient
{
    /// <summary>
    /// implements the IStorage interface
    /// </summary>
    public class WCFStorage : IStorage
    {
        private ServiceReference.IStorageService cloud = new ServiceReference.StorageServiceClient();
        
        #region IStorage Members
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
            cloud.CreateEmpty(name, len, random);
        }
        #endregion
    }
}
