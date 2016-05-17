using FormulasCollection.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeCalculatorFormulas : ICalculatorFormulas
    {
        public TwoOutComeCalculatorFormulas()
        {
            Formatter = "-";
        }

        public string CalculateIncome(double coef, double rate) => (coef * rate).ToString(); // dont use in code

        public string CalculateRate(double? rateMain, double? rateCurrent, double? kof) =>
            $"Прибыли ставка {rateCurrent} это {((rateCurrent * kof) - rateMain)}";

        //todo do not remove because I think using all our double? without checking and key-work Value is incorrect
        //if (rateMain == null || rateCurrent == null || kof == null) return string.Empty;


        public List<string> GetRecommendedRates(double? rate, double? kof1, double? kof2)
        {
            if ((rate != null) && (kof1 != null) && (kof2 != null))
            {
                string rate1 = $"{((rate.Value / (kof1.Value + kof2.Value)) * kof2.Value)} рекумендуемая ставка на кофф {kof1.Value}";
                string rate2 = $"{((rate.Value / (kof1.Value + kof2.Value)) * kof1.Value)} рекумендуемая ставка на кофф {kof2.Value}";
                return new List<string>(new[] { rate1, rate2 });
            }
            return new List<string>(new [] { "Нет рекомендуемых ставок" });
        }
        public string CalculateSummaryRate(params double?[] rates) => rates.Sum().ToString();

        public string CalculateAverageProfit(params double?[] profit) => (profit.Sum() / 2).ToString(); 
        public string CalculateSummaryIncome(params string[] incomes) => incomes.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}