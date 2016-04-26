
using DataParser.DefaultRealization;
using DataParser.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataParser.MY
{
    public class ParsePinnacle
    {

        private const string forTeam = "<div class=\"member-name nowrap \" data-ellipsis='{}'>";
        private const string forDate = "<td class=\"date\">";
        private const int countCoff1 = 10;
        private const int countCoff2 = 6;


        private string oldLine = "";
        private Dictionary<string, EnglishNameTeams> englishNameTeams_Dictionary;
        public List<ResultForForks> result;

        string url = "";
        string namefile = "";

        //твоя метода this.GetResult(Type.basketball);

        public ParsePinnacle()
        {
            result = new List<ResultForForks>();
            //this.WriteToFile();
            //var a = this.GetResult(Type.football);
            Initi(SportType.Volleyball);
        }

        private void Initi(SportType sportType)
        {

            /*UrlAndNameFile(sportType, out url, out namefile, true);
            WriteToHtmlDocumentAsync(url, namefile);
            UrlAndNameFile(sportType, out url, out namefile);
            WriteToHtmlDocumentAsync(url, namefile);
            
            List<ResultForForks> a = GetNameTeamsAndDateAsync(sportType).Result;
            this.ShowForks(a);*/
            try
            {
                this.englishNameTeams_Dictionary = this.GetEnglishNameTEams(sportType);
                var a = GetNameTeamsAndDateAsync(sportType);
                this.ShowForks(a);
            }
            catch{}
        }
        private static async Task WriteToHtmlDocumentAsync(string url, string namefile)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
            StreamReader reader = new StreamReader(response.GetResponseStream());
            List<string> result = new List<string>();
            string HTML = await reader.ReadToEndAsync().ConfigureAwait(false);
            reader.Close();
            StreamWriter sw = new StreamWriter(namefile);
            sw.WriteLine(HTML);
            sw.Close();

            File.ReadAllLines(namefile);
        }

        private Dictionary<string, EnglishNameTeams> GetEnglishNameTEams(SportType sportType)
        {
            Dictionary<string, EnglishNameTeams> resultEnglishTeams = new Dictionary<string, EnglishNameTeams>();
            string url = "";
            string namefile = "";
            UrlAndNameFile(sportType, out url, out namefile, true);

            WriteToHtmlDocumentAsync(url, namefile);

            string[] lines = File.ReadAllLines(namefile);

            string name1 = null;
            string name2 = null;
            string _eventid = null;

            foreach (var line in lines)
            {
                if (line.Contains("data-event-treeId"))
                    _eventid = this.GetEventID(line);

                if (this.Get(line, forTeam))
                {
                    string findElements = forTeam;
                    int startIndex = line.LastIndexOf(findElements);
                    int endIndex = line.LastIndexOf("</");
                    if (startIndex < endIndex && startIndex != -1 && endIndex != -1)
                    {
                        if (name1 == null)
                            name1 = line.Substring(findElements.Length + 1, endIndex - findElements.Length - 1);
                        else name2 = line.Substring(findElements.Length + 1, endIndex - findElements.Length - 1);
                    }

                }
                if (!string.IsNullOrEmpty(name1) && !string.IsNullOrEmpty(name2) && !string.IsNullOrEmpty(_eventid))
                {
                    var t = new EnglishNameTeams();
                    t.name1 = name1;//.Add(name1, name2);
                    t.name2 = name2;
                    resultEnglishTeams.Add(_eventid, t);
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
                case SportType.Football:
                    namefile = "Football" + en_namefile + ".html";
                    url = "https://www.marathonbet.com/" + language + "/popular/Football/?menu=true";
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
        private List<ResultForForks> GetNameTeamsAndDateAsync(SportType sportType)
        {
            string url = "";
            string namefile = "";
            UrlAndNameFile(sportType, out url, out namefile);
            WriteToHtmlDocumentAsync(url, namefile).ConfigureAwait(false);
            List<Teams> teams = new List<Teams>();

            List<string> koff = new List<string>();
            string[] lines = File.ReadAllLines(namefile);

            string date = null;
            bool isDate = false;
            string _eventid = null;
                    List<string> countTypeCoff = new List<string>();

            int i = 0;
            int index = 0;
            foreach (var line in lines)
            {
                if (line.Contains("data-event-treeId"))
                    _eventid = this.GetEventID(line);
                if (isDate)
                {
                    date = line;
                    isDate = false;
                }
                if (this.Get(line, forDate))
                {
                    isDate = true;
                }

                        string res = null;
                        if (line.Contains("data-selection-price=") /*&& line.Contains("Match_Result")*/)
                        {
                            int indexStart = line.IndexOf("data-selection-price=") + ("data-selection-price=").Length;
                            res = line.Substring(indexStart).Trim('\"');
                            koff.Add(res);

                        }

                if (date != null && res != null && _eventid != null)
                {
                    if (index >= countTypeCoff.Count)
                        index = 0;
                    //var a=countTypeCoff[index];
                    try
                    {
                        string name1 = englishNameTeams_Dictionary[_eventid].name1;
                        string name2 = englishNameTeams_Dictionary[_eventid].name2;
                        result.Add(new ResultForForks(name1, name2, date, " - ", res));
                        res = null;
                        i++;
                    }
                    catch { }
                }
                if (date != null && _eventid != null && res != null)
                {
                    date = null;
                    res = null;
                    _eventid = null;
                }
                #region
                /*if (name1 != null && name2 != null && date != null && _eventid != null)
                {
                    if (this.is_Football_Hokey(sportType) && (koff.Count == 10))
                    {
                        string win1 = koff[0];
                        string x = koff[1];
                        string win2 = koff[2];
                        string x_win1 = koff[3];
                        string x_win2 = koff[4];
                        string win1_win2 = koff[5];
                        string fora1 = koff[6];
                        string fora2 = koff[7];
                        string less = koff[8];
                        string more = koff[9];

                        name1 = englishNameTeams_Dictionary[_eventid].FirstOrDefault().Key;
                        name2 = englishNameTeams_Dictionary[_eventid].FirstOrDefault().Value;
                        teams.Add(new Teams_Football_Hokey(_eventid, name1, name2, date, win1, x, win2, x_win1, x_win2, win1_win2, fora1, fora2, less, more));
                        name1 = null;
                        name2 = null;
                        date = null;
                        _eventid = null;
                        koff = new List<string>();
                    }
                    if (!this.is_Football_Hokey(sportType) && (koff.Count == 6))
                    {
                        string win1 = koff[0];
                        string win2 = koff[1];
                        string fora1 = koff[2];
                        string fora2 = koff[3];
                        string less = koff[4];
                        string more = koff[5];

                        name1 = englishNameTeams_Dictionary[_eventid].FirstOrDefault().Key;
                        name2 = englishNameTeams_Dictionary[_eventid].FirstOrDefault().Value;
                        teams.Add(new Teams_Tenis_Volleyball_Basketball(_eventid, name1, name2, date, win1, win2, fora1, fora2, less, more));
                        name1 = null;
                        name2 = null;
                        date = null;
                        _eventid = null;
                        koff = new List<string>();
                    }

                }*/
                #endregion

                        if (koff.Count > (is_Football_Hokey(sportType) ? countCoff1 : countCoff2))
                        {
                            koff = new List<string>();
                        }

                        if (teams.Count == (is_Football_Hokey(sportType) ? countCoff1 : countCoff2))
                        {
                            i++;
                        }
                        this.oldLine = line;
                    }
                    break;
                case Site.PinnacleSports:
                    result.AddRange(await new PinnacleSportsDataParser().GetAllPinacleEventsForRequestAsync(sportType).ConfigureAwait(false));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(site), site, null);
            }

            #endregion

            return result;

        }

        private bool is_Football_Hokey(SportType sportType)
        {
            if (sportType == SportType.Football || sportType == SportType.Hockey)
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

        private void ShowForks(List<ResultForForks> forks)
        {
            foreach (var fork in forks)
            {
                Console.WriteLine(fork._event + "-"
                    + fork.name_Coff + " - "
                    + fork.value
                    );
            }
        }

        /*public List<Teams> PinnacleSports() {
            //<span class="text"><span class="trigger" назви команд
        }*/
        //Total_Goals
    }

    public class EnglishNameTeams
        {
            public string eventid;
            public string name1;
            public string name2;
            public EnglishNameTeams() { }
            public EnglishNameTeams(string _eventid, string _name1, string _name2)
            {
                this.eventid = _eventid;
                this.name1 = _name1;
                this.name2 = _name2;
            }
        }
        public class ResultForForks
        {
            public string _event;
            public string name_Coff;
            public string value;

            //  X1 X2 1 2 
            public ResultForForks(string nameTeam1, string nameTeam2, string date, string nameCoff, string value)
            {
                this._event = nameTeam1 + "-" + nameTeam2 + "-" + date;
                this.name_Coff = nameCoff;
                this.value = value;
            }
        }

        public class Teams
        {
            protected string nameTeam1;
            protected string nameTeam2;
            protected string date;

            public string eventId;
            public string win1;
            public string win2;
            public string fora1;
            public string fora2;
            public string less;
            public string more;

            public string x;
            public string x_win1;
            public string x_win2;
            public string win1_win2;
            public Teams() { }
            public Teams(string _eventid, string nameTeam1, string nameTeam2, string date,
                string win1, string win2,
                string fora1, string fora2, string less, string more)
            {
                this.eventId = _eventid;
                this.nameTeam1 = nameTeam1;
                this.nameTeam2 = nameTeam2;
                this.date = date;

                this.win1 = win1;
                this.win2 = win2;
                this.fora1 = fora1;
                this.fora2 = fora2;
                this.less = less;
                this.more = more;
            }

            public string NameTeame1 { get { return this.nameTeam1; } set { this.nameTeam1 = value; } }
            public string NameTeame2 { get { return this.nameTeam2; } set { this.nameTeam2 = value; } }
            public string Date { get { return this.date; } set { this.date = value; } }
        }
        public class Teams_Football_Hokey : Teams
        {
            public Teams_Football_Hokey() : base() { }
            public Teams_Football_Hokey(string _eventid, string nameTeam1, string nameTeam2, string date,
                string win1, string x, string win2,
                string x_win1, string x_win2, string win1_win2,
                string fora1, string fora2, string less, string more)
                : base(_eventid, nameTeam1, nameTeam2, date, win1, win2, fora1, fora2, less, more)
            {
                this.x = x;
                this.x_win1 = x_win1;
                this.x_win2 = x_win2;
                this.win1_win2 = win1_win2;

            }
        }
        public class Teams_Tenis_Volleyball_Basketball : Teams
        {
            public Teams_Tenis_Volleyball_Basketball() : base() { }
            public Teams_Tenis_Volleyball_Basketball(string _eventid, string nameTeam1, string nameTeam2, string date,
                string win1, string win2,
                string fora1, string fora2, string less, string more)
                : base(_eventid, nameTeam1, nameTeam2, date, win1, win2, fora1, fora2, less, more)
            {
                this.x = null;
                this.x_win1 = null;
                this.x_win2 = null;
                this.win1_win2 = null;
            }
        }
    
}