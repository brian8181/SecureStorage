using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLocking_ProofOfConcept
{
    class FileLock : IFileLock
    {
        #region IFileLock Members

        public bool isLocked(string path)
        {
            throw new NotImplementedException();
        }

        public bool GetLock()
        {
            throw new NotImplementedException();
        }

        public bool ReleaseLock()
        {
            throw new NotImplementedException();
        }

        public void ForceUnLock(string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
