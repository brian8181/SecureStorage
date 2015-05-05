using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    /// <summary>
    /// high level interface 
    /// </summary>
    public interface IStorage
    {
        // named objects
        void Lock(string name);
        void Unlock(string name);
        bool Create(string name, byte[] data, FileMode mode = FileMode.Create);
        void CreateEmpty(string name, int len, bool random = false);
        byte[] Read(string name, int offset, int len);
        void Delete(string name);
        int GetLength(string name);
        bool Exists(string name);
        void DeleteAll();
    }
}
