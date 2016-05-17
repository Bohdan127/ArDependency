using FormulasCollection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            1 > 1/coef1.Value + 1/coef2.Value;


        public double GetProfit(double rate, double? kof1, double? kof2) => (kof2 != null && kof1 != null)
            ? rate/(kof1.Value + kof2.Value)*(kof1.Value*kof2.Value)
            : 0d;

        public List<Fork> GetAllForks(List<ResultForForks> marafon, List<ResultForForks> pinacle)
        {
            List<Fork> buffDic = new List<Fork>();
            foreach (var buff in marafon)
            {
                foreach (var buff2 in pinacle)
                {
                    try
                    {
                        if (isTheSame(buff.Event, buff2.Event) && checkForType(buff.Type.Trim(), buff2.Type.Trim()) &&
                            CheckIsFork(buff.Coef.ConvertToDoubleOrNull(), buff2.Coef.ConvertToDoubleOrNull()))
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

        public List<Fork> GetAllForksDictionary(Dictionary<string, ResultForForksDictionary> pinnacle,
            List<ResultForForks> marathon)
        {
            var resList = new List<Fork>();

            foreach (var eventItem in marathon)
            {
                var pinKey = pinnacle.Keys.FirstOrDefault(key =>
                    Extentions.GetStringSimilarityInPercent(eventItem.Event, key, true) >= 90);
                if (pinKey != null)
                {
                    var pinEventKey = IsAnyFork(eventItem, pinnacle[pinKey]);
                    if(pinEventKey.IsNotBlank())
                        resList.Add(new Fork
                        {
                            Event = pinnacle[pinKey].TeamNames,
                            TypeFirst = eventItem.Type,
                            CoefFirst = eventItem.Coef,
                            TypeSecond = pinEventKey,
                            CoefSecond = pinnacle[pinKey].TypeCoefDictionary[pinEventKey].ConvertToStringOrNull(),
                            Sport = eventItem.SportType,
                            MatchDateTime = eventItem.MatchDateTime,
                            BookmakerFirst = "https://www.marathonbet.com/",
                            BookmakerSecond = "http://www.pinnaclesports.com/"
                        });
                }
            }

            return resList;
        }

        private string IsAnyFork(ResultForForks marEvent, ResultForForksDictionary pinEvent)
        {
            try
            {
                marEvent.Type = marEvent.Type.Trim();

                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "12" && pinEvent.TypeCoefDictionary.ContainsKey("X") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["X"]))
                        return "X";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "X2" && pinEvent.TypeCoefDictionary.ContainsKey("1") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["1"]))
                        return "1";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "1X" && pinEvent.TypeCoefDictionary.ContainsKey("2") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["2"]))
                        return "2";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "X" && pinEvent.TypeCoefDictionary.ContainsKey("12") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["12"]))
                        return "12";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "1" && pinEvent.TypeCoefDictionary.ContainsKey("X2") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["X2"]))
                        return "X2";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "2" && pinEvent.TypeCoefDictionary.ContainsKey("1X") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["1X"]))
                        return "1X";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0)"]))
                        return "F2(0)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0)"]))
                        return "F1(0)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.25)"]))
                        return "F2(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.25)"]))
                        return "F1(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.25)"]))
                        return "F1(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.25)"]))
                        return "F2(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.5)"]))
                        return "F2(0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.5)"]))
                        return "F1(-0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.5)"]))
                        return "F1(0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.5)"]))
                        return "F2(-0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.75)"]))
                        return "F2(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.75)"]))
                        return "F1(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.75)"]))
                        return "F1(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.75)"]))
                        return "F2(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1)"]))
                        return "F2(1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1)"]))
                        return "F1(-1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1)"]))
                        return "F1(1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1)"]))
                        return "F2(-1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-1.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.25)"]))
                        return "F2(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.25)"]))
                        return "F1(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.25)"]))
                        return "F1(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.25)"]))
                        return "F2(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/



                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.5)"]))
                        return "F2(1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.5)"]))
                        return "F1(-1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.5)"]))
                        return "F1(1.5)";
                }
                catch (Exception ex)                                                                                                                                       {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.5)"]))
                        return "F2(-1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
            }
            catch (Exception ex)
            {
                //ignored
            }
            return null;
        }
    }
}
