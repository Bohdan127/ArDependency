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

        private bool CheckIsTheSameEvents(string eventMarafon, string eventPinacle)
        {
            string evP1 = eventPinacle.Split('-')[0];
            string evP2 = eventPinacle.Split('-')[1];
            string evM1 = eventMarafon.Split('-')[0];
            string evM2 = eventMarafon.Split('-')[1];
            evP1 = evP1.Split(' ')[0];
            evP2 = evP2.Split(' ')[1];
            evM1 = evM1.Split(' ')[0];
            evM2 = evM2.Split(' ')[1];
            if (evP1.Equals(evM1) && evP2.Equals(evM2))
                return false;
            else if (evP1.Equals(evM2) && evP2.Equals(evM1))
            {
                return true;
            }
            else return false;
        }

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

                #region Wins
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
                    //{
                    //    if (!CheckIsTheSameEvents(marEvent.Event, pinEvent.TeamNames)) // in working
                    //        return "1";
                    //    else if (CheckIsTheSameEvents(marEvent.Event, pinEvent.TeamNames))
                    //    {
                    //        marEvent.Type = "1X";
                    //    }
                    //}
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
                #endregion

                #region Odds

                #region 0 to 0
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
                #endregion

                #region  -0.25 to 0.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.25)"]))    
                        return "F2(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.25)"]))  
                        return "F2(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.25)"]))   
                        return "F1(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }                            

                try
                {
                    if (marEvent.Type == "F2(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.25)"]))   
                        return "F1(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.25)"]))
                        return "F1(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.25)"]))
                        return "F1(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.25)"]))
                        return "F2(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.25)"]))
                        return "F2(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -0.5 to 0.5
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
                #endregion

                #region -0.75 to 0.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.75)"]))
                        return "F2(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.75)"]))
                        return "F2(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.75)"]))
                        return "F1(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.75)"]))
                        return "F1(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.75)"]))
                        return "F1(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "F2(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.75)"]))
                        return "F1(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.75)"]))
                        return "F2(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.75)"]))
                        return "F2(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1 to 1
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
                #endregion

                #region -1.25 to 1.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.25)"]))
                        return "F2(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.25)"]))
                        return "F2(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.25)"]))
                        return "F1(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.25)"]))
                        return "F1(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.25)"]))
                        return "F1(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.25)"]))
                        return "F1(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.25)"]))
                        return "F2(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.25)"]))
                        return "F2(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1.5 to 1.5
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
                catch (Exception ex)
                {
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
                #endregion

                #region -1.75 to 1.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.75)"]))
                        return "F2(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.75)"]))
                        return "F2(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.75)"]))
                        return "F1(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.75)"]))
                        return "F1(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.75)"]))
                        return "F1(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.75)"]))
                        return "F1(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.75)"]))
                        return "F2(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F1(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.75)"]))
                        return "F2(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2 to 2
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F2(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2)"]))
                        return "F2(2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2)"]))
                        return "F2(-2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2)"]))
                        return "F2(2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2)"]))
                        return "F2(-2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.25 to 2.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
                        return "F2(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
                        return "F2(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
                        return "F2(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
                        return "F2(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
                        return "F2(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
                        return "F2(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
                        return "F2(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
                        return "F2(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.5 to 2.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.5)"]))
                        return "F2(2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.5)"]))
                        return "F2(-2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.5)"]))
                        return "F2(2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.5)"]))
                        return "F2(-2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.75 to 2.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
                        return "F2(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
                        return "F2(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
                        return "F2(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
                        return "F2(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
                        return "F2(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
                        return "F2(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
                        return "F2(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
                        return "F2(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3 to 3
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F3(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3)"]))
                        return "F3(3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3)"]))
                        return "F3(-3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3)"]))
                        return "F3(3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3)"]))
                        return "F3(-3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.25 to 3.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
                        return "F3(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
                        return "F3(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
                        return "F3(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
                        return "F3(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
                        return "F3(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
                        return "F3(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
                        return "F3(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
                        return "F3(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.5 to 3.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.5)"]))
                        return "F3(3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.5)"]))
                        return "F3(-3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.5)"]))
                        return "F3(3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.5)"]))
                        return "F3(-3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.75 to 3.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
                        return "F3(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
                        return "F3(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
                        return "F3(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
                        return "F3(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
                        return "F3(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
                        return "F3(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
                        return "F3(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
                        return "F3(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #endregion

                #region Totals

                #region 0 to 0
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0)"]))
                        return "TU(0)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0)"]))
                        return "TO(0)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -0.25 to 0.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.25)"]))
                        return "TU(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.25)"]))
                        return "TU(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.25)"]))
                        return "TO(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.25)"]))
                        return "TO(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.25)"]))
                        return "TO(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.25)"]))
                        return "TO(0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.25)"]))
                        return "TU(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.25)"]))
                        return "TU(-0.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -0.5 to 0.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.5)"]))
                        return "TU(0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.5)"]))
                        return "TO(-0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.5)"]))
                        return "TO(0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.5)"]))
                        return "TU(-0.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -0.75 to 0.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.75)"]))
                        return "TU(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.75)"]))
                        return "TU(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.75)"]))
                        return "TO(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.75)"]))
                        return "TO(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.75)"]))
                        return "TO(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.75)"]))
                        return "TO(0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.75)"]))
                        return "TU(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.75)"]))
                        return "TU(-0.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1 to 1
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1)"]))
                        return "TU(1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1)"]))
                        return "TO(-1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1)"]))
                        return "TO(1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1)"]))
                        return "TU(-1)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1.25 to 1.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.25)"]))
                        return "TU(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.25)"]))
                        return "TU(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.25)"]))
                        return "TO(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.25)"]))
                        return "TO(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.25)"]))
                        return "TO(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.25)"]))
                        return "TO(1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.25)"]))
                        return "TU(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.25)"]))
                        return "TU(-1.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1.5 to 1.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.5)"]))
                        return "TU(1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.5)"]))
                        return "TO(-1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.5)"]))
                        return "TO(1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.5)"]))
                        return "TU(-1.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -1.75 to 1.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.75)"]))
                        return "TU(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.75)"]))
                        return "TU(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.75)"]))
                        return "TO(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.75)"]))
                        return "TO(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.75)"]))
                        return "TO(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.75)"]))
                        return "TO(1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.75)"]))
                        return "TU(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.75)"]))
                        return "TU(-1.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion   

                #region -2 to 2
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2)"]))
                        return "TU(2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2)"]))
                        return "TO(-2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2)"]))
                        return "TO(2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2)"]))
                        return "TU(-2)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.25 to 2.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.25)"]))
                        return "TU(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.25)"]))
                        return "TU(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.25)"]))
                        return "TO(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.25)"]))
                        return "TO(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.25)"]))
                        return "TO(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.25)"]))
                        return "TO(2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.25)"]))
                        return "TU(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.25)"]))
                        return "TU(-2.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.5 to 2.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.5)"]))
                        return "TU(2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.5)"]))
                        return "TO(-2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.5)"]))
                        return "TO(2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.5)"]))
                        return "TU(-2.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -2.75 to 2.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.75)"]))
                        return "TU(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.75)"]))
                        return "TU(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.75)"]))
                        return "TO(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.75)"]))
                        return "TO(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.75)"]))
                        return "TO(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.75)"]))
                        return "TO(2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.75)"]))
                        return "TU(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.75)"]))
                        return "TU(-2.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3 to 3
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3)"]))
                        return "TU(3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3)"]))
                        return "TO(-3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3)"]))
                        return "TO(3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3)"]))
                        return "TU(-3)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.25 to 3.25
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.25)"]))
                        return "TU(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.25)"]))
                        return "TU(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.25)"]))
                        return "TO(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.25)"]))
                        return "TO(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.25)"]))
                        return "TO(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.25)"]))
                        return "TO(3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.25)"]))
                        return "TU(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.25)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.25)"]))
                        return "TU(-3.25)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.5 to 3.5
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.5)"]))
                        return "TU(3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.5)"]))
                        return "TO(-3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.5)"]))
                        return "TO(3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.5)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.5)"]))
                        return "TU(-3.5)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion

                #region -3.75 to 3.75
                /**************************************************************************************/
                try
                {
                    if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.75)"]))
                        return "TU(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(-3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.75)"]))
                        return "TU(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.75)"]))
                        return "TO(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.75)"]))
                        return "TO(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.75)"]))
                        return "TO(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TU(-3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.75)"]))
                        return "TO(3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }


                try
                {
                    if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.75)"]))
                        return "TU(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }

                try
                {
                    if (marEvent.Type == "TO(3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.75)") &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.75)"]))
                        return "TU(-3.75)";
                }
                catch (Exception ex)
                {
                    //ignored
                }
                /**************************************************************************************/
                #endregion   

                #endregion
            }
            catch (Exception ex)
            {
                //ignored
            }
            return null;
        }
    }
}
