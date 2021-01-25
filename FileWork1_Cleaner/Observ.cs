using System;
using System.IO;
using static System.Environment;

namespace FileWork1_Cleaner
{
    /// <summary>
    /// наблюдение за папкой с целью очистки файлов и подкаталогов, котороые не использовались более заданного интервала
    /// </summary>
    public class Observ
    {
        private string fullPath;
        private string folderOnDesktop;
        public int refreshInterval { get; set; }
        public string path 
        { 
            get { return fullPath; }
            set 
            {
                folderOnDesktop = value;
                fullPath = Environment.GetFolderPath(SpecialFolder.Desktop) + "\\" + value;
            } 
        }

        public Observ(string path)
        {
            this.path = path;
            refreshInterval = 30;
        }

        public Observ(string path, int interval)
        {
            this.path = path;
            refreshInterval = interval;
        }

        public void WriteInfo()
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo d = new DirectoryInfo(fullPath);
                Console.WriteLine(DirectoryExtension.DeepView(d, refreshInterval));



            }
            else
            {
                Console.WriteLine("Папки {0} нет на рабочем столе", folderOnDesktop);
            }
        }

    }
}
