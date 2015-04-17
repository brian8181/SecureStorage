using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesEncryptDirectory
{
    interface ICloudFile
    {
        string[] GetDirectory(string path);
        bool ValidateSig(byte[] file, byte[] sig);
        void CreateDirectory(string path);
        // create file
        void Create(string path, byte[] data);
        // get file contents
        byte[] Read(string path);
        // use copy & delete to update a file
        void Update(string path, byte[] data);
        // removes file a loction
        void Delete(string path);
        // create copy of file to another location
        void Copy(string src, string dst);
    }
}
