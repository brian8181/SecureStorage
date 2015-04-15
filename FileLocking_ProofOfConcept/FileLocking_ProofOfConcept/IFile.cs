using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLocking_ProofOfConcept
{
    interface IFileLock
    {
        /// <summary>
        /// checkes if file is locked
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool isLocked(string path);
        /// <summary>
        /// gets file lock
        /// </summary>
        /// <returns></returns>
        bool GetLock();
        /// <summary>
        /// release file lock
        /// </summary>
        /// <returns></returns>
        bool ReleaseLock();
        /// <summary>
        /// force unlock
        /// </summary>
        /// <param name="path"></param>
        void ForceUnLock(string path);
     }
}
