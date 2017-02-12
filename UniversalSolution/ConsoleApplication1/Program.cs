using FormulasCollection.Models;
using System;
using System.Windows.Forms;
using DataSaver;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Console.WriteLine("Удаление старой базы...");
            var saver = new LocalSaver();
            saver.ClearDatabase();
            Console.WriteLine("удаление успешно завершено!");
            //Application.Run(new CalculatorPage { Fork = new Fork { Profit = 100 }, WindowState = FormWindowState.Normal });
        }
    }
}