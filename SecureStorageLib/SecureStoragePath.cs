using System;
using System.Text;

namespace SecureStorageLib
{
    public static class CloudPath
    {
        public static char PathSeperator = '/';
        //string local = Properties.Settings.Default.init_input_dir;

        /// <summary>
        /// gets name / name used for cloud, aka removes local root & adjust slashes
        /// </summary>
        /// <param name="name">name / name to convert</param>
        /// <returns>cloud name / name</returns>
        //private static string GetCloudPath(string name)
        //{
        //    string local = Properties.Settings.Default.init_input_dir;
        //    name = name.Remove(0, local.Length);
        //    name = name.Replace('\\', '/');
        //    name = name.TrimStart('/'); // may trim start ?
        //    return name + "/";
        //}


        public static string[] GetSubDirectories(string path)
        {
            StringBuilder sb = new StringBuilder();
            path = path.TrimEnd('/');
            string[] splits = path.Split('/');

            int len = splits.Length - 1;
            string[] ret = new string[len];

            for(int i = 0; i < len; ++i)
            {
                sb.Clear();    
                for(int j = 0; j < len - i; ++j)
                {
                    sb.Append(splits[j] + "/");
                }
                ret[i] = sb.ToString();
            }

            return ret;
        }

        public static string GetDirectory(string path)
        {
            path = path.Trim('/');
             
            StringBuilder sb = new StringBuilder();
            string[] splits = path.Split(CloudPath.PathSeperator);
            int len = splits.Length;

            for (int i = 0; i < (len - 1); ++i)
            {
                sb.Append(splits[i] + PathSeperator.ToString());
            }

            string ret = sb.ToString();
            return ret != string.Empty ? ret : "/";
       }

        public static string GetShortName(string path)
        {
            // get file or dir name
            string[] splits = path.Split(CloudPath.PathSeperator);
            return splits[splits.Length - 1];
        }


        public static string GetFileName(string path)
        {
            int len = path.Length;
            if (path[len - 1] == '/')
                return null; // is a directory

            string[] splits = path.Split(CloudPath.PathSeperator);

            return splits[splits.Length - 1];
        }

    
    }
}
