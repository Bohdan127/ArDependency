using DataSaver;
using DataSaver.Models;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXApplication1.Models
{
    public class DataManager
    {
        private LocalSaver localSaver;

        public DataManager(IForkFormulas forkFormulas)
        {
            localSaver = new LocalSaver();
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            if (filterPage == null) return new List<Fork>();

            return await localSaver.GetForksAsync(filterPage).ConfigureAwait(false);
        }
    }
}
