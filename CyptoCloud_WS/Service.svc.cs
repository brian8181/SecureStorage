using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using Utility;

namespace CyptoCloud_WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        private string working_dir = "c:\\tmp\\svr\\";

        #region ILowLevel Members

        public void Create(string name, byte[] data)
        {
            File.WriteAllBytes(working_dir + name, data);
        }

        public void CreateEmpty(string name, int len, bool random = false)
        {
            byte[] data = null;
            if (random)
            {
                data = Utility.CryptoFunctions.GenerateRandomBytes(len);
                File.WriteAllBytes(working_dir + name, data);
            }
            else
            {
                data = new byte[len];
                // are they not already zero?
                Array.Clear(data, 0, len);
            }
        }

        public byte[] Read(string name)
        {
            return File.ReadAllBytes(working_dir + name);
        }
        
        public void Delete(string name)
        {
            File.Delete(working_dir + name);
        }

        public void Write(string name, int start, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Append(string name, byte[] data)
        {
            //TODO
            FileStream fs = new FileStream(working_dir + name, FileMode.Append);
        }

        public void Move(string src, string dst)
        {
            throw new NotImplementedException();
        }

        public void Copy(string src, string dst)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadData(string name, int start, int lenght)
        {
            throw new NotImplementedException();
        }

        public void MoveData(string src, int src_idx, string dst, int dst_idx, int len)
        {
            throw new NotImplementedException();
        }

        public void CopyData(string src, int src_idx, string dst, int dst_idx, int len)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            return di.GetFileSystemInfos().Length;
        }

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

        //return subset of file list
        public string[] GetAllNames()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            return GetNames(0, fs_infos.Length);
        }

        public void DeleteAll()
        {
            DirectoryInfo di = new DirectoryInfo(working_dir);
            FileSystemInfo[] fs_infos = di.GetFileSystemInfos();
            foreach(FileSystemInfo info in fs_infos)
            {
                info.Delete();
            }
        }
        #endregion
    }
}
