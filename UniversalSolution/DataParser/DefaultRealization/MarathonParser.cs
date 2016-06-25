//using DataParser.DefaultRealization;
//using DataParser.Enums;
using DataParser.Enums;
using DataParser.Extensions;
using DataParser.Models;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataParser.MY
{
    public class MarathonParser
    {
        private async Task<List<ResultForForks>> GetNameTeamsAndDateAsync(SportType sportType)
        {
            result.Clear();

            //strings
            var url = "";
            var namefile = "";
            var oldEvent = "";
            string date = null;
            string foraName1 = null;
            string _eventid = null;
            string totalName = null;

            //Boolean
            var isDate = false;
            var isTypeCoff = false;
            var isFora = false;
            var isTotal = false;
            var isForaforteam1 = true;
            var isTotalUnder = true;

            //List<string>
            var koff = new List<string>();
            var countTypeCoff = new List<string>();

            //Integer
            var i = 0;
            var index = 0;


            UrlAndNameFile(sportType, out url, out namefile);
            var lines = (await HtmlAsync(url).ConfigureAwait(false)).Split('\n');


            foreach (var line in lines)
            {
                if (isFora)
                {
                    foraName1 = (isForaforteam1 ? "F1(" : "F2(") + line.Substrings("(", ")") + ")";
                    isFora = false;
                    isForaforteam1 = !isForaforteam1;
                }
                if (line.Contains(Tags.Fora))
                {
                    isFora = true;
                }

                if (isTotal)
                {
                    totalName = (isTotalUnder ? "TU(" : "TO(") + line.Substrings("(", ")") + ")";
                    isTotalUnder = !isTotalUnder;
                    isTotal = false;
                }
                if (line.Contains(Tags.Total))
                {
                    isTotal = true;
                }

                if (countTypeCoff.Count < 10)
                {
                    if (isTypeCoff)
                    {
                        countTypeCoff.Add((line.IndexOf('<') != -1) ? line.Replace("<b>", "").Replace("</b>", "") : line);
                        isTypeCoff = false;
                    }
                    if (line._Contains(Tags.TypeCoff))
                        isTypeCoff = true;
                }
                if (line._Contains(Tags.EventID))
                {
                    _eventid = line.GetEventID();
                    oldEvent = _eventid;
                }

                if (isDate)
                {
                    date = line;
                    isDate = false;
                }
                if (line._Contains(Tags.Date))
                {
                    isDate = true;
                }

                string res = null;
                if (line.Contains(Tags.Coff) /*&& line.Contains("Match_Result")*/)
                {
                    res = line.Substrings(Tags.Coff, "\"");
                    koff.Add(res);
                }
                if (line.Contains("<span>&mdash;</span>"))
                {
                    res = "-";
                    koff.Add(res);
                }

                if (date != null && res != null && _eventid != null)
                {
                    if (index >= countTypeCoff.Count)
                        index = 0;
                    try
                    {
                        string q1 = englishNameTeams_Dictionary[_eventid].name1;
                        string q2 = englishNameTeams_Dictionary[_eventid].name2;

                        result.Add(new ResultForForks(englishNameTeams_Dictionary[_eventid].name1,
                                                      englishNameTeams_Dictionary[_eventid].name2,
                                                      date,
                                                      (!string.IsNullOrEmpty(totalName) || !string.IsNullOrEmpty(foraName1)) ? (!string.IsNullOrEmpty(totalName) ? totalName : foraName1) : countTypeCoff[i],  //  Type coff
                                                      res,                //   znaczenia
                                                      sportType.ToString(),
                                                      Site.MarathonBet.ToString()
                                                      ));
                        if (string.IsNullOrEmpty(totalName) || string.IsNullOrEmpty(foraName1))
                            i++;
                        totalName = null;
                        foraName1 = null;
                        res = null;
                    }
                    catch { }
                }
                if (date != null && _eventid != null && res != null)
                {
                    date = null;
                    res = null;
                    _eventid = null;
                }

                if (oldEvent != _eventid)
                {
                    koff = new List<string>();
                    i = 0;
                }
                if (koff.Count > (is_Football_Hokey(sportType) ? countCoff1 : countCoff2) - 1)
                {
                    koff = new List<string>();
                    i = 0;
                }

                /*if (teams.Count == (is_Football_Hokey(sportType) ? countCoff1 : countCoff2))
                {
                    i++;
                }*/
                this.oldLine = line;
            }

            return result;
        }

        private const int countCoff1 = 10;
        private const int countCoff2 = 6;

        private string oldLine = "";
        private Dictionary<string, EnglishNameTeams> englishNameTeams_Dictionary;
        public List<ResultForForks> result;

        //твоя метода this.GetResult(Type.basketball);

        public MarathonParser()
        {
            result = new List<ResultForForks>();
            // Initi(sportType);
        }

        public async Task<List<ResultForForks>> InitiAsync(SportType sportType)
        {
            try
            {
                this.englishNameTeams_Dictionary = await this.GetEnglishNameTEams(sportType).ConfigureAwait(false);
                var result = await GetNameTeamsAndDateAsync(sportType).ConfigureAwait(false);
                return result;
                //this.ShowForks(a);
            }
            catch { }
            return new List<ResultForForks>();
        }

        private static async Task<string> HtmlAsync(string url, bool a = true)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
            List<string> result = new List<string>();
            string HTML = null;
            StreamReader reader = null;
            try
            {
                Debug.Assert(response != null, "response != null");
                reader = new StreamReader(response.GetResponseStream());
                HTML = await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            catch (FileLoadException)
            {
                //ignored
            }
            finally
            {
                reader?.Close();
            }

            return HTML;
        }

        private async Task<Dictionary<string, EnglishNameTeams>> GetEnglishNameTEams(SportType sportType)
        {
            var resultEnglishTeams = new Dictionary<string, EnglishNameTeams>();
            var url = "";
            var namefile = "";
            string name1 = null;
            string name2 = null;
            string _eventid = null;


            UrlAndNameFile(sportType, out url, out namefile, true);
            var lines = (await HtmlAsync(url, false).ConfigureAwait(false)).Split('\n');


            foreach (var line in lines)
            {
                if (line._Contains(Tags.EventID))
                    _eventid = line.GetEventID();

                if (line._Contains(Tags.NameTeam))
                {
                    if (name1 == null)
                        name1 = line.Substrings(Tags.NameTeam);
                    else name2 = line.Substrings(Tags.NameTeam);
                }
                if (!string.IsNullOrEmpty(name1) && !string.IsNullOrEmpty(name2) && !string.IsNullOrEmpty(_eventid))
                {
                    var teams = new EnglishNameTeams();
                    teams.name1 = name1;
                    teams.name2 = name2;
                    resultEnglishTeams.Add(_eventid, teams);
                    _eventid = null;
                    name1 = null;
                    name2 = null;
                }
            }

            return resultEnglishTeams;
        }

        private void UrlAndNameFile(SportType sportType, out string url, out string namefile, bool isEnglish = false)
        {
            string language = isEnglish ? "en" : "su";
            string en_namefile = isEnglish ? "en" : "";
            url = "https://www.marathonbet.com/" + language + "/popular/Ice+Hockey/?menu=true";
            namefile = "Default.html";
            switch (sportType)
            {
                case SportType.Soccer:
                    namefile = "Soccer" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Football/?menu=true"; //"/betting/Football/England/Championship/Promotion+Play-Offs/Semi+Final/1st+Leg/";
                    break;

                case SportType.Basketball:
                    namefile = "Basketball" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Basketball/?menu=true";
                    break;

                case SportType.Hockey:
                    namefile = "Hokey" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Ice+Hockey/?menu=true";
                    break;

                case SportType.Tennis:
                    namefile = "Tenis" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Tennis/?menu=true";
                    break;

                case SportType.Volleyball:
                    namefile = "Volleyball" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Volleyball/?menu=true";
                    break;
            }
        }

        private bool is_Football_Hokey(SportType sportType)
        {
            if (sportType == SportType.Soccer || sportType == SportType.Hockey)
                return true;
            return false;
        }

        private bool Get(string line, string nameclassparse, string nametypeparse = "")
        {
            if (string.IsNullOrEmpty(nameclassparse))
            {
                if (line.Contains(nameclassparse))
                    return true;
            }
            else
            {
                if (line.Contains(nameclassparse) && line.Contains(nametypeparse))
                    return true;
            }
            return false;
        }

        private string GetEventID(string line)
        {
            string element = "data-event-treeId";
            string eventid = null;
            int start = line.IndexOf(element) + element.Length + 2;
            line = line.Substring(start);
            eventid = line.Substring(0, line.IndexOf("\""));
            return eventid;
        }

        private void Show(List<Teams> teams)
        {
            foreach (var team in teams)
            {
                Console.WriteLine(team.eventId + "-"
                    + team.NameTeame1 + " - "
                    + team.NameTeame2 + " - "
                    + team.Date + " - "
                    + team.win1 + " - "
                    + (team.x != null ? team.x : "") + " - "
                    + team.win2 + " - "
                    + (team.x_win1 != null ? team.x_win1 : "") + " - "
                    + (team.x_win2 != null ? team.x_win2 : "") + " - "
                    + (team.win1_win2 != null ? team.win1_win2 : "") + " - "
                    + team.fora1 + " - "
                    + team.fora2 + " - "
                    + team.less + " - "
                    + team.more);
            }
        }

        public void ShowForks(List<ResultForForks> forks)
        {
            foreach (var fork in forks)
            {
                Console.WriteLine(fork.Event + "-"
                    + fork.Type + " - "
                    + fork.Coef
                    );
            }
        }
    }
}