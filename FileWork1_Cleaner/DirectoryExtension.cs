using System;
using System.IO;

namespace FileWork1_Cleaner
{
	public static class DirectoryExtension
	{
		static int deep = 0; // счётчик погружения рекурсии для визуализации отступа
		/// <summary>
		/// вывод содержимого папки с указанием времени последнего обращения с пометкой о предстоящем удалении
		/// </summary>
		/// <param name="di">исследуемая папка</param>
		/// <param name="i">срок жизни файла в минутах</param>
		public static void DeepView(DirectoryInfo di, int i)
		{
			deep++; // визуализация погружения при выводе имени файла/папки
			string result = "";
			string tab = new String(' ', deep * 2); // отступ

			// обход каталогов
			DirectoryInfo[] dirs = di.GetDirectories();
			foreach (DirectoryInfo dir in dirs)
			{
				result = String.Format("{0,-20} {1,20} {2}"
					, String.Format("{0}[{1}]", tab, dir.Name)
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
				result = String.Format("{0,-20} {1,20} {2}"
					, tab + f.Name
					, f.LastAccessTime
					, DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(i) ? "sorry..." : ""
				);
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

				//далее попытка удаления папки без дополнительных проверок - положимся на try..catch
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
