using DataParser.Enums;
using FormulasCollection.Enums;
using FormulasCollection.Helpers;
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
                if(CoefsWhichMustBeRevert.revertCoefs.ContainsKey(marEvent.Type))
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
                    var pinEventKey = IsAnyForkAll(eventItem, pinnacle[pinKey], eventItem.SportType.EnumParse<SportType>());
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

        private string IsAnyForkAll(ResultForForks marEvent, ResultForForksDictionary pinEvent, SportType st)
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

                case SportType.Basketball:
                        foreach (var list in SportTypes.TypeListBasketBall)
                        {
                        /*
                        if (marEvent.Type == list.Item1 && pinEvent.TypeCoefDictionary.ContainsKey(list.Item2) && move to 
                        pinEvent.TypeCoefDictionary.ContainsKey(SportsConverterTypes.TypeParseBasketball(marEvent.Type))
                        not checked but should be true, easer than write so many strings code
                        */

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
            return null;
        }
    }
}