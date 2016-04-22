using DataParser.MY;
using FormulasCollection.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeForkFormulas : IForkFormulas
    {
        public bool CheckIsFork(double coef1, double coef2) => 1 > (1 / coef1 + 1 / coef2);

        public List<Fork> GetAllForks(List<ResultForForks> events)
        {
            var resList = new List<Fork>(); //todo remake later to linq!!!!
            foreach (var eventFirst in events)
                foreach (var eventSecond in events)
                {
                    if (eventFirst != eventSecond &&
                        CheckIsFork(eventFirst.Coef.ConvertToDouble(), eventSecond.Coef.ConvertToDouble()))
                    {
                        resList.Add(new Fork //todo remake it to default C# 6.0 constructor
                        {
                            Event = eventFirst.Event,
                            TypeFirst = eventFirst.Type,
                            CoefFirst = eventFirst.Coef.ConvertToDouble(),
                            TypeSecond = eventSecond.Type,
                            CoefSecond = eventSecond.Coef.ConvertToDouble()
                        });
                    }
                }
            return resList;
        }

        public async Task<List<Fork>> GetAllForksAsync(List<ResultForForks> events)
        {
            return GetAllForks(events);
        }
    }
}
