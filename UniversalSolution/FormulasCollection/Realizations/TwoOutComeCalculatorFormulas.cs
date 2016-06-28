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
                return Math.Round(rate.Value, 2).
                    ToString(CultureInfo.CurrentCulture);
            return null;
        }

        public Tuple<string, string> GetRecommendedRates(double? rate, double? kof1, double? kof2)
        {
            if ((rate == null) || (kof1 == null) || (kof2 == null)) return null;

            var rate1 = Math.Round(rate.Value / (kof1.Value + kof2.Value) * kof2.Value, 2);
            var rate2 = Math.Round(rate.Value / (kof1.Value + kof2.Value) * kof1.Value, 2);
            return new Tuple<string, string>(
                rate1.ToString(CultureInfo.CurrentCulture),
                rate2.ToString(CultureInfo.CurrentCulture));
        }

        public double? CalculateSummaryRate(params double?[] rates) => rates?.Sum();

        public string CalculateAverageProfit(params double?[] profit) => (profit?.Sum() / 2).ToString();

        public string CalculateSummaryIncome(params double?[] incomes) => incomes?.Sum().ToString();

        public double? ConvertToRate(string value)
        {
            return value?.Trim().Replace(".", ",").ConvertToDoubleOrNull();
        }

        public string Formatter { get; set; }

        public string CalculateClearRate(double? v1, double? v2)
        {
            return (v1 + v2).ToString();
        }
    }
}