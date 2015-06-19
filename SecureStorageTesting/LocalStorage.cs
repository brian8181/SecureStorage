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
        private string working_dir = Properties.Settings.Default.PATH;

        public LocalStorage(string path)
        {
        }

        #region IStorage Members

        public void Create(string name, byte[] data, System.IO.FileMode mode = FileMode.Create)
        {
            using (FileStream fs = new FileStream(working_dir + name, FileMode.Create | FileMode.Append))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public void CreateEmpty(string name, int len, bool random = false)
        {
            byte[] data = null;
            if (random)
            {
                data = SecureStorageUtility.GererateKey(len);
                File.WriteAllBytes(working_dir + name, data);
            }
            else
            {
                data = new byte[len];
                // are they not already zero?
                Array.Clear(data, 0, len);
            }
        }

        public byte[] Read(string name, int offset, int len)
        {
            if (len <= 0)
                len = (int)GetLength(name);

            using (FileStream fs = new FileStream(working_dir + name, FileMode.Open))
            {
                byte[] data = new byte[len];
                fs.Seek(offset, SeekOrigin.Begin);
                long read = fs.Read(data, 0, len);
                if (read != len)
                    return null;
                return data;
            }
        }

        public void Delete(string name)
        {
            File.Delete(working_dir + name);
        }

        public int GetLength(string name)
        {
            FileInfo fi = new FileInfo(working_dir + name);
            return (int)fi.Length;
        }

        public bool Exists(string name)
        {
            FileInfo fi = new FileInfo(working_dir + name);
            return fi.Exists;
        }

        public void DeleteAll() 
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            foreach (FileSystemInfo info in fs_infos)
            {
                info.Delete();
            }
        }

        public void Copy(string src_name, string dst_name)
        {
            File.Copy(src_name, dst_name);
        }

        public void Move(string src_name, string dst_name)
        {
            File.Move(src_name, dst_name);
        }

        #endregion
    }
}
