﻿using System;
using System.Text;

namespace ClientWindowsFormsApplication
{
    public static class CloudPath
    {
        public static char PathSeperator = '/';

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

            return sb.ToString();
       }

        public static string GetName(string path)
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
