using DataSaver;
using DataSaver.Models;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;

namespace DXApplication1.Models
{
    public class DataManager
    {
        private LocalSaver localSaver;


        public DataManager(IForkFormulas forkFormulas)
        {
            localSaver = new LocalSaver();
        }

        public double getProfit(double rate, double kof1, double kof2)
        {
            return ((rate / (kof1 + kof2)) * (kof1 * kof2));
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            if (filterPage == null) return new List<Fork>();

            var forks = await localSaver.GetForksAsync(filterPage).ConfigureAwait(false);

            if (filterPage.DefaultRate != null)
            {
                foreach (var fork in forks)
                {
                    fork.Profit = getProfit(
                        Convert.ToDouble(filterPage.DefaultRate.Value),
                        fork.CoefFirst.ConvertToDouble(),
                        fork.CoefSecond.ConvertToDouble());
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
