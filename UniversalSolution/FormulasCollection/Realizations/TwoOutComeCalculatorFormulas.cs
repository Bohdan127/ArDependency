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

        public string CalculateIncome(double coef, double rate) => (coef * rate).ToString();

        public string CalculateRate(double? rateMain, double? rateCurrent, double? kof)
        {
            //todo do not remove because I think using all our double? without checking and key-work Value is incorrect
            //if (rateMain == null || rateCurrent == null || kof == null) return string.Empty;

            return $"Прибыли ставка {rateCurrent} это {rateCurrent * kof - rateMain}" ;
        }

        public List<string> GetRates(double rate, double kof1, double kof2)
        {
            List<string> buff = new List<string>();
            string rate1 = ((rate / (kof1 + kof2)) * kof2).ToString();
            string rate2 = ((rate / (kof1 + kof2)) * kof1).ToString();
            buff.Add(rate1);
            buff.Add(rate2);
            return buff;
        }

        public string CalculateSummaryRate(params double?[] rates) => rates.Sum().ToString();

        public string CalculateSummaryIncome(params string[] incomes) => incomes.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}