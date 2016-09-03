﻿using DataParser.Enums;
using FormulasCollection.Enums;
using FormulasCollection.Helpers;
using FormulasCollection.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ToolsPortable;

namespace FormulasCollection.Realizations
{
    public class TwoOutComeForkFormulas
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
        private bool CheckIsMustToBeRevert(string eventMarafon, string eventPinacle) =>
        Extentions.GetStringSimilarityInPercent(eventMarafon.Split('-')[0],
                eventPinacle.Split('-')[0], true) < 90;

        public List<Fork> GetAllForksDictionary(Dictionary<string, ResultForForksDictionary> pinnacle,
            List<ResultForForks> marathon)
        {
            var resList = new List<Fork>();
            foreach (var eventItem in marathon)
            {
                var pinKey = pinnacle.Keys.FirstOrDefault(key =>
                    Extentions.GetStringSimilarityInPercent(eventItem.Event, key, true) >= 85);
                if (pinKey == null)
                    continue;

                var pin = pinnacle[pinKey];
                try
                {
                    var pinEventKey = IsAnyForkAll(eventItem, pinnacle[pinKey], eventItem.SportType.EnumParse<SportType>());
                    if (pinEventKey == null) continue;
                    if (pinEventKey.IsNotBlank())
                    {
                        resList.Add(new Fork
                        {
                            League = eventItem.League,
                            EventId = eventItem.EventId,
                            Event = eventItem.Event,
                            TypeFirst = eventItem.Type,
                            CoefFirst = eventItem.Coef,
                            TypeSecond = pinEventKey.ConvertToStringOrNull(),
                            CoefSecond = pin.TypeCoefDictionary[pinEventKey].ConvertToStringOrNull(),
                            Sport = eventItem.SportType,
                            MatchDateTime = pin.MatchDateTime.ToString(CultureInfo.CurrentCulture),
                            BookmakerFirst = "https://www.marathonbet.com/",
                            BookmakerSecond = "http://www.pinnaclesports.com/",
                            Type = ForkType.Current,
                            LineId = pin.TypeLineIdDictionary[pinEventKey]
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                }
            }
            return resList;
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
                    string buffBasketBall = SportsConverterTypes.TypeParseAll(marEvent.Type, st);
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