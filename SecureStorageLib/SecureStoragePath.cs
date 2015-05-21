using System;
using System.IO;
using System.Text;

namespace SecureStorageLib
{
    /// <summary>
    /// path functions for storage
    /// </summary>
    public static class StoragePath
    {
        public static char PathSeperator = '/';
        
        /// <summary>
        /// test if this is a directory
        /// </summary>
        /// <param name="name">name / path to test</param>
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
        /// 
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

        /// <summary>
        /// gets short name of object (no path)
        /// </summary>
        /// <param name="path">the full path</param>
        /// <returns>short name (no path) or null if directory</returns>
        public static string GetShortName(string path)
        {
            int len = path.Length;
            if (path[len - 1] == '/')
                return null; // is a directory
            string[] splits = path.Split(StoragePath.PathSeperator);
            return splits[splits.Length - 1];
        }
    }
}
