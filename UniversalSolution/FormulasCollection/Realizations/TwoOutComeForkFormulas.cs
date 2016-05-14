using FormulasCollection.Interfaces;
using System;
using System.Collections.Generic;
using Tools;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeForkFormulas : IForkFormulas
    {

        public bool CheckIsFork(double? coef1, double? coef2) => 
            coef1 != null && 
            coef2 != null && 
            Math.Abs(coef1.Value) > 0.01 && 
            Math.Abs(coef2.Value) > 0.01 && 
            1 > 1 / coef1.Value + 1 / coef2.Value;


        public double getProfit(double rate, double kof1, double kof2)
        {
            return rate / (kof1 + kof2) * (kof1 * kof2);
        }

        public List<Fork> GetAllForks(List<ResultForForks> marafon, List<ResultForForks> pinacle)
        {
            List<Fork> buffDic = new List<Fork>();
            foreach (var buff in marafon)
            {
                foreach (var buff2 in pinacle)
                {
                    try
                    {
                        if (isTheSame(buff.Event, buff2.Event) && checkForType(buff.Type.Trim(), buff2.Type.Trim()) && CheckIsFork(buff.Coef.ConvertToDoubleOrNull(), buff2.Coef.ConvertToDoubleOrNull()))
                        {
                            buffDic.Add(new Fork()
                            {
                                Event = buff.Event,
                                TypeFirst = buff.Type,
                                CoefFirst = buff.Coef,
                                TypeSecond = buff2.Type,
                                CoefSecond = buff2.Coef,
                                Sport = buff.SportType,
                                MatchDateTime = buff2.MatchDateTime,
                                BookmakerFirst = buff.Bookmaker,
                                BookmakerSecond = buff2.Bookmaker,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        //ignored
                    }
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
            //if (type1 == "Меньше" && type2 == "Больше")
            //    return true;
            //if (type2 == "Меньше" && type1 == "Больше")
            //    return true;
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
