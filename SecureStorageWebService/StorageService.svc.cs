using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SecureStorageLib;

namespace SecureStorageWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class StorageService : IStorageService
    {
        private string working_dir = Properties.Settings.Default.PATH;

        #region IData Members

        public void CreateEmpty(string name, int len, bool random = false)
        {
            byte[] data = null;
            if (random)
            {
                data = SecureStorageUtility.GenerateRandomBytes(len);
                File.WriteAllBytes(working_dir + name, data);
            }
            else
            {
                data = new byte[len];
                // are they not already zero?
                Array.Clear(data, 0, len);
            }
        }

        /// <summary>
        /// deletes object name
        /// </summary>
        /// <param name="name"></param>
        public void Delete(string name)
        {
            File.Delete(working_dir + name);
        }

        /// <summary>
        /// gets object exsits by name
        /// </summary>
        /// <param name="name">the object name</param>
        /// <returns>true if exists</returns>
        public bool Exists(string name)
        {
            FileInfo fi = new FileInfo(working_dir + name);
            return fi.Exists;
        }

        /// <summary>
        /// write date to end of file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public bool CreateAppend(string name, byte[] data)
        {
            using (FileStream fs = new FileStream(working_dir + name, FileMode.Create | FileMode.Append))
            {
                fs.Write(data, 0, data.Length);
            }
            return true;
        }

        /// <summary>
        /// write date to end of file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public bool CreateReplace(string name, byte[] data)
        {
            using (FileStream fs = new FileStream(working_dir + name, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
            return true;
        }

        /// <summary>
        /// read file @ offset for len bytes
        /// </summary>
        /// <param name="name">file by name</param>
        /// <param name="offset">offset to read into file</param>
        /// <param name="len">len of data to read, 0 read all</param>
        /// <returns>the data read</returns>
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

        /// <summary>
        /// get count of obejects
        /// </summary>
        /// <returns>inta a count</returns>
        public int GetCount()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            return di.GetFileSystemInfos().Length;
        }

        /// <summary>
        /// gets the length/ size of object
        /// </summary>
        /// <param name="name">objects name</param>
        /// <returns>long as length / size</returns>
        public long GetLength(string name)
        {
            FileInfo fi = new FileInfo(working_dir + name);
            return fi.Length;
        }

        /// <summary>
        /// gets all names in a directory (files/dirs)
        /// </summary>
        /// <returns>arrary of strings</returns>
        public string[] GetAllNames()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            return GetNames(0, fs_infos.Length);
        }

        /// <summary>
        /// return subset of filename
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string[] GetNames(int idx, int len)
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            string[] names = new string[len];
            for (int i = idx; i < len; ++i)
            {
                names[i] = fs_infos[i].Name;
            }

            return names;
        }

        /// <summary>
        /// delete all files
        /// </summary>
        public void DeleteAll()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            foreach (FileSystemInfo info in fs_infos)
            {
                info.Delete();
            }
        }

        /// <summary>
        /// get sha256 of file data
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] SHA256(string name)
        {
            byte[] data = File.ReadAllBytes(working_dir + name);
            //BKP TODO return SecureStorageUtility.SHA256(data);
            return null;
        }
        #endregion
    }
}
