using System;
using System.Globalization;
using System.Linq;
using ToolsPortable;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeCalculatorFormulas
    {
        public TwoOutComeCalculatorFormulas()
        {
            Formatter = "-";
        }

        public string CalculateIncome(double coef, double rate) => (coef * rate).ToString(); // dont use in code

        public string CalculateRate(double? rateMain, double? rateCurrent, double? kof)
        {
            var rate = rateCurrent * kof - rateMain;
            if (rate != null)
                return decimal.Round((decimal)rate, 2, MidpointRounding.AwayFromZero).
                    ToString(CultureInfo.CurrentCulture);
            return null;
        }

        public Tuple<string, string> GetRecommendedRates(double? rate, double? kof1, double? kof2)
        {
            if ((rate == null) || (kof1 == null) || (kof2 == null)) return null;

            string rate1 = $"{rate.Value / (kof1.Value + kof2.Value) * kof2.Value}";
            string rate2 = $"{rate.Value / (kof1.Value + kof2.Value) * kof1.Value}";
            return new Tuple<string, string>(rate1, rate2);
        }

        public double? CalculateSummaryRate(params double?[] rates) => rates?.Sum();

        public string CalculateAverageProfit(params double?[] profit) => (profit?.Sum() / 2).ToString();

        public string CalculateSummaryIncome(params string[] incomes) => incomes?.Aggregate((a, b) => a + Formatter + b);

        public double? ConvertToRate(string value)
        {
            return value?.Trim().Replace(".", ",").ConvertToDoubleOrNull();
        }

        public string Formatter { get; set; }
    }
}