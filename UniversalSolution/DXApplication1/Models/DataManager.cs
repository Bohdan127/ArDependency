using DataParser.DefaultRealization;
using DataParser.Enums;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXApplication1.Models
{
    public class DataManager
    {
        private Parser _parser;

        public DataManager(IForkFormulas forkFormulas)
        {
            _parser = new Parser(forkFormulas);
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            if (filterPage == null) return new List<Fork>();

            var defValue = 0;

            if (filterPage.DefaultRate != null)
                defValue = filterPage.DefaultRate.Value;

            var resList = new List<Fork>();

            if (filterPage.Basketball)
                resList.AddRange(await GetForksForSportTypeAsync(
                        SportType.Basketball,
                        filterPage.UserName, filterPage.UserPass,
                       defValue)
                    .ConfigureAwait(false));
            if (filterPage.Football)
                resList.AddRange(await GetForksForSportTypeAsync(
                        SportType.Soccer,
                        filterPage.UserName,
                        filterPage.UserPass,
                        defValue)
                    .ConfigureAwait(false));
            if (filterPage.Hockey)
                resList.AddRange(await GetForksForSportTypeAsync(
                        SportType.Hockey,
                        filterPage.UserName,
                        filterPage.UserPass,
                       defValue)
                    .ConfigureAwait(false));
            if (filterPage.Tennis)
                resList.AddRange(await GetForksForSportTypeAsync(
                        SportType.Tennis,
                        filterPage.UserName,
                        filterPage.UserPass,
                        defValue)
                    .ConfigureAwait(false));
            if (filterPage.Volleyball)
                resList.AddRange(await GetForksForSportTypeAsync(
                        SportType.Volleyball,
                        filterPage.UserName,
                        filterPage.UserPass,
                        defValue)
                    .ConfigureAwait(false));

            return resList;
        }

        public async Task<List<Fork>> GetForksForSportTypeAsync(SportType sportType, string userLogin, string userPass, int defaultRate)
        {
            return await _parser.GetGetAllForksAsync(sportType, userLogin, userPass, defaultRate).ConfigureAwait(false);
        }
    }
}
