using System.Linq;

namespace FormulasCollection
{
    public class TwoOutComeCalculatorFormulas : ICalculatorFormulas
    {
        public TwoOutComeCalculatorFormulas()
        {
            Formatter = "-";
        }

        public object CalculateIncome(double coef, double rate) => coef * rate;

        public object CalculateSummaryRate(params double[] rates) => rates.Sum();

        public object CalculateSummaryIncome(params string[] incomes) => incomes.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}