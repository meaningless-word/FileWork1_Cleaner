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
		public int refreshInterval { get; set; }
		public string path 
		{ 
			get { return fullPath; }
			set 
			{
				if (value.Length == 0)
				{
					fullPath = Environment.GetFolderPath(SpecialFolder.Desktop) + "\\Testing";
				}
				else
				{
					fullPath = value;
				}
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
				string barrier = new String('=', 20);
				DirectoryInfo d = new DirectoryInfo(fullPath);
				Console.WriteLine(barrier + " До зачистки... " + barrier);
				DirectoryExtension.DeepView(d, refreshInterval);
				Console.WriteLine();
				Console.WriteLine(barrier + " А теперь зачистка... " + barrier);
				Console.ReadKey();
				DirectoryExtension.DeepClean(d, refreshInterval);
				Console.ReadKey();
				Console.WriteLine();
				Console.WriteLine(barrier + " После зачистки... " + barrier);
				DirectoryExtension.DeepView(d, refreshInterval);
			}
			else
			{
				Console.WriteLine("Папка {0} не обнаружена", fullPath);
			}
		}
	}
}
