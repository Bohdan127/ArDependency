using DataParser.Enums;
using FormulasCollection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeForkFormulas : IForkFormulas
    {

        public double defaultRate = 0.0;

        public bool CheckIsFork(double? coef1, double? coef2)
        {
            if (coef1 == 0 || coef2 == 0)
                return false;
            return 1 > (1 / coef1 + 1 / coef2);
        }

        public double getProfit(double rate, double kof1, double kof2)
        {
            return (((rate * 2) / (kof1 + kof2)) * (kof1 * kof2));
        }

        public Dictionary<string, Fork> GetAllForks(Dictionary<string, ResultForForks> marafon, Dictionary<string, ResultForForks> pinacle)
        {
            Dictionary<string, Fork> buffDic = new Dictionary<string, Fork>();

            foreach (KeyValuePair<string, ResultForForks> buff in marafon)
            {
                try
                {
                    if (pinacle.ContainsKey(buff.Key))
                    {
                        if (isTheSame(buff.Value.Event, pinacle[buff.Key].Event) &&
                            checkForType(buff.Value.Type.Trim(), pinacle[buff.Key].Type.Trim()) &&
                            CheckIsFork(Double.Parse(buff.Value.Coef), Double.Parse(pinacle[buff.Key].Coef)))
                        {
                            buffDic.Add(buff.Key,
                                new Fork() { 
                                Event = buff.Value.Event,
                                TypeFirst = buff.Value.Type,
                                CoefFirst = buff.Value.Coef,
                                TypeSecond = pinacle[buff.Key].Type,
                                CoefSecond = pinacle[buff.Key].Coef,
                                Sport = buff.Value.SportType,
                                MatchDateTime = pinacle[buff.Key].MatchDateTime,
                                BookmakerFirst = buff.Value.Bookmaker,
                                BookmakerSecond = pinacle[buff.Key].Bookmaker,
                                Profit = getProfit(defaultRate, Double.Parse(buff.Value.Coef), Double.Parse(pinacle[buff.Key].Coef)).ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ignored
                }
            }
            return buffDic;
        }
        public bool checkForType(string type1, string type2)
        {
            if (type1 == "1")
            {
                switch (type2)
                {
                    case "X2":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type1 == "2")
            {
                switch (type2)
                {
                    case "X1":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type1 == "1X")
            {
                switch (type2)
                {
                    case "2":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type1 == "2X")
            {
                switch (type2)
                {
                    case "1":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type2 == "1")
            {
                switch (type1)
                {
                    case "2X":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type2 == "2")
            {
                switch (type1)
                {
                    case "1X":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type2 == "1X")
            {
                switch (type1)
                {
                    case "2":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type2 == "2X")
            {
                switch (type1)
                {
                    case "1":
                        return true;
                        break;
                    default:
                        break;
                }
            }
            if (type1 == "Меньше" && type2 == "Больше")
                return true;
            if (type2 == "Меньше" && type1 == "Больше")
                return true;
            return false;

        }
        public bool isTheSame(string marafon, string pinacle)
        {
            string name1 = pinacle.Split('-')[0];
            name1 = name1.Split(' ')[0];
            string name2 = pinacle.Split('-')[1];
            name2 = name2.Split(' ')[1];

            if (marafon.Contains(name1) && marafon.Contains(name2))
            {
                return true;
            }
            else
                return false;
        }
    }
}
