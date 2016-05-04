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
        private Parser _parser;

        public DataManager(IForkFormulas forkFormulas)
        {
            _forkFormulas = forkFormulas;
            _parser = new Parser();
        }

        public async Task<List<Fork>> GetForksForAllSportsAsync(Filter filterPage)
        {
            if (filterPage == null) return new List<Fork>();

            var resList = new List<Fork>();

            if (filterPage.Basketball)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Basketball, filterPage.UserName, filterPage.UserPass).ConfigureAwait(false));
            if (filterPage.Football)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Soccer, filterPage.UserName, filterPage.UserPass).ConfigureAwait(false));
            if (filterPage.Hockey)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Hockey, filterPage.UserName, filterPage.UserPass).ConfigureAwait(false));
            if (filterPage.Tennis)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Tennis, filterPage.UserName, filterPage.UserPass).ConfigureAwait(false));
            if (filterPage.Volleyball)
                resList.AddRange(await GetForksForSportTypeAsync(
                    SportType.Volleyball, filterPage.UserName, filterPage.UserPass).ConfigureAwait(false));

            return resList;
        }

        public async Task<List<Fork>> GetForksForSportTypeAsync(SportType sportType, string userLogin, string userPass)
        {
            // await new PinnacleSportsDataParser(new ConverterFormulas()).GetAllPinacleEventsForRequestAsync(sportType, userLogin, userPass).ConfigureAwait(false);

            return await _parser.GetGetAllForksAsync(sportType, userLogin, userPass).ConfigureAwait(false);
            //await _forkFormulas.GetAllForksAsync(await new ParsePinnacle().GetNameTeamsAndDateAsync(sportType, site).ConfigureAwait(true)).ConfigureAwait(false);
        }
    }
}
