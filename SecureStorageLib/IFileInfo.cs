using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStorageLib
{
    //public interface IFileSysObject
    //{
    //    string Name { get; }
    //}

    public interface IFileInfo //: IFileSysObject
    {
        string Name { get; }
        string Signature { get; }
        string Created { get; }
        string Modified { get; }
    }

    //public class FileDesc
    //{
    //    string name;
    //    string signature;
    //    byte[] data;
    //    DateTime created;
    //    DateTime modified;
    //}

    //public interface IDirectoryInfo 
}
