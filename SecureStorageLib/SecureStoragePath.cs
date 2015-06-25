using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SecureStorageLib
{
    /// <summary>
    /// path functions for storage
    /// </summary>
    public static class StoragePath
    {
        public static char PathSeperator = '/';
        

        /// <summary>
        /// validate a name (file, or directory name before hashing)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValidName(string name)
        {
            // BKP simplify this sucker
            //string expression = @"^[a-zA-Z0-9\s.,;~`'@#$%^&{}()\[\]!+-=_]+$";

            // BKP no = sign, had problems with + sign
            string expression = @"^[a-zA-Z0-9\s._=,;~`'@#$%^&{}()\[\]!+-]+$";
            Regex regx = new Regex(expression, RegexOptions.Singleline);
            
            MatchCollection mc = regx.Matches(name);
            // test for exact match
            if (mc.Count == 1 && mc[0].Length == name.Length)
                return true;
            return false;
        }


        /// <summary>
        /// IsValidPath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsValidPath(string path)
        {

            return false;
        }

        /// <summary>
        /// DescriptorExists
        /// </summary>
        /// <param name="sercure_name"></param>
        /// <param name="store"></param>
        /// <param name="crypto"></param>
        /// <returns></returns>
        public static bool DescriptorExists(string sercure_name, IStorage store, ICryptography crypto)
        {
            string sub_dir_name = StoragePath.GetDirectory(sercure_name);
            string secure_sub_dir_name = SecureStorageUtility.GetSecureName(sub_dir_name, crypto.Key);
            return store.Exists(secure_sub_dir_name);
        }
        
        /// <summary>
        /// test if this is a directory
        /// </summary>
        /// <param name="name">name ends with / path to test</param>
        /// <returns>true if a directory</returns>
        public static bool IsDirectory(string name)
        {
            // this is as simple as it gets
            return name.EndsWith(PathSeperator.ToString());
        }
        
        /// <summary>
        /// converts windows path to a storage path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetStoragePath(string path)
        {
            int root_len = Path.GetPathRoot(path).Length;
            path = path.Remove(0, root_len); // remove drive letter
            path = path.Replace('\\', '/');
            path = path.Trim('/'); 
            return path + "/";
        }
        
        /// <summary>
        /// GetSubDirectories
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetSubDirectories(string path)
        {
            StringBuilder sb = new StringBuilder();
            path = path.TrimEnd('/');
            string[] splits = path.Split('/');

            int len = splits.Length - 1;
            string[] ret = new string[len];

            for (int i = 0; i < len; ++i)
            {
                sb.Clear();
                for (int j = 0; j < len - i; ++j)
                {
                    sb.Append(splits[j] + "/");
                }
                ret[i] = sb.ToString();
            }
            return ret;
        }

        /// <summary>
        /// gets directory of name
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectory(string path)
        {
            path = path.Trim('/');

            StringBuilder sb = new StringBuilder();
            string[] splits = path.Split(StoragePath.PathSeperator);
            int len = splits.Length;

            for (int i = 0; i < (len - 1); ++i)
            {
                sb.Append(splits[i] + PathSeperator.ToString());
            }

            string ret = sb.ToString();
            return ret != string.Empty ? ret : "/";
        }
    }
}
