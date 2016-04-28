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
        private SportType sportType = SportType.NoType;


        public async Task<List<ResultForForks>> GetAllPinacleEventsForRequestAsync(SportType sportType)
        {
            var resList = new List<ResultForForks>();
            this.sportType = sportType;
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
                foreach (var withTotal in eventsWithTotal)
                {
                    if (withNames.Id == withTotal.Id)
                        list.Add(new ResultForForks()
                        {
                            Event = withNames.TeamNamesWithDate,
                            Type = withTotal.TotalType,
                            Coef = withTotal.TotalValue
                        });
                }
            return list;
        }

        private List<EventWithTotal> ParseEventWithTotals(HttpWebResponse totalResp)
        {
            bool bSwitch = false;
            var resList = new List<EventWithTotal>();
            var sportEvents = (JsonObject)JsonObject.Load(totalResp.GetResponseStream());
            foreach (KeyValuePair<string, JsonValue> league in sportEvents["leagues"])
                foreach (var sportEvent in league.Value["events"])
                {
                    if (sportEvent.Value != null)
                        try
                        {
                            var type = bSwitch ? "home" : "away";
                            resList.Add(new EventWithTotal()
                            {
                                Id = sportEvent.Value["id"].ConvertToLong(),
                                TotalType = type,
                                TotalValue = sportEvent.Value["periods"]?[0]?["teamTotal"]?[type]?["points"].ToString()
                            });
                            bSwitch = !bSwitch;
                        }
                        catch (Exception) //can be when this event without total
                        {
                            // ignored
                        }
                }
            return resList;
        }

        private List<EventWithTeamName> ParseEventWithNames(HttpWebResponse teamNamesResp)
        {
            return (from league in ((JsonObject)
                        JsonObject.Load(teamNamesResp.GetResponseStream()))["league"]
                    from sportEvent in league.Value["events"]
                    where sportEvent.Value != null
                    select new EventWithTeamName()
                    {
                        Id = sportEvent.Value["id"].ConvertToLong(),
                        TeamNamesWithDate = $"home {sportEvent.Value["home"]} - " + $"away {sportEvent.Value["away"]} - " + $"starts {sportEvent.Value["starts"]}"
                    }).ToList();
        }

        /// <summary>
        /// This function get events with their team names
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpWebResponse> GetAllTeamNamesAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/fixtures?sportid=" + (int)sportType);
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
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/odds?sportid=" + (int)sportType);
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
