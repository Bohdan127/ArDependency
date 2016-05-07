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

        public string CalculateRate(double rateMain, double rateCurrent ,double kof){
            return string.Format("Profit of rate {0} is {1} ", rateCurrent, ((rateCurrent*kof) - rateMain));
        }
        public List<string> getRates(double rate, double kof1, double kof2)
        {
            //  1.5   2.5    x = 400/(1.5+2.5) = 100   stawka2 =  x * kof1; stawka1 = x * kof2
            List<string> buff = new List<string>();
            string rate1 = (((rate*2) / (kof1 + kof2)) * kof2).ToString();
            string rate2 = (((rate*2) / (kof1 + kof2)) * kof1).ToString();
            buff.Add(rate1);
            buff.Add(rate2);
            return buff;
        }

        public string CalculateSummaryRate(params double[] rates) => rates.Sum().ToString();

        public string CalculateSummaryIncome(params string[] incomes) => incomes.Aggregate((a, b) => a + Formatter + b);

        public string Formatter { get; set; }
    }
}