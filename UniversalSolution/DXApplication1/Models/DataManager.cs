using DataSaver;
using DataSaver.Models;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXApplication1.Models
{
    public class DataManager
    {
        private readonly TwoOutComeCalculatorFormulas _calculatorFormulas;
        private readonly TwoOutComeForkFormulas _forkFormulas;
        private LocalSaver localSaver;

        public DataManager(TwoOutComeForkFormulas forkFormulas)
        {
            _forkFormulas = forkFormulas;
            localSaver = new LocalSaver();
            _calculatorFormulas = new TwoOutComeCalculatorFormulas(); ;
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            if (filterPage == null) return new List<Fork>();

            var forks = await localSaver.GetForksAsync(filterPage).ConfigureAwait(false);

            if (filterPage.DefaultRate != null)
            {
                foreach (var fork in forks)
                {
                    fork.Profit = _forkFormulas.GetProfit(
                        Convert.ToDouble(filterPage.DefaultRate.Value),
                        _calculatorFormulas.ConvertToRate(fork.CoefFirst),
                        _calculatorFormulas.ConvertToRate(fork.CoefSecond));
                }
            }

            if (filterPage.Min != null)
                forks.RemoveAll(f => f.Profit <= filterPage.Min.Value);
            if (filterPage.Max != null)
                forks.RemoveAll(f => f.Profit >= filterPage.Max.Value);

            return forks;
        }
    }
}