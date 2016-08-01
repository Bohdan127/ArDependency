using DataParser.Enums;
using DataParser.Models;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
using NLog;
using System;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToolsPortable;

namespace DataParser.DefaultRealization
{
    public class PinnacleSportsDataParser
    {
        private SportType _sportType = SportType.NoType;
        private readonly ConverterFormulas _converter;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public PinnacleSportsDataParser()
        {
            _converter = new ConverterFormulas();
        }

        private Dictionary<long, List<EventWithTotalDictionary>> ParseEventWithTotalsDictionaty(HttpWebResponse totalResp)
        {
            var resList = new Dictionary<long, List<EventWithTotalDictionary>>();
            try
            {
                var sportEvents = (JsonObject)JsonValue.Load(totalResp.GetResponseStream());

                if (sportEvents?["leagues"] == null) return resList;

                foreach (var league in sportEvents["leagues"])
                {
                    try
                    {
                        foreach (var sportEvent in league.Value?["events"])
                        {
                            try
                            {
                                //if id null - all ok will be exception and we go to the next item in foreach
                                var id = sportEvent.Value["id"].ConvertToLongOrNull().Value;
                                if (!resList.ContainsKey(id))
                                    resList.Add(id, new List<EventWithTotalDictionary>());

                                foreach (var period in sportEvent.Value?["periods"])
                                {
                                    var matchDateTime = period.Value?["cutoff"]?.ToString();

                                    try
                                    {
                                        resList[id].Add(new EventWithTotalDictionary
                                        {
                                            LineId = period.Value?["lineId"]?.ToString(),
                                            TotalType = "1",
                                            TotalValue = period.Value?["moneyline"]?["home"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                        resList[id].Add(new EventWithTotalDictionary
                                        {
                                            LineId = period.Value?["lineId"]?.ToString(),
                                            TotalType = "2",
                                            TotalValue = period.Value?["moneyline"]?["away"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                        resList[id].Add(new EventWithTotalDictionary
                                        {
                                            LineId = period.Value?["lineId"]?.ToString(),
                                            TotalType = "X",
                                            TotalValue = period.Value?["moneyline"]?["draw"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                                    }

                                    try
                                    {
                                        foreach (var spread in period.Value?["spreads"])
                                        {
                                            try
                                            {
                                                resList[id].Add(new EventWithTotalDictionary
                                                {
                                                    LineId = period.Value?["lineId"]?.ToString(),
                                                    TotalType = $"F2({spread.Value?["hdp"]})",
                                                    TotalValue = spread.Value?["away"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                                resList[id].Add(new EventWithTotalDictionary
                                                {
                                                    LineId = period.Value?["lineId"]?.ToString(),
                                                    TotalType = $"F1({spread.Value?["hdp"]})",
                                                    TotalValue = spread.Value?["home"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                                            }
                                        }
                                        foreach (var total in period.Value?["totals"])
                                        {
                                            try
                                            {
                                                resList[id].Add(new EventWithTotalDictionary
                                                {
                                                    LineId = period.Value?["lineId"]?.ToString(),
                                                    TotalType = $"TO({total.Value?["points"]})",
                                                    TotalValue = total.Value?["over"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                                resList[id].Add(new EventWithTotalDictionary
                                                {
                                                    LineId = period.Value?["lineId"]?.ToString(),
                                                    TotalType = $"TU({total.Value?["points"]})",
                                                    TotalValue = total.Value?["under"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
            }
            return resList;
        }

        private Dictionary<long, string> ParseEventWithNamesDictionary(HttpWebResponse teamNamesResp)
        {
            var resList = new Dictionary<long, string>();
            try
            {
                var jsonValue = ((JsonObject)JsonValue.
                    Load(teamNamesResp.GetResponseStream()))?["league"];

                if (jsonValue == null) return resList;

                foreach (var league in jsonValue)
                {
                    try
                    {
                        var sportEvents = league.Value?["events"];

                        if (sportEvents == null) continue;

                        foreach (var sportEvent in sportEvents)
                        {
                            //if id null - all ok will be exception and we go to the next item in foreach
                            var convertToLongOrNull = sportEvent.Value["id"].ConvertToLongOrNull();

                            if (convertToLongOrNull == null) continue;

                            var id = convertToLongOrNull.Value;

                            if (!resList.ContainsKey(id))
                                resList.Add(id, $"{sportEvent.Value["home"]} - {sportEvent.Value["away"]}"
                                    .Replace("\"", ""));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                        _logger.Error(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            return resList;
        }

        private Dictionary<string, ResultForForksDictionary> GroupResponsesDictionary(HttpWebResponse totalResp, HttpWebResponse teamNamesResp)
        {
            var eventsWithNames = ParseEventWithNamesDictionary(teamNamesResp);
            var eventsWithTotal = ParseEventWithTotalsDictionaty(totalResp);

            var resDic = new Dictionary<string, ResultForForksDictionary>();

            foreach (var eventWithName in eventsWithNames)
            {
                if (!eventsWithTotal.ContainsKey(eventWithName.Key)) continue;

                foreach (var eventWithTotal in eventsWithTotal[eventWithName.Key])
                {
                    try
                    {
                        //var teams = eventWithName.Value.Split('-');
                        //var key = $"{teams[0].Trim().Split(' ').First()}-{teams[1].Trim().Split(' ').First()}";
                        var key = $"{eventWithName.Value}";//-{DateTime.Parse(eventWithTotal.MatchDateTime.Replace("\"",""), CultureInfo.InvariantCulture)}";
                        if (!resDic.ContainsKey(key))
                        {
                            resDic.Add(key,
                                new ResultForForksDictionary
                                {
                                    TeamNames = eventWithName.Value,
                                    MatchDateTime = DateTime.Parse(eventWithTotal.MatchDateTime.Replace("\"", "")),
                                    TypeCoefDictionary = new Dictionary<string, double>(),
                                    TypeLineIdDictionary = new Dictionary<string, string>()
                                });
                            if (eventWithTotal.TotalType.Contains("T"))
                                eventWithTotal.TotalType = ParseTotalType(eventWithTotal.TotalType);
                            {
                                resDic[key].TypeCoefDictionary.Add(eventWithTotal.TotalType,
                                _converter.ConvertAmericanToDecimal(eventWithTotal.TotalValue.ConvertToDoubleOrNull()));
                            }
                            resDic[key].TypeLineIdDictionary.Add(eventWithTotal.TotalType, eventWithTotal.LineId);
                        }
                        else
                        {
                            if (eventWithTotal.TotalType.Contains("T"))
                                eventWithTotal.TotalType = ParseTotalType(eventWithTotal.TotalType);
                            if (!resDic[key].TypeCoefDictionary.ContainsKey(eventWithTotal.TotalType))
                                resDic[key].TypeCoefDictionary.Add(eventWithTotal.TotalType,
                                    _converter.ConvertAmericanToDecimal(
                                        eventWithTotal.TotalValue.ConvertToDoubleOrNull()));
                            if (!resDic[key].TypeLineIdDictionary.ContainsKey(eventWithTotal.TotalType))
                                resDic[key].TypeLineIdDictionary.Add(eventWithTotal.TotalType, eventWithTotal.LineId);

                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message); _logger.Error(ex.StackTrace);
                    }
                }
            }
            return resDic;
        }

        private string ParseTotalType(string totalType)
        {
            var prefix = totalType.Substring(0, 3);

            totalType = totalType.Remove(0, 3);
            totalType = totalType.Remove(totalType.Length - 1, 1);

            return prefix + Extentions.Round(_converter.ConvertAmericanToDecimal(totalType.ConvertToDoubleOrNull())) + ")";
        }

        public async Task<Dictionary<string, ResultForForksDictionary>> GetAllPinacleEventsDictionaryAsync(
            SportType sportType, string userLogin, string userPass)
        {
            var resDic = new Dictionary<string, ResultForForksDictionary>();
            _sportType = sportType;
            try
            {
                var totalResp = await GetAllTotalsAsync(userLogin, userPass).ConfigureAwait(false);
                var teamNamesResp = await GetAllTeamNamesAsync(userLogin, userPass).ConfigureAwait(false);
                resDic = GroupResponsesDictionary(totalResp, teamNamesResp);
                var listForRemove = new List<string>();

                foreach (var key in resDic.Keys)
                {
                    listForRemove.Clear();

                    foreach (var typeCoefKey in resDic[key].TypeCoefDictionary.Keys)
                    {
                        var coef = resDic[key].TypeCoefDictionary[typeCoefKey];

                        if (Math.Abs(coef) < 0.01 || Math.Abs(coef - _converter.IncorrectAmericanOdds) < 0.01)
                            listForRemove.Add(typeCoefKey);
                    }

                    foreach (var keyForRemove in listForRemove)
                        resDic[key].TypeCoefDictionary.Remove(keyForRemove);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            return resDic;
        }

        /// <summary>
        /// This function get events with their team names
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpWebResponse> GetAllTeamNamesAsync(string userLogin, string userPass)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/fixtures?sportid=" + (int)_sportType);
            string credentials = String.Format("{0}:{1}", userLogin, userPass);//for test "VB794327", "artem89@"
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.Headers.Add("Authorization", authorization);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)";
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            return
                    ((HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// This function get events with their totals
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpWebResponse> GetAllTotalsAsync(string userLogin, string userPass)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/odds?sportid=" + (int)_sportType);
            string credentials = $"{userLogin}:{userPass}";//for test "VB794327", "artem89@"
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.Headers.Add("Authorization", authorization);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)";
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            return (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
        }
    }
}