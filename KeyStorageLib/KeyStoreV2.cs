using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStorage
{
    public class KeyStoreV2
    {
        Dictionary<string, Key> keys = new Dictionary<string, Key>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crypt"></param>
        public KeyStoreV2(SecureStorageLib.ICryptography crypt)
        {

        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Key this[string name]
        {
            get
            {
                return null;
            }
        }
    
    }
}
