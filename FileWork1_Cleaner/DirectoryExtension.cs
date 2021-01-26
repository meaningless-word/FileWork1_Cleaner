using System;
using System.IO;

namespace FileWork1_Cleaner
{
    public static class DirectoryExtension
    {
        static int deep = 0;
        public static void DeepView(DirectoryInfo di, int i)
        {
            string result = "";
            deep++; // визуализация погружения при выводе имени файла/папки

            // обход каталогов
            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                result = String.Format("{0}{1,-20} {2,20} {3}"
                    , new String(' ', deep * 2)
                    , String.Format("[{0}]", dir.Name)
                    , dir.LastAccessTime
                    , dir.GetFiles().Length + dir.GetDirectories().Length == 0 && DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(i) ? "sorry..." : ""
                );
                Console.WriteLine(result);
                DeepView(dir, i);
            }

            // переходим к файлам
            FileInfo[] fs = di.GetFiles();
            foreach (FileInfo f in fs)
            {
                result = String.Format("{0}{1,-20} {2,20} {3}", new String(' ', deep * 2), f.Name, f.LastAccessTime, DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(i) ? "sorry..." : "" );
                Console.WriteLine(result); 
            }

            deep--;
        }

        /// <summary>
        /// очистка файлов и подкаталогов, чьё время последнего обращения превышает указанный интервал 
        /// </summary>
        /// <param name="di">исследуемая папка</param>
        /// <param name="i">срок жизни файла в минутах</param>
        public static void DeepClean(DirectoryInfo di, int i)
		{
            //поскольку очистка файлов поменяет время последнего обращения папки на текущее, 
            //необходимо вычислить время жизни каталога до начала операций
            TimeSpan t = DateTime.Now - di.LastAccessTime;

            //сначала исследуются файлы
            FileInfo[] fs = di.GetFiles();
            foreach (FileInfo f in fs)
			{
                try
				{
                    if (DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(i))
                        f.Delete();
				}
                catch (Exception e)
				{
                    Console.WriteLine("файл {0} не удаляется по причине: {1}", f.FullName, e.Message);
				}
			}

            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
			{
                //сначала погружение
                DeepClean(dir, i);

                //далее попытка удаления без дополнительных проверок - положимся на try..catch
                try
				{
                    if (t > TimeSpan.FromMinutes(i))
                        dir.Delete();
				}
                catch (Exception e)
                {
                    Console.WriteLine("папка {0} не удаляется по причине: {1}", dir.FullName, e.Message);
                }
			}

        }
    }
}
