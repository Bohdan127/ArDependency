using DataParser.Enums;
using DataParser.MY;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataParser.DefaultRealization
{
    public class Parser
    {

        private IForkFormulas _forkFormulas;

        public Parser(IForkFormulas forkFormulas)
        {
            _forkFormulas = forkFormulas;
        }


        public async Task<List<Fork>> GetGetAllForksAsync(SportType sportType, string userLogin, string userPass, int defaultRate)
        {
            var result = new List<Fork>();      //resList

            //resList.AddRange(await new PinnacleSportsDataParser(new ConverterFormulas()).
            //    GetAllPinacleEventsForRequestAsync(sportType, userLogin, userPass).ConfigureAwait(false));

            //resList.AddRange(await new ParsePinnacle().InitiAsync(sportType).ConfigureAwait(false));
            // todo Convert Lists into Dictionary
            //var result = _forkFormulas.GetAllForks(resList, resList, defaultRate);
            //MessageBox.Show($"всего было найдено {resList.Count} событий");
            //MessageBox.Show($"всего было найдено {result.Count} з них вилок");

            return result;
        }
    }
}
