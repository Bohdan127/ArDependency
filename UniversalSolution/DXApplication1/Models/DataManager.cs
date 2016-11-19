using DataSaver;
using DataSaver.Models;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolsPortable;

namespace DXApplication1.Models
{
    public class DataManager
    {
        private readonly TwoOutComeCalculatorFormulas _calculatorFormulas;
        private readonly LocalSaver _localSaver;

        public DataManager()
        {
            _calculatorFormulas = new TwoOutComeCalculatorFormulas();
            _localSaver = new LocalSaver();
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage, ForkType forkType)
        {
            if (filterPage == null) return new List<Fork>();

            var forks = await _localSaver.GetForksAsync(filterPage, forkType)
                                         .ConfigureAwait(false);

            foreach (var fork in forks)
            {
                var defRate = 100.00;
                var coef1 = fork.CoefFirst.ConvertToDoubleOrNull();
                var coef2 = fork.CoefSecond.ConvertToDoubleOrNull();
                var rates = _calculatorFormulas.GetRecommendedRates(defRate, coef1, coef2);

                if (rates == null) continue;

                var rate1 = Convert.ToDouble(rates.Item1);
                var rate2 = Convert.ToDouble(rates.Item2);
                var allRate = _calculatorFormulas.CalculateSummaryRate(rate1, rate2);
                var income1 = Convert.ToDouble(_calculatorFormulas.CalculateRate(allRate, allRate - rate2, coef1));
                var income2 = Convert.ToDouble(_calculatorFormulas.CalculateRate(allRate, allRate - rate1, coef2));
                var income3 = Convert.ToDouble(_calculatorFormulas.CalculateClearRate(rate2, income1));
                var income4 = Convert.ToDouble(_calculatorFormulas.CalculateClearRate(rate1, income2));
                //todo delete this shit and refactored to one command
                fork.Profit = Math.Round(Convert.ToDouble(_calculatorFormulas.CalculateSummaryIncome(income3, income4)) * 100 / defRate - 100, 2);
            }

            return FilteredForks(forks, filterPage);
        }

        private List<Fork> FilteredForks(List<Fork> forks, Filter filterPage)
        {
            if (filterPage.Min != null) forks.RemoveAll(f => Convert.ToDecimal(f.Profit) <= filterPage.Min.Value);
            if (filterPage.Max != null) forks.RemoveAll(f => Convert.ToDecimal(f.Profit) >= filterPage.Max.Value);
            if (filterPage.FaterThen != null)
            {
                DateTime dateFater;
                forks.RemoveAll(a => DateTime.Compare(DateTime.TryParse(a.MatchDateTime.Trim(), out dateFater)
                                                          ? dateFater
                                                          : DateTime.Now, filterPage.FaterThen.Value) < 0);
            }
            if (filterPage.LongerThen != null)
            {
                DateTime dateLonger;
                forks.RemoveAll(a => DateTime.Compare(DateTime.TryParse(a.MatchDateTime.Trim(), out dateLonger)
                                                          ? dateLonger
                                                          : DateTime.Now, filterPage.LongerThen.Value) > 0);
            }
            if (filterPage.MinMarBet != null) forks.RemoveAll(f => Convert.ToDecimal(f.MarRate) < filterPage.MinMarBet);
            if (filterPage.MinPinBet != null) forks.RemoveAll(f => f.PinRate.ConvertToDecimalOrNull() < filterPage.MinPinBet);
            return forks;
        }
    }
}