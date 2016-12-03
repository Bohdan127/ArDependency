using DXApplication1.Pages;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
using System;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var _calculatorFormulas = new TwoOutComeCalculatorFormulas();
            var defRate = 100d;
            var rates = _calculatorFormulas.GetRecommendedRates(defRate, 5.090, 1.260);

            var rate1 = Convert.ToDouble(rates.Item1);
            var rate2 = Convert.ToDouble(rates.Item2);
            var allRate = _calculatorFormulas.CalculateSummaryRate(rate1, rate2);
            var income1 = Convert.ToDouble(_calculatorFormulas.CalculateRate(allRate, allRate - rate2, 5.090));
            var income2 = Convert.ToDouble(_calculatorFormulas.CalculateRate(allRate, allRate - rate1, 1.260));
            var income3 = Convert.ToDouble(_calculatorFormulas.CalculateClearRate(rate2, income1));
            var income4 = Convert.ToDouble(_calculatorFormulas.CalculateClearRate(rate1, income2));
            //todo delete this shit and refactored to one command
            Console.WriteLine(income1 < income2
                ? income1
                : income2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var calc = new CalculatorPage();

            calc.Fork = new Fork
            {
                BookmakerFirst = "Олімп",
                BookmakerSecond = "Плюс-Минус",
                CoefFirst = "5.090",
                CoefSecond = "1.260",
                Profit = 100
            };
            calc.WindowState = FormWindowState.Normal;
            calc.ShowDialog();
        }

    }
}