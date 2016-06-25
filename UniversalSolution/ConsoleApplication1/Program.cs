using DataParser.Enums;
using DataParser.MY;
using System;

namespace ParseAPI
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            MarathonParser p = new MarathonParser();
            p.ShowForks(p.InitiAsync(SportType.Basketball).Result);
            Console.ReadKey();
        }
    }
}