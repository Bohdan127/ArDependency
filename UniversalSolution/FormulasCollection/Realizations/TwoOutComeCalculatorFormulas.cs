using FormulasCollection.Interfaces;
using System.Linq;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeCalculatorFormulas : ICalculatorFormulas
    {
        public TwoOutComeCalculatorFormulas()
        {
            Formatter = "-";
        }

        public string CalculateIncome(double coef, double rate) => (coef * rate).ToString();

        public string CalculateSummaryRate(params double[] rates) => rates.Sum().ToString();

        public string CalculateSummaryIncome(params string[] incomes) => incomes.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}