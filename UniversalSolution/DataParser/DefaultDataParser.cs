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
                    if((int)ev["T"] == 1){
                        match.Event.P1 = (double)(ev["C"] ?? 0);
                    }
                }
                matchesRes.Add(match);
                matchesJson.Remove(val);
            }
            return matchesRes;
        }
        
    }
}
