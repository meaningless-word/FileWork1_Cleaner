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
            Observ watchIt = new Observ("Testing", 5);

            watchIt.WriteInfo();
        }
    }
}
