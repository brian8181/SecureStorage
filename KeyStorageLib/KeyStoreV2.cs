using System;
using System.Collections.Generic;
using System.Text;
using CryptographyLib;

namespace KeyStorage
{
    public class KeyStoreV2
    {
        Dictionary<string, Key> keys = new Dictionary<string, Key>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crypt"></param>
        public KeyStoreV2(string path, string password, ICryptography crypt)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="passowrd"></param>
        /// <param name="keys"></param>
        public static void CreateStore(string path, string passowrd, params Key[] keys)
        {

        }
    }
}
