using DataParser.Enums;
using DataParser.Models;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DataParser.DefaultRealization
{
    public class PinnacleSportsDataParser
    {
        private SportType SportType = SportType.NoType;
        private IConverterFormulas _converter;

        public PinnacleSportsDataParser(IConverterFormulas converter)
        {
            _converter = converter;
        }

        public async Task<List<ResultForForks>> GetAllPinacleEventsForRequestAsync(SportType sportType, string userLogin, string userPass)
        {
            var resList = new List<ResultForForks>();
            this.SportType = sportType;
            try
            {
                var totalResp = await GetAllTotalsAsync(userLogin, userPass).ConfigureAwait(false);
                var teamNamesResp = await GetAllTeamNamesAsync(userLogin, userPass).ConfigureAwait(false);
                resList.AddRange(GroupResponses(totalResp, teamNamesResp));
                resList.RemoveAll(r =>
                {
                    var d = r.Coef.ConvertToDoubleOrNull();
                    return d != null && (Math.Abs(d.Value) < 0.01 || Math.Abs(d.Value - _converter.IncorrectAmericanOdds) < 0.01);
                });
            }
            catch (Exception ex)
            {
                // ignored
            }
            return resList;
        }

        private IEnumerable<ResultForForks> GroupResponses(HttpWebResponse totalResp, HttpWebResponse teamNamesResp)
        {
            var eventsWithNames = ParseEventWithNames(teamNamesResp);
            var eventsWithTotal = ParseEventWithTotals(totalResp);

            return (from withNames in eventsWithNames
                    from withTotal in eventsWithTotal.Where(t => withNames.Id != null && t.Id != null && t.Id.Value == withNames.Id.Value)
                    select new ResultForForks()
                    {
                        Event = withNames.TeamNames,
                        Type = withTotal.TotalType,
                        Coef = _converter.ConvertAmericanToDecimal(
                           withTotal.TotalValue.ConvertToDoubleOrNull()).ToString(),
                        SportType = SportType.ToString(),
                        Bookmaker = Site.PinnacleSports.ToString(),
                        MatchDateTime = withTotal.MatchDateTime
                    }).ToList();
        }

        private List<EventWithTotal> ParseEventWithTotals(HttpWebResponse totalResp)
        {
            var resList = new List<EventWithTotal>();
            try
            {
                var sportEvents = (JsonObject)JsonObject.Load(totalResp.GetResponseStream());

                if (sportEvents?["leagues"] == null) return resList;

                foreach (var league in sportEvents["leagues"])
                {
                    try
                    {
                        foreach (var sportEvent in league.Value?["events"])
                        {
                            try
                            {
                                foreach (var period in sportEvent.Value?["periods"])
                                {
                                    try
                                    {
                                        resList.Add(new EventWithTotal()
                                        {
                                            Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                            TotalType = "1",
                                            TotalValue = period.Value?["moneyline"]?["home"]?.ToString(),
                                            MatchDateTime = period.Value?["cutoff"]?.ToString()
                                        });
                                        resList.Add(new EventWithTotal()
                                        {
                                            Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                            TotalType = "2",
                                            TotalValue = period.Value?["moneyline"]?["away"]?.ToString(),
                                            MatchDateTime = period.Value?["cutoff"]?.ToString()
                                        });
                                        resList.Add(new EventWithTotal()
                                        {
                                            Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                            TotalType = "X",
                                            TotalValue = period.Value?["moneyline"]?["draw"]?.ToString(),
                                            MatchDateTime = period.Value?["cutoff"]?.ToString()
                                        });
                                    }
                                    catch (Exception ex)
                                    {
                                        //ignored
                                    }

                                    try
                                    {
                                        foreach (var spread in period.Value?["spreads"])
                                        {
                                            try
                                            {
                                                resList.Add(new EventWithTotal()
                                                {
                                                    Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                                    TotalType = spread.Value?["hdp"]?.ToString(),
                                                    TotalValue = spread.Value?["away"]?.ToString(),
                                                    //  Remark = "spreads away",
                                                    MatchDateTime = period.Value?["cutoff"]?.ToString()
                                                });
                                                resList.Add(new EventWithTotal()
                                                {
                                                    Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                                    TotalType = spread.Value?["hdp"]?.ToString(),
                                                    TotalValue = spread.Value?["home"]?.ToString(),
                                                    //  Remark = "spreads home",
                                                    MatchDateTime = period.Value?["cutoff"]?.ToString()
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                //ignored
                                            }
                                        }
                                        try
                                        {
                                            //resList.Add(new EventWithTotal()
                                            //{
                                            //    Id = sportEvent.Value["id"].ConvertToLong(),
                                            //    TotalType = "Больше",
                                            //    TotalValue = period.Value?["teamTotal"]?["away"]?["points"]?.ToString(),
                                            //    MatchDateTime = period.Value?["cutoff"]?.ToString()
                                            //});
                                            //resList.Add(new EventWithTotal()
                                            //{
                                            //    Id = sportEvent.Value["id"].ConvertToLong(),
                                            //    TotalType = "Меньше",
                                            //    TotalValue = period.Value?["teamTotal"]?["home"]?["points"]?.ToString(),
                                            //    MatchDateTime = period.Value?["cutoff"]?.ToString()
                                            //});
                                        }
                                        catch (Exception ex)
                                        {
                                            //ignored
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // ignored
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // ignored
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            return resList;
        }

        private List<EventWithTeamName> ParseEventWithNames(HttpWebResponse teamNamesResp)
        {
            List<EventWithTeamName> list = new List<EventWithTeamName>();
            try
            {
                foreach (var league in
                        ((JsonObject)JsonObject.Load(teamNamesResp.GetResponseStream()))?["league"])
                {
                    var sportEvents = league.Value?["events"];
                    if (sportEvents != null)
                        foreach (var sportEvent in sportEvents)
                        {
                            if (sportEvent.Value != null)
                                list.Add(new EventWithTeamName()
                                {
                                    Id = sportEvent.Value["id"].ConvertToLongOrNull(),
                                    TeamNames =
                                        $"{sportEvent.Value["home"]} - {sportEvent.Value["away"]}"
                                        .Replace("\"", "")
                                });
                        }
                }
            }
            catch (Exception ex)
            {
                //ignored
            }
            return list;
        }

        private Dictionary<long, List<EventWithTotalDictionary>> ParseEventWithTotalsDictionaty(HttpWebResponse totalResp)
        {
            var resList = new Dictionary<long, List<EventWithTotalDictionary>>();
            try
            {
                var sportEvents = (JsonObject)JsonObject.Load(totalResp.GetResponseStream());

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
                                        resList[id].Add(new EventWithTotalDictionary()
                                        {
                                            TotalType = "1",
                                            TotalValue = period.Value?["moneyline"]?["home"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                        resList[id].Add(new EventWithTotalDictionary()
                                        {
                                            TotalType = "2",
                                            TotalValue = period.Value?["moneyline"]?["away"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                        resList[id].Add(new EventWithTotalDictionary()
                                        {
                                            TotalType = "X",
                                            TotalValue = period.Value?["moneyline"]?["draw"]?.ToString(),
                                            MatchDateTime = matchDateTime
                                        });
                                    }
                                    catch (Exception ex)
                                    {
                                        //ignored
                                    }

                                    try
                                    {
                                        foreach (var spread in period.Value?["spreads"])
                                        {
                                            try
                                            {
                                                resList[id].Add(new EventWithTotalDictionary()
                                                {
                                                    TotalType = $"F2({spread.Value?["hdp"]})",
                                                    TotalValue = spread.Value?["away"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                                resList[id].Add(new EventWithTotalDictionary()
                                                {
                                                    TotalType = $"F1({spread.Value?["hdp"]})",
                                                    TotalValue = spread.Value?["home"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                //ignored
                                            }
                                        }
                                        foreach (var total in period.Value?["totals"])
                                        {
                                            try
                                            {
                                                resList[id].Add(new EventWithTotalDictionary()
                                                {
                                                    TotalType = $"TO({total.Value?["points"]})",
                                                    TotalValue = total.Value?["over"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                                resList[id].Add(new EventWithTotalDictionary()
                                                {
                                                    TotalType = $"TU({total.Value?["points"]})",
                                                    TotalValue = total.Value?["under"]?.ToString(),
                                                    MatchDateTime = matchDateTime
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                //ignored
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // ignored
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // ignored
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            return resList;
        }

        private Dictionary<long, string> ParseEventWithNamesDictionary(HttpWebResponse teamNamesResp)
        {
            Dictionary<long, string> resList = new Dictionary<long, string>();
            try
            {
                foreach (var league in ((JsonObject)JsonObject.
                    Load(teamNamesResp.GetResponseStream()))?["league"])
                {
                    try
                    {
                        foreach (var sportEvent in league.Value?["events"])
                        {
                            //if id null - all ok will be exception and we go to the next item in foreach
                            var id = sportEvent.Value["id"].ConvertToLongOrNull().Value;

                            if (!resList.ContainsKey(id))
                                resList.Add(id, $"{sportEvent.Value["home"]} - {sportEvent.Value["away"]}"//todo change here
                                    .Replace("\"", ""));
                            else
                                Console.Write("*******************Duplicate Id for ParseEventWithNames**********************"); //todo tmp need for checking
                        }
                    }
                    catch (Exception ex)
                    {
                        //ignored
                    }
                }
            }
            catch (Exception ex)
            {
                //ignored
            }
            return resList;
        }

        private Dictionary<string,ResultForForksDictionary> GroupResponsesDictionary(HttpWebResponse totalResp, HttpWebResponse teamNamesResp)
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
                                    TypeCoefDictionary = new Dictionary<string, double>()
                                });
                            if (eventWithTotal.TotalType.Contains("T"))
                                eventWithTotal.TotalType = ParseTotalType(eventWithTotal.TotalType);
                            resDic[key].TypeCoefDictionary.Add(eventWithTotal.TotalType,
                                _converter.ConvertAmericanToDecimal(eventWithTotal.TotalValue.ConvertToDoubleOrNull()));
                        }
                        else
                        {
                            if (eventWithTotal.TotalType.Contains("T"))
                                eventWithTotal.TotalType = ParseTotalType(eventWithTotal.TotalType);
                            if (!resDic[key].TypeCoefDictionary.ContainsKey(eventWithTotal.TotalType))
                                resDic[key].TypeCoefDictionary.Add(eventWithTotal.TotalType,
                                    _converter.ConvertAmericanToDecimal(
                                        eventWithTotal.TotalValue.ConvertToDoubleOrNull()));
                        }
                    }
                    catch (Exception ex)
                    {
                        //ignored
                    }
                }
            }
            return resDic;
        }

        private string ParseTotalType(string totalType)
        {
            var prefix = totalType.Substring(0,3);

            totalType = totalType.Remove(0, 3);
            totalType = totalType.Remove(totalType.Length-1, 1);

            return prefix + _converter.ConvertAmericanToDecimal(totalType.ConvertToDoubleOrNull()) + ")";
        }

        public async Task<Dictionary<string, ResultForForksDictionary>> GetAllPinacleEventsForRequestDictionaryAsync(SportType sportType, string userLogin, string userPass)
        {
            var resDic = new Dictionary<string, ResultForForksDictionary>();
            this.SportType = sportType;
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
                // ignored
            }
            return resDic;
        }

        /// <summary>
        /// This function get events with their team names
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpWebResponse> GetAllTeamNamesAsync(string userLogin, string userPass)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/fixtures?sportid=" + (int)SportType);
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
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/odds?sportid=" + (int)SportType);
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
    }
}
