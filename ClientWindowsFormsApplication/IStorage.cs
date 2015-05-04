using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    /// <summary>
    /// high level interface 
    /// </summary>
    interface IStorage
    {
        void Lock(string name);
        void Unlock(string name);
        void Create(string name);
        void Read(string name);
        void Delete(string name);
    }
}
