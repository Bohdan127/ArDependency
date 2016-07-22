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
        public bool CheckIsFork(double? coef1, double? coef2, ResultForForks marEvent, ResultForForksDictionary pinEvent)
        {
            if (CheckIsMustToBeRevert(marEvent.Event, pinEvent.TeamNames))
            {
                if (CoefsWhichMustBeRevert.revertCoefs.ContainsKey(marEvent.Type))
                    coef2 = pinEvent.TypeCoefDictionary[CoefsWhichMustBeRevert.revertCoefs[marEvent.Type]];
                else
                    coef2 = pinEvent.TypeCoefDictionary[CoefsWhichMustBeRevert.TypeRevertParse(marEvent.Type)];
            }

            return coef1 != null &&
            coef2 != null &&
            Math.Abs(coef1.Value) > 0.01 &&
            Math.Abs(coef2.Value) > 0.01 &&
            1 > 1 / coef1.Value + 1 / coef2.Value;

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
            foreach (var eventItem in marathon)
            {
                var pinKey = pinnacle.Keys.FirstOrDefault(key =>
                    Extentions.GetStringSimilarityInPercent(eventItem.Event, key, true) >= 90);
                if (pinKey == null) continue;
                var pin = pinnacle[pinKey];
                try
                {
                    var pinEventKey = IsAnyForkAll(eventItem, pinnacle[pinKey], eventItem.SportType.EnumParse<SportType>());
                    if (pinEventKey == null) continue;
                    if (pinEventKey.IsNotBlank())
                    {
                        resList.Add(new Fork
                        {
                            Event = pin.TeamNames,
                            TypeFirst = eventItem.Type,
                            CoefFirst = eventItem.Coef,
                            TypeSecond = pinEventKey.ConvertToStringOrNull(),
                            CoefSecond = pin.TypeCoefDictionary[pinEventKey.ConvertToStringOrNull()].ConvertToStringOrNull(),
                            Sport = eventItem.SportType,
                            MatchDateTime = pin.MatchDateTime.ToString(),
                            BookmakerFirst = "https://www.marathonbet.com/",
                            BookmakerSecond = "http://www.pinnaclesports.com/",
                            Type = ForkType.Current
                        });
                    }
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
                    string buffSoccer = SportsConverterTypes.TypeParseAll(marEvent.Type, st);
                    if (pinEvent.TypeCoefDictionary.ContainsKey(buffSoccer) &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[buffSoccer], marEvent, pinEvent)
                        && buffSoccer != null)
                        return buffSoccer;
                    break;

                case SportType.Basketball:
                    string buffBasketBall = SportsConverterTypes.TypeParseAll(marEvent.Type,st);
                    if (pinEvent.TypeCoefDictionary.ContainsKey(buffBasketBall) &&
                                CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[buffBasketBall], marEvent, pinEvent)
                                && buffBasketBall != null)
                                return buffBasketBall;
                    break;

                case SportType.Tennis:
                    string buffTennis = SportsConverterTypes.TypeParseAll(marEvent.Type, st);
                    if (pinEvent.TypeCoefDictionary.ContainsKey(buffTennis) &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[buffTennis], marEvent, pinEvent)
                        && buffTennis != null)
                        return buffTennis;
                    break;

                case SportType.Hockey:
                    string buffHockey = SportsConverterTypes.TypeParseAll(marEvent.Type, st);
                    if (pinEvent.TypeCoefDictionary.ContainsKey(buffHockey) &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[buffHockey], marEvent, pinEvent)
                        && buffHockey != null)
                        return buffHockey;
                    break;

                case SportType.Volleyball:
                    string buffVolleyball = SportsConverterTypes.TypeParseAll(marEvent.Type, st);
                    if (pinEvent.TypeCoefDictionary.ContainsKey(buffVolleyball) &&
                        CheckIsFork(marEvent.Coef.ConvertToDoubleOrNull(), pinEvent.TypeCoefDictionary[buffVolleyball], marEvent, pinEvent)
                        && buffVolleyball != null)
                        return buffVolleyball;
                    break;

                default:
                    SportType.NoType.ToString();
                    break;
        }
            return null;
        }
    }
}