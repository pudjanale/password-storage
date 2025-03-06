using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mypass
{
    internal class PathManager
    {
        public static string DocumentsDir
        {
            get => CheckDir($@"C:\Users\{Environment.UserName}\Documents\mypass");
        }

        public static string PasswordFile
        {
            get => CheckPath(DocumentsDir + $@"\hash.json");
        }

        private static string CheckPath(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            return path;
        }

        private static string CheckDir(string path)
        {
            Directory.CreateDirectory(path);

            return path;
        }
    }
}
