using System;
using System.Linq;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeCalculatorFormulas
    {
        public TwoOutComeCalculatorFormulas()
        {
            Formatter = "-";
        }

        public string CalculateIncome(double coef, double rate) => (coef * rate).ToString(); // dont use in code

        public string CalculateRate(double? rateMain, double? rateCurrent, double? kof) =>
            $"{rateCurrent * kof - rateMain}";

        public Tuple<string, string> GetRecommendedRates(double? rate, double? kof1, double? kof2)
        {
            if ((rate == null) || (kof1 == null) || (kof2 == null)) return null;

            string rate1 = $"{rate.Value / (kof1.Value + kof2.Value) * kof2.Value}";
            string rate2 = $"{rate.Value / (kof1.Value + kof2.Value) * kof1.Value}";
            return new Tuple<string, string>(rate1, rate2);
        }

        public string CalculateSummaryRate(params double?[] rates) => rates?.Sum().ToString();

        public string CalculateAverageProfit(params double?[] profit) => (profit?.Sum() / 2).ToString();

        public string CalculateSummaryIncome(params string[] incomes) => incomes?.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}