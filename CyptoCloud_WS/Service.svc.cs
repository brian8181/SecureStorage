using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void CreateEmpty(string name, int len, bool random = false)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, byte[] data)
        {
            File.WriteAllBytes(working_dir + name, data);
        }

        public byte[] Read(string name)
        {
            return File.ReadAllBytes(working_dir + name);
        }

        public void Write(string name, int start, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            File.Delete(working_dir + name);
        }

        public void Append(string name, byte[] data)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}
