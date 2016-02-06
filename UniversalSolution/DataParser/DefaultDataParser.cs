using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DataParser
{
    public class DefaultDataParser : IDataParser
    {
        public List<Match> ParseData(JArray matchesJson)
        {
            //Результуючий масив матчів зпарсений з json
            List<Match> matchesRes = new List<Match>();
            //Перебираєм всі матчі і запаковуєм в об'єкти
            while (matchesJson != null && matchesJson.HasValues)
            {
                Match match = new Match();
                //Мабуть це піде в окремий метод, але поітм
                JToken val = matchesJson.First;
                match.Id = (int)val["Num"];
                match.Champ = val["ChampEng"].ToString();
                match.Sportname = val["SportNameEng"].ToString();
                match.Opp1Name = val["Opp1Eng"].ToString();
                match.Opp2Name = val["Opp2Eng"].ToString();
                foreach(var ev in val["Events"])
                {
                    //todo поміняти на switch
                    switch ((int)ev["T"])
                    {
                        case 1:
                            match.Event.P1 = (double)(ev["C"] ?? 0);
                            break;
                        case 2:
                            match.Event.X = (double)(ev["C"] ?? 0);
                            break;
                        case 3:
                            match.Event.P2 = (double)(ev["C"] ?? 0);
                            break;
                        case 4:
                            match.Event.X1 = (double)(ev["C"] ?? 0);
                            break;
                        case 5:
                            match.Event.I2 = (double)(ev["C"] ?? 0);
                            break;
                        case 6:
                            match.Event.X2 = (double)(ev["C"] ?? 0);
                            break;
                        case 7:
                            match.Event.I = (double)(ev["C"] ?? 0);
                            match.Event.Fora1 = (double)(ev["P"] ?? 0);
                            break;
                        case 8:
                            match.Event.II = (double)(ev["C"] ?? 0);
                            match.Event.Fora2 = (double)(ev["P"] ?? 0);
                            break;
                        case 9:
                            match.Event.B = (double)(ev["C"] ?? 0);
                            match.Event.Total = (double)(ev["P"] ?? 0);
                            break;
                        case 10:
                            match.Event.M = (double)(ev["C"] ?? 0);
                            match.Event.Total = (double)(ev["P"] ?? 0);
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
