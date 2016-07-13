using DataParser.Enums;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolsPortable;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeForkFormulas
    {
        public bool CheckIsFork(double? coef1, double? coef2, ResultForForks marEvent, ResultForForksDictionary pinEvent, Tuple<string,string> list)
        {
            if (CheckIsMustToBeRevert(marEvent.Event, pinEvent.TeamNames))
            {
                coef2 = pinEvent.TypeCoefDictionary[CoefsWhichMustBeRevert.revertCoefs[marEvent.Type]];
            }

            return coef1 != null &&
            coef2 != null &&
            Math.Abs(coef1.Value) > 0.01 &&
            Math.Abs(coef2.Value) > 0.01 &&
            1 > 1 / coef1.Value + 1 / coef2.Value &&
            1 / coef1.Value + 1 / coef2.Value > 0.8d;

        }
        private bool CheckIsMustToBeRevert(string eventMarafon, string eventPinacle)
        {
            if (Extentions.GetStringSimilarityInPercent(eventMarafon.Split('-')[0],
                eventPinacle.Split('-')[0], true) >= 80)
            {
                return false;
            }
            else
                return true;
        }
        public double GetProfit(double? rate, double? kof1, double? kof2) => (rate != null && kof2 != null && kof1 != null)
            ? Math.Round(rate.Value / (kof1.Value + kof2.Value) * (kof1.Value * kof2.Value), 2)
            : 0d;

        public List<Fork> GetAllForksDictionary(Dictionary<string, ResultForForksDictionary> pinnacle,
            List<ResultForForks> marathon)
        {
            var resList = new List<Fork>();
            //marathon = AddAsian(marathon);
            foreach (var eventItem in marathon)
            {
                var pinKey = pinnacle.Keys.FirstOrDefault(key =>
                    Extentions.GetStringSimilarityInPercent(eventItem.Event, key, true) >= 90);
                if (pinKey == null) continue;

                try
                {
                    var pinEventKey = IsAnyForkSoccer(eventItem, pinnacle[pinKey], eventItem.SportType.EnumParse<SportType>());
                    if (pinEventKey.IsNotBlank())
                        resList.Add(new Fork
                        {
                            Event = pinnacle[pinKey].TeamNames,
                            TypeFirst = eventItem.Type,
                            CoefFirst = eventItem.Coef,
                            TypeSecond = pinEventKey.ConvertToStringOrNull(),
                            CoefSecond = pinnacle[pinKey].TypeCoefDictionary[pinEventKey.ConvertToStringOrNull()].ConvertToStringOrNull(),
                            Sport = eventItem.SportType,
                            MatchDateTime = pinnacle[pinKey].MatchDateTime.ToString(),
                            BookmakerFirst = "https://www.marathonbet.com/",
                            BookmakerSecond = "http://www.pinnaclesports.com/",
                            Type = ForkType.Current
                        });
                }
                catch
                {
                    // ingored
                }
            }
            return resList;
        }

        private List<ResultForForks> AddAsian(List<ResultForForks> marathon)
        {
            var result = new List<ResultForForks>();
            ResultForForks zero = null;
            ResultForForks five = null;
            bool isZero = false;
            foreach (var r in marathon)
            {
                result.Add(r);
                if (r.Type.EndsWith("0)"))
                {
                    isZero = true;
                    zero = r;
                }
                if (r.Type.EndsWith("5)"))
                {
                    isZero = false;
                    five = r;
                }
                if (zero != null && five != null)
                {
                    if (zero.Event == five.Event)
                    {
                        if (isZero)
                            zero.Type = zero.Type.Remove(zero.Type.Length - 2) + "75)";
                        else
                            zero.Type = zero.Type.Remove(zero.Type.Length - 2) + "25)";
                        zero.Coef = ((zero.Coef.ConvertToDoubleOrNull() + five.Coef.ConvertToDoubleOrNull()) / 2).ToString();
                        result.Add(zero);
                    }
                }
            }
            return result;
        }

        private string IsAnyForkSoccer(ResultForForks marEvent, ResultForForksDictionary pinEvent, SportType st)
        {
            marEvent.Type = marEvent.Type.Trim();

            switch (st)
            {
                case SportType.Soccer:
                        foreach (var list in SportTypes.TypeListSoccer)
                        {
                            if (marEvent.Type == list.Item1 && pinEvent.TypeCoefDictionary.ContainsKey(list.Item2) &&
                                CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[list.Item2], marEvent, pinEvent, list))
                                return list.Item2;
                        }
                    break;

                case SportType.Tennis:
                        foreach (var list in SportTypes.TypeListTennis)
                        {
                            if (marEvent.Type == list.Item1 && pinEvent.TypeCoefDictionary.ContainsKey(list.Item2) &&
                                CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[list.Item2], marEvent, pinEvent, list))
                                return list.Item2;
                        }
                    break;

                default:
                    SportType.NoType.ToString();
                    break;
        }

            //#region Wins

            ///**************************************************************************************/

            //if (marEvent.Type == "12" && pinEvent.TypeCoefDictionary.ContainsKey("X") &&
            //CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["X"]))
            //    return "X";

            //if (marEvent.Type == "X2" && pinEvent.TypeCoefDictionary.ContainsKey("1") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["1"]))
            //    return "1";

            //if (marEvent.Type == "1X" && pinEvent.TypeCoefDictionary.ContainsKey("2") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["2"]))
            //    return "2";
            ///**************************************************************************************/

            ///**************************************************************************************/
            //if (marEvent.Type == "X" && pinEvent.TypeCoefDictionary.ContainsKey("12") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["12"]))
            //    return "12";

            //if (marEvent.Type == "1" && pinEvent.TypeCoefDictionary.ContainsKey("X2") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["X2"]))
            //    return "X2";

            //if (marEvent.Type == "2" && pinEvent.TypeCoefDictionary.ContainsKey("1X") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["1X"]))
            //    return "1X";

            ///**************************************************************************************/

            //#endregion Wins

            //#region Odds

            //#region 0 to 0

            ///**************************************************************************************/
            //if (marEvent.Type == "F1(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0)"]))
            //    return "F2(0)";

            //if (marEvent.Type == "F2(0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0)"]))
            //    return "F1(0)";
            ///**************************************************************************************/

            //#endregion 0 to 0

            //#region -0.25 to 0.25

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.25)"]))
            //    return "F2(0.25)";

            //if (marEvent.Type == "F2(0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.25)"]))
            //    return "F1(-0.25)";

            //if (marEvent.Type == "F1(0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.25)"]))
            //    return "F2(-0.25)";

            //if (marEvent.Type == "F2(-0.25)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.25)"]))
            //    return "F1(0.25)";

            ///**************************************************************************************/

            //#endregion -0.25 to 0.25

            //#region -0.5 to 0.5

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.5)"]))
            //    return "F2(0.5)";

            //if (marEvent.Type == "F2(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.5)"]))
            //    return "F1(-0.5)";

            //if (marEvent.Type == "F2(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.5)"]))
            //    return "F1(0.5)";

            //if (marEvent.Type == "F1(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.5)"]))
            //    return "F2(-0.5)";

            ///**************************************************************************************/

            //#endregion -0.5 to 0.5

            //#region -0.75 to 0.75

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(0.75)"]))
            //    return "F2(0.75)";

            //if (marEvent.Type == "F2(0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-0.75)"]))
            //    return "F1(-0.75)";

            //if (marEvent.Type == "F2(-0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(0.75)"]))
            //    return "F1(0.75)";

            //if (marEvent.Type == "F1(0.75)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-0.75)"]))
            //    return "F2(-0.75)";

            ///**************************************************************************************/

            //#endregion -0.75 to 0.75

            //#region -1 to 1

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1)"]))
            //    return "F2(1)";

            //if (marEvent.Type == "F2(1)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1)"]))
            //    return "F1(-1)";

            //if (marEvent.Type == "F2(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1)"]))
            //    return "F1(1)";

            //if (marEvent.Type == "F1(1)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1)"]))
            //    return "F2(-1)";

            ///**************************************************************************************/

            //#endregion -1 to 1

            //#region -1.25 to 1.25

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.25)"]))
            //    return "F2(1.25)";

            //if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.25)"]))
            //    return "F2(1.25)";

            //if (marEvent.Type == "F2(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.25)"]))
            //    return "F1(-1.25)";

            //if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.25)"]))
            //    return "F1(-1.25)";

            //if (marEvent.Type == "F2(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.25)"]))
            //    return "F1(1.25)";

            //if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.25)"]))
            //    return "F1(1.25)";

            //if (marEvent.Type == "F1(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.25)"]))
            //    return "F2(-1.25)";

            //if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.25)"]))
            //    return "F2(-1.25)";

            ///**************************************************************************************/

            //#endregion -1.25 to 1.25

            //#region -1.5 to 1.5

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.5)"]))
            //    return "F2(1.5)";

            //if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.5)"]))
            //    return "F1(-1.5)";

            //if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.5)"]))
            //    return "F1(1.5)";

            //if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.5)"]))
            //    return "F2(-1.5)";

            ///**************************************************************************************/

            //#endregion -1.5 to 1.5

            //#region -1.75 to 1.75

            ///**************************************************************************************/

            //if (marEvent.Type == "F1(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.75)"]))
            //    return "F2(1.75)";

            //if (marEvent.Type == "F1(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(1.75)"]))
            //    return "F2(1.75)";

            //if (marEvent.Type == "F2(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.75)"]))
            //    return "F1(-1.75)";

            //if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(-1.75)"]))
            //    return "F1(-1.75)";

            //if (marEvent.Type == "F2(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.75)"]))
            //    return "F1(1.75)";

            //if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F1(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F1(1.75)"]))
            //    return "F1(1.75)";

            //if (marEvent.Type == "F1(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.75)"]))
            //    return "F2(-1.75)";

            //if (marEvent.Type == "F1(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-1.75)"]))
            //    return "F2(-1.75)";

            ///**************************************************************************************/

            //#endregion -1.75 to 1.75

            //#region -2 to 2

            ///**************************************************************************************/

            //if (marEvent.Type == "F2(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2)"]))
            //    return "F2(2)";

            //if (marEvent.Type == "F2(2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2)"]))
            //    return "F2(-2)";

            //if (marEvent.Type == "F2(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2)"]))
            //    return "F2(2)";

            //if (marEvent.Type == "F2(2)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2)"]))
            //    return "F2(-2)";

            ///**************************************************************************************/

            //#endregion -2 to 2

            //#region -2.25 to 2.25

            ///**************************************************************************************/

            //if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
            //    return "F2(2.25)";

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
            //    return "F2(2.25)";

            //if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
            //    return "F2(-2.25)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
            //    return "F2(-2.25)";

            //if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
            //    return "F2(2.25)";

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.25)"]))
            //    return "F2(2.25)";

            //if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
            //    return "F2(-2.25)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.25)"]))
            //    return "F2(-2.25)";

            ///**************************************************************************************/

            //#endregion -2.25 to 2.25

            //#region -2.5 to 2.5

            ///**************************************************************************************/

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.5)"]))
            //    return "F2(2.5)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.5)"]))
            //    return "F2(-2.5)";

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.5)"]))
            //    return "F2(2.5)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.5)"]))
            //    return "F2(-2.5)";

            ///**************************************************************************************/

            //#endregion -2.5 to 2.5

            //#region -2.75 to 2.75

            ///**************************************************************************************/

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
            //    return "F2(2.75)";

            //if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
            //    return "F2(2.75)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
            //    return "F2(-2.75)";

            //if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
            //    return "F2(-2.75)";

            //if (marEvent.Type == "F2(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
            //    return "F2(2.75)";

            //if (marEvent.Type == "F2(-2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(2.75)"]))
            //    return "F2(2.75)";

            //if (marEvent.Type == "F2(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
            //    return "F2(-2.75)";

            //if (marEvent.Type == "F2(2.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F2(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F2(-2.75)"]))
            //    return "F2(-2.75)";

            ///**************************************************************************************/

            //#endregion -2.75 to 2.75

            //#region -3 to 3

            ///**************************************************************************************/

            //if (marEvent.Type == "F3(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3)"]))
            //    return "F3(3)";

            //if (marEvent.Type == "F3(3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3)"]))
            //    return "F3(-3)";

            //if (marEvent.Type == "F3(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3)"]))
            //    return "F3(3)";

            //if (marEvent.Type == "F3(3)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3)"]))
            //    return "F3(-3)";

            ///**************************************************************************************/

            //#endregion -3 to 3

            //#region -3.25 to 3.25

            ///**************************************************************************************/

            //if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
            //    return "F3(3.25)";

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
            //    return "F3(3.25)";

            //if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
            //    return "F3(-3.25)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
            //    return "F3(-3.25)";

            //if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
            //    return "F3(3.25)";

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.25)"]))
            //    return "F3(3.25)";

            //if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
            //    return "F3(-3.25)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.25)"]))
            //    return "F3(-3.25)";
            ///**************************************************************************************/

            //#endregion -3.25 to 3.25

            //#region -3.5 to 3.5

            ///**************************************************************************************/

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.5)"]))
            //    return "F3(3.5)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.5)"]))
            //    return "F3(-3.5)";

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.5)"]))
            //    return "F3(3.5)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.5)"]))
            //    return "F3(-3.5)";

            ///**************************************************************************************/

            //#endregion -3.5 to 3.5

            //#region -3.75 to 3.75

            ///**************************************************************************************/

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
            //    return "F3(3.75)";

            //if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
            //    return "F3(3.75)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
            //    return "F3(-3.75)";

            //if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
            //    return "F3(-3.75)";

            //if (marEvent.Type == "F3(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
            //    return "F3(3.75)";

            //if (marEvent.Type == "F3(-3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(3.75)"]))
            //    return "F3(3.75)";

            //if (marEvent.Type == "F3(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
            //    return "F3(-3.75)";

            //if (marEvent.Type == "F3(3.0)" && pinEvent.TypeCoefDictionary.ContainsKey("F3(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["F3(-3.75)"]))
            //    return "F3(-3.75)";

            ///**************************************************************************************/

            //#endregion -3.75 to 3.75

            //#endregion Odds

            //#region Totals

            //#region 0 to 0

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0)"]))
            //    return "TU(0)";

            //if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0)"]))
            //    return "TO(0)";

            ///**************************************************************************************/

            //#endregion 0 to 0

            //#region -0.25 to 0.25

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.25)"]))
            //    return "TU(0.25)";

            //if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.25)"]))
            //    return "TU(0.25)";

            //if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.25)"]))
            //    return "TO(-0.25)";

            //if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.25)"]))
            //    return "TO(-0.25)";

            //if (marEvent.Type == "TU(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.25)"]))
            //    return "TO(0.25)";

            //if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.25)"]))
            //    return "TO(0.25)";

            //if (marEvent.Type == "TO(0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.25)"]))
            //    return "TU(-0.25)";

            //if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.25)"]))
            //    return "TU(-0.25)";

            ///**************************************************************************************/

            //#endregion -0.25 to 0.25

            //#region -0.5 to 0.5

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.5)"]))
            //    return "TU(0.5)";

            //if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.5)"]))
            //    return "TO(-0.5)";

            //if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.5)"]))
            //    return "TO(0.5)";

            //if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.5)"]))
            //    return "TU(-0.5)";

            ///**************************************************************************************/

            //#endregion -0.5 to 0.5

            //#region -0.75 to 0.75

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.75)"]))
            //    return "TU(0.75)";

            //if (marEvent.Type == "TO(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(0.75)"]))
            //    return "TU(0.75)";

            //if (marEvent.Type == "TU(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.75)"]))
            //    return "TO(-0.75)";

            //if (marEvent.Type == "TU(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-0.75)"]))
            //    return "TO(-0.75)";

            //if (marEvent.Type == "TU(-0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.75)"]))
            //    return "TO(0.75)";

            //if (marEvent.Type == "TU(-1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(0.75)"]))
            //    return "TO(0.75)";

            //if (marEvent.Type == "TO(0.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.75)"]))
            //    return "TU(-0.75)";

            //if (marEvent.Type == "TO(1.0)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-0.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-0.75)"]))
            //    return "TU(-0.75)";

            ///**************************************************************************************/

            //#endregion -0.75 to 0.75

            //#region -1 to 1

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1)"]))
            //    return "TU(1)";

            //if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1)"]))
            //    return "TO(-1)";

            //if (marEvent.Type == "TU(-1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1)"]))
            //    return "TO(1)";

            //if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1)"]))
            //    return "TU(-1)";

            ///**************************************************************************************/

            //#endregion -1 to 1

            //#region -1.25 to 1.25

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.25)"]))
            //    return "TU(1.25)";

            //if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.25)"]))
            //    return "TU(1.25)";

            //if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.25)"]))
            //    return "TO(-1.25)";

            //if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.25)"]))
            //    return "TO(-1.25)";

            //if (marEvent.Type == "TU(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.25)"]))
            //    return "TO(1.25)";

            //if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.25)"]))
            //    return "TO(1.25)";

            //if (marEvent.Type == "TO(1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.25)"]))
            //    return "TU(-1.25)";

            //if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.25)"]))
            //    return "TU(-1.25)";

            ///**************************************************************************************/

            //#endregion -1.25 to 1.25

            //#region -1.5 to 1.5

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.5)"]))
            //    return "TU(1.5)";

            //if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.5)"]))
            //    return "TO(-1.5)";

            //if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.5)"]))
            //    return "TO(1.5)";

            //if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.5)"]))
            //    return "TU(-1.5)";

            ///**************************************************************************************/

            //#endregion -1.5 to 1.5

            //#region -1.75 to 1.75

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.75)"]))
            //    return "TU(1.75)";

            //if (marEvent.Type == "TO(-1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(1.75)"]))
            //    return "TU(1.75)";

            //if (marEvent.Type == "TU(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.75)"]))
            //    return "TO(-1.75)";

            //if (marEvent.Type == "TU(1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-1.75)"]))
            //    return "TO(-1.75)";

            //if (marEvent.Type == "TU(-1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.75)"]))
            //    return "TO(1.75)";

            //if (marEvent.Type == "TU(-1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(1.75)"]))
            //    return "TO(1.75)";

            //if (marEvent.Type == "TO(1.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.75)"]))
            //    return "TU(-1.75)";

            //if (marEvent.Type == "TO(1.1)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-1.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-1.75)"]))
            //    return "TU(-1.75)";

            ///**************************************************************************************/

            //#endregion -1.75 to 1.75

            //#region -2 to 2

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2)"]))
            //    return "TU(2)";

            //if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2)"]))
            //    return "TO(-2)";

            //if (marEvent.Type == "TU(-2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2)"]))
            //    return "TO(2)";

            //if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2)"]))
            //    return "TU(-2)";

            ///**************************************************************************************/

            //#endregion -2 to 2

            //#region -2.25 to 2.25

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.25)"]))
            //    return "TU(2.25)";

            //if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.25)"]))
            //    return "TU(2.25)";

            //if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.25)"]))
            //    return "TO(-2.25)";

            //if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.25)"]))
            //    return "TO(-2.25)";

            //if (marEvent.Type == "TU(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.25)"]))
            //    return "TO(2.25)";

            //if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.25)"]))
            //    return "TO(2.25)";

            //if (marEvent.Type == "TO(2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.25)"]))
            //    return "TU(-2.25)";

            //if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.25)"]))
            //    return "TU(-2.25)";

            ///**************************************************************************************/

            //#endregion -2.25 to 2.25

            //#region -2.5 to 2.5

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.5)"]))
            //    return "TU(2.5)";

            //if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.5)"]))
            //    return "TO(-2.5)";

            //if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.5)"]))
            //    return "TO(2.5)";

            //if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.5)"]))
            //    return "TU(-2.5)";

            ///**************************************************************************************/

            //#endregion -2.5 to 2.5

            //#region -2.75 to 2.75

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.75)"]))
            //    return "TU(2.75)";

            //if (marEvent.Type == "TO(-2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(2.75)"]))
            //    return "TU(2.75)";

            //if (marEvent.Type == "TU(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.75)"]))
            //    return "TO(-2.75)";

            //if (marEvent.Type == "TU(2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-2.75)"]))
            //    return "TO(-2.75)";

            //if (marEvent.Type == "TU(-2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.75)"]))
            //    return "TO(2.75)";

            //if (marEvent.Type == "TU(-2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(2.75)"]))
            //    return "TO(2.75)";

            //if (marEvent.Type == "TO(2.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.75)"]))
            //    return "TU(-2.75)";

            //if (marEvent.Type == "TO(2.2)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-2.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-2.75)"]))
            //    return "TU(-2.75)";

            ///**************************************************************************************/

            //#endregion -2.75 to 2.75

            //#region -3 to 3

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3)"]))
            //    return "TU(3)";

            //if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3)"]))
            //    return "TO(-3)";

            //if (marEvent.Type == "TU(-3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3)"]))
            //    return "TO(3)";

            //if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3)"]))
            //    return "TU(-3)";

            ///**************************************************************************************/

            //#endregion -3 to 3

            //#region -3.25 to 3.25

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.25)"]))
            //    return "TU(3.25)";

            //if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.25)"]))
            //    return "TU(3.25)";

            //if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.25)"]))
            //    return "TO(-3.25)";

            //if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.25)"]))
            //    return "TO(-3.25)";

            //if (marEvent.Type == "TU(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.25)"]))
            //    return "TO(3.25)";

            //if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.25)"]))
            //    return "TO(3.25)";

            //if (marEvent.Type == "TO(3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.25)"]))
            //    return "TU(-3.25)";

            //if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.25)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.25)"]))
            //    return "TU(-3.25)";

            ///**************************************************************************************/

            //#endregion -3.25 to 3.25

            //#region -3.5 to 3.5

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.5)"]))
            //    return "TU(3.5)";

            //if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.5)"]))
            //    return "TO(-3.5)";

            //if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.5)"]))
            //    return "TO(3.5)";

            //if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.5)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.5)"]))
            //    return "TU(-3.5)";

            ///**************************************************************************************/

            //#endregion -3.5 to 3.5

            //#region -3.75 to 3.75

            ///**************************************************************************************/

            //if (marEvent.Type == "TO(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.75)"]))
            //    return "TU(3.75)";

            //if (marEvent.Type == "TO(-3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(3.75)"]))
            //    return "TU(3.75)";

            //if (marEvent.Type == "TU(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.75)"]))
            //    return "TO(-3.75)";

            //if (marEvent.Type == "TU(3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(-3.75)"]))
            //    return "TO(-3.75)";

            //if (marEvent.Type == "TU(-3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.75)"]))
            //    return "TO(3.75)";

            //if (marEvent.Type == "TU(-3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TO(3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TO(3.75)"]))
            //    return "TO(3.75)";

            //if (marEvent.Type == "TO(3.5)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.75)"]))
            //    return "TU(-3.75)";

            //if (marEvent.Type == "TO(3.3)" && pinEvent.TypeCoefDictionary.ContainsKey("TU(-3.75)") &&
            //    CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary["TU(-3.75)"]))
            //    return "TU(-3.75)";

            ///**************************************************************************************/

            //#endregion -3.75 to 3.75

            //#endregion Totals

            return null;
        }
    }
}