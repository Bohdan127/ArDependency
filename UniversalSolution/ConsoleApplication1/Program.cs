using DXApplication1.Pages;
using FormulasCollection.Models;
using System;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.Run(new CalculatorPage { Fork = new Fork { Profit = 100 }, WindowState = FormWindowState.Normal });
        }
    }
}