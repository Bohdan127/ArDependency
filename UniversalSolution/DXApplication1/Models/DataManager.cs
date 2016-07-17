using DataSaver;
using DataSaver.Models;
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
        private readonly TwoOutComeForkFormulas _forkFormulas;
        private readonly LocalSaver _localSaver;

        public DataManager()
        {
            _forkFormulas = new TwoOutComeForkFormulas();
            _localSaver = new LocalSaver();
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage, ForkType forkType)
        {
            if (filterPage == null) return new List<Fork>();

            var forks = await _localSaver.GetForksAsync(filterPage, forkType).ConfigureAwait(false);

            if (filterPage.DefaultRate != null)
            {
                foreach (var fork in forks)
                {
                    fork.Profit = _forkFormulas.GetProfit(
                        filterPage.DefaultRate.Value.ConvertToDoubleOrNull(),
                        fork.CoefFirst.ConvertToDoubleOrNull(),
                        fork.CoefSecond.ConvertToDoubleOrNull());
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