using DataParser.Enums;
using DataParser.MY;
using FormulasCollection.Realizations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataParser.DefaultRealization
{
    public class Parser
    {
        public async Task<List<Fork>> GetGetAllForksAsync(SportType sportType, string userLogin, string userPass)
        {
            var resList = new List<ResultForForks>();

            resList.AddRange(await new PinnacleSportsDataParser(new ConverterFormulas()).
                GetAllPinacleEventsForRequestAsync(sportType, userLogin, userPass).ConfigureAwait(false));

            resList.AddRange(await new ParsePinnacle().InitiAsync(sportType).ConfigureAwait(false));

            //call Forks

            return null;//return Forks
        }
    }
}
