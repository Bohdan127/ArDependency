using DataSaver;
using DataSaver.Models;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
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

            List<Fork> buffs = await localSaver.GetForksAsync(filterPage).ConfigureAwait(false);

            foreach(Fork fk in buffs)
            {
                if(filterPage.DefaultRate != null)
                fk.Profit = getProfit(filterPage.DefaultRate.Value.ConvertToDouble(), fk.CoefFirst.ConvertToDouble(), fk.CoefSecond.ConvertToDouble()).ToString();
            }
            return await localSaver.GetForksAsync(filterPage).ConfigureAwait(false);
        }
    }
}
