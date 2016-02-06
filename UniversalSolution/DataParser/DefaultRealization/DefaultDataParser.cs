using DataParser.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DataParser.DefaultRealization
{
    public class DefaultDataParser : IDataParser
    {
        //Результуючий масив матчів зпарсений з json
        private List<IDataMatch> matchesRes;

        private IDataMatch mainMatchInst;

        public DefaultDataParser(IDataMatch _mainMatchInst)
        {
            matchesRes = new List<IDataMatch>();
            mainMatchInst = _mainMatchInst;
        }

        public List<IDataMatch> ParseData(JArray matchesJson)
        {
            //Перебираєм всі матчі і запаковуєм в об'єкти
            while (matchesJson != null && matchesJson.HasValues)
            {
                IDataMatch match = mainMatchInst.GetInstance();
                //Мабуть це піде в окремий метод, але поітм
                JToken val = matchesJson.First;
                match.Id = (int)val["Num"];
                match.Champ = val["ChampEng"].ToString();
                match.Sportname = val["SportNameEng"].ToString();
                match.Opp1Name = val["Opp1Eng"].ToString();
                match.Opp2Name = val["Opp2Eng"].ToString();
                foreach (var ev in val["Events"])
                {
                    switch ((int)ev["T"])
                    {
                        case 1:
                            match.P1 = (double)(ev["C"] ?? 0);
                            break;

                        case 2:
                            match.X = (double)(ev["C"] ?? 0);
                            break;

                        case 3:
                            match.P2 = (double)(ev["C"] ?? 0);
                            break;

                        case 4:
                            match.X1 = (double)(ev["C"] ?? 0);
                            break;

                        case 5:
                            match.I2 = (double)(ev["C"] ?? 0);
                            break;

                        case 6:
                            match.X2 = (double)(ev["C"] ?? 0);
                            break;

                        case 7:
                            match.I = (double)(ev["C"] ?? 0);
                            match.Fora1 = (double)(ev["P"] ?? 0);
                            break;

                        case 8:
                            match.II = (double)(ev["C"] ?? 0);
                            match.Fora2 = (double)(ev["P"] ?? 0);
                            break;

                        case 9:
                            match.B = (double)(ev["C"] ?? 0);
                            match.Total = (double)(ev["P"] ?? 0);
                            break;

                        case 10:
                            match.M = (double)(ev["C"] ?? 0);
                            match.Total = (double)(ev["P"] ?? 0);
                            break;
                    }
                }
                matchesRes.Add(match);
                matchesJson.Remove(val);
            }
            return matchesRes;
        }
    }
}