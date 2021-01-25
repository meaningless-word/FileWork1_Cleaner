using System;
using System.IO;

namespace FileWork1_Cleaner
{
    public static class DirectoryExtension
    {
        static int deep = 0;
        static int freshItemsCount = 0;
        public static string DeepView(DirectoryInfo di, int i)
        {
            string result = "";

            // обход каталогов
            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                result += String.Format("{0}{1,-20} {2,20} {3}\n "
                    , new String(' ', deep * 2)
                    , String.Format("[{0}]", dir.Name)
                    , dir.LastAccessTime
                    , dir.GetFiles().Length + dir.GetDirectories().Length == 0 && DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(i) ? "sorry..." : ""
                );
                deep++;
                result += DeepView(dir, i);
                deep--;
            }

            // переходим к файлам
            FileInfo[] fs = di.GetFiles();
            foreach(FileInfo f in fs)
            {
                result += String.Format("{0}{1,-20} {2,20} {3}\n", new String(' ', deep * 2), f.Name, f.LastAccessTime, DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(i) ? "sorry..." : "" );
            }

            return result;
        }
    }
}
