using System;

namespace FileWork1_Cleaner
{
	/// <summary>
	/// Напишите программу, которая чистит нужную нам папку от файлов и папок, которые не использовались более 30 минут 
	/// </summary>

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Необходимо указать путь к рабочей папке.");
			Console.WriteLine("При вводе пустой строки будет исползьоваться папка Testing на рабочем столе");
			Console.Write(": ");
			string workPath = Console.ReadLine();

			Observ watchIt = new Observ(workPath, 5);

			watchIt.WriteInfo();
		}
	}
}
