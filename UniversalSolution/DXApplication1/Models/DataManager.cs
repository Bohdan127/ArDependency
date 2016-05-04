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
        private IForkFormulas _forkFormulas;

        public DataManager(IForkFormulas forkFormulas)
        {
            _forkFormulas = forkFormulas;
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage, Site site)
        {
            if (filterPage == null) return new List<Fork>();

            var resList = new List<Fork>();

            if (filterPage.Basketball)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Basketball, site).ConfigureAwait(false));
            else if (filterPage.Football)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Football, site).ConfigureAwait(false));
            else if (filterPage.Hockey)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Hockey, site).ConfigureAwait(false));
            else if (filterPage.Tennis)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Tennis, site).ConfigureAwait(false));
            else if (filterPage.Volleyball)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Volleyball, site).ConfigureAwait(false));

            return resList;
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            var resList = new List<Fork>();

            if (filterPage.MarathonBet)
                resList.AddRange(await GetForksForAllSportsAsync(filterPage, Site.MarathonBet).ConfigureAwait(false));
            if (filterPage.PinnacleSports)
                resList.AddRange(await GetForksForAllSportsAsync(filterPage, Site.PinnacleSports).ConfigureAwait(false));

            return resList;
        }

        public async Task<List<Fork>> GetForksForSportTypeAsync(SportType sportType, Site site)
        {
            await new PinnacleSportsDataParser(new ConverterFormulas()).GetAllPinacleEventsForRequestAsync(sportType).ConfigureAwait(false);

            return null;
            //await _forkFormulas.GetAllForksAsync(await new ParsePinnacle().GetNameTeamsAndDateAsync(sportType, site).ConfigureAwait(true)).ConfigureAwait(false);
        }
    }
}
