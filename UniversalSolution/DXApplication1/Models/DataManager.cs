using DataSaver;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
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
            if (filterPage == null)
                return new List<Fork>();

            var forks = await _localSaver.GetForksAsync(filterPage, forkType)
                                         .ConfigureAwait(false);

            foreach (var fork in forks)
            {
                fork.Profit = _calculatorFormulas.GetProfit(
                    fork.CoefFirst.ConvertToDoubleOrNull() ?? 0d,
                    fork.CoefSecond.ConvertToDoubleOrNull() ?? 0d);
                fork.prices = null;
            }

            return _calculatorFormulas.FilteredForks(forks, filterPage);
        }

        public Filter GetFilter()
        {
            return _localSaver.FindFilter();
        }

        public void MapFilter(Filter filter)
        {
            var dbFilter = GetFilter();
            filter.Id = dbFilter.Id;
            filter.Min = dbFilter.Min;
            filter.Max = dbFilter.Max;
            filter.Football = dbFilter.Football;
            filter.Basketball = dbFilter.Basketball;
            filter.Volleyball = dbFilter.Volleyball;
            filter.Hockey = dbFilter.Hockey;
            filter.Tennis = dbFilter.Tennis;
            filter.FaterThen = dbFilter.FaterThen;
            filter.LongerThen = dbFilter.LongerThen;
            filter.LicenseKey = dbFilter.LicenseKey;
            filter.AutoUpdateTime = dbFilter.AutoUpdateTime;
            filter.RecommendedRate1 = dbFilter.RecommendedRate1;
            filter.RecommendedRate2 = dbFilter.RecommendedRate2;
            filter.MinPinBet = dbFilter.MinPinBet;
            filter.MinMarBet = dbFilter.MinMarBet;
        }
    }
}