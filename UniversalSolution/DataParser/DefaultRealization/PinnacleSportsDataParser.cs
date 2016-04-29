using DataParser.Enums;
using DataParser.Models;
using System;
using System.Collections.Generic;
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


        public async Task<List<ResultForForks>> GetAllPinacleEventsForRequestAsync(SportType sportType)
        {
            var resList = new List<ResultForForks>();
            this.SportType = sportType;
            try
            {
                var totalResp = await GetAllTotalsAsync().ConfigureAwait(false);
                var teamNamesResp = await GetAllTeamNamesAsync().ConfigureAwait(false);
                resList.AddRange(GroupResponses(totalResp, teamNamesResp));
            }
            catch (Exception ex)
            {
                // ignored
            }
            return resList;
        }

        private IEnumerable<ResultForForks> GroupResponses(HttpWebResponse totalResp, HttpWebResponse teamNamesResp)
        {
            var evenstWithNames = ParseEventWithNames(teamNamesResp);
            var eventsWithTotal = ParseEventWithTotals(totalResp);

            List<ResultForForks> list = new List<ResultForForks>();
            foreach (EventWithTeamName withNames in evenstWithNames)
                foreach (var withTotal in eventsWithTotal.Where(t => t.Id == withNames.Id))
                {
                    list.Add(new ResultForForks()
                    {
                        Event = withNames.TeamNames,
                        Type = withTotal.TotalType,
                        Coef = withTotal.TotalValue,
                        Remark = withTotal.Remark,
                        SportType = SportType,
                        MatchDateTime = withTotal.MatchDateTime
                    });
                }
            return list;
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
                                        foreach (var spread in period.Value?["spreads"])
                                        {
                                            try
                                            {
                                                resList.Add(new EventWithTotal()
                                                {
                                                    Id = sportEvent.Value["id"].ConvertToLong(),
                                                    TotalType = spread.Value?["hdp"]?.ToString(),
                                                    TotalValue = spread.Value?["away"]?.ToString(),
                                                    Remark = "away",
                                                    MatchDateTime = period.Value?["cutoff"]?.ToString()
                                                });
                                                resList.Add(new EventWithTotal()
                                                {
                                                    Id = sportEvent.Value["id"].ConvertToLong(),
                                                    TotalType = spread.Value?["hdp"]?.ToString(),
                                                    TotalValue = spread.Value?["home"]?.ToString(),
                                                    Remark = "home",
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
                                            resList.Add(new EventWithTotal()
                                            {
                                                Id = sportEvent.Value["id"].ConvertToLong(),
                                                TotalType = "total away",
                                                TotalValue = period.Value?["teamTotal"]?["away"]?["points"]?.ToString(),
                                                Remark = "away",
                                                MatchDateTime = period.Value?["cutoff"]?.ToString()
                                            });
                                            resList.Add(new EventWithTotal()
                                            {
                                                Id = sportEvent.Value["id"].ConvertToLong(),
                                                TotalType = "total home",
                                                TotalValue = period.Value?["teamTotal"]?["home"]?["points"]?.ToString(),
                                                Remark = "home",
                                                MatchDateTime = period.Value?["cutoff"]?.ToString()
                                            });
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

            catch
                (Exception ex)
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
                                    Id = sportEvent.Value["id"].ConvertToLong(),
                                    TeamNames =
                                        $"{sportEvent.Value["home"]} - {sportEvent.Value["away"]}"
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

        /// <summary>
        /// This function get events with their team names
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpWebResponse> GetAllTeamNamesAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/fixtures?sportid=" + (int)SportType);
            string credentials = String.Format("{0}:{1}", "VB794327", "artem89@");
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
        protected virtual async Task<HttpWebResponse> GetAllTotalsAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/odds?sportid=" + (int)SportType);
            string credentials = String.Format("{0}:{1}", "VB794327", "artem89@");
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
