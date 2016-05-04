
//using DataParser.DefaultRealization;
//using DataParser.Enums;
using DataParser.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataParser.MY
{
    public static class HelperParse
    {
        public static string Substrings(this string line, string start, string end = "</")
        {
            string replaceStartElement = "@@";
            string replaceEndElement = "##";
            line = line.Replace(start, replaceStartElement).Replace(end, replaceEndElement);
            int indexStart = line.IndexOf(replaceStartElement) + replaceStartElement.Length;
            int indexEnd = line.IndexOf(replaceEndElement);
            return line.Substring(indexStart, indexEnd - indexStart);
        }
        public static bool _Contains(this string line, params string[] elements)
        {
            foreach (var e in elements)
                if (!line.Contains(e)) return false;
            return true;
        }

        public static string GetEventID(this string line)
        {
            string eventid = null;
            int start = line.IndexOf(Tags.EventID) + Tags.EventID.Length + 2;
            line = line.Substring(start);
            eventid = line.Substring(0, line.IndexOf("\""));
            return eventid;
        }
    }
    public static class Tags
    {
        public static readonly string NameTeam = "<div class=\"member-name nowrap \" data-ellipsis='{}'>";
        public static readonly string Date = "<td class=\"date\">";
        public static readonly string EventID = "data-event-treeId";
        public static readonly string Coff = "data-selection-price=\"";
        public static readonly string TypeCoff = "<span class=\"hint\">";
    }



    public class ParsePinnacle
    {

        private List<ResultForForks> GetNameTeamsAndDateAsync(SportType sportType)
        {
            string url = "";
            string namefile = "";
            UrlAndNameFile(sportType, out url, out namefile);
            //WriteToHtmlDocumentAsync(url, namefile).ConfigureAwait(false);
            List<Teams> teams = new List<Teams>();

            //<span class="hint">

            List<string> koff = new List<string>();
            string[] lines = HTML(url).Result.Split('\n');//File.ReadAllLines(namefile);

            string date = null;
            bool isDate = false;
            bool isTypeCoff = false;
            string oldEvent = "";

            string _eventid = null;
            List<string> countTypeCoff = new List<string>();

            int i = 0;
            int index = 0;
            foreach (var line in lines)
            {
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
                        if (englishNameTeams_Dictionary[_eventid].name1 == "Denmark")
                        {
                            int s = 0;
                        }
                        result.Add(new ResultForForks(englishNameTeams_Dictionary[_eventid].name1,
                                                      englishNameTeams_Dictionary[_eventid].name2,
                                                      date,
                                                      countTypeCoff[i],
                                                      res)
                                                      );
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















        enum SportType { Football, Basketball, Hockey, Tennis, Volleyball }
        private const int countCoff1 = 10;
        private const int countCoff2 = 6;


        private string oldLine = "";
        private Dictionary<string, EnglishNameTeams> englishNameTeams_Dictionary;
        public List<ResultForForks> result;

        //твоя метода this.GetResult(Type.basketball);

        public ParsePinnacle()
        {
            result = new List<ResultForForks>();
            Initi(SportType.Football);
        }

        private void Initi(SportType sportType)
        {
            try
            {
                this.englishNameTeams_Dictionary = this.GetEnglishNameTEams(sportType);
                var a = GetNameTeamsAndDateAsync(sportType);
                this.ShowForks(a);
            }
            catch { }
        }
        /*private static async Task WriteToHtmlDocumentAsync(string url, string namefile)
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
        }*/

        private static async Task<string> HTML(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
            StreamReader reader = new StreamReader(response.GetResponseStream());
            List<string> result = new List<string>();
            string HTML = await reader.ReadToEndAsync().ConfigureAwait(false);
            reader.Close();
            return HTML;
        }


        private Dictionary<string, EnglishNameTeams> GetEnglishNameTEams(SportType sportType)
        {
            Dictionary<string, EnglishNameTeams> resultEnglishTeams = new Dictionary<string, EnglishNameTeams>();
            string url = "";
            string namefile = "";
            UrlAndNameFile(sportType, out url, out namefile, true);

            string[] lines = HTML(url).Result.Split('\n');

            string name1 = null;
            string name2 = null;
            string _eventid = null;

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
    public string Event;
    public string Type;
    public string Coef;

    //  X1 X2 1 2 
    public ResultForForks(string nameTeam1, string nameTeam2, string date, string nameCoff, string coef)
    {
        this.Event = nameTeam1 + "-" + nameTeam2 + "-" + date;
        this.Type = nameCoff;
        this.Coef = coef;
    }

    public SportType SportType { get; set; }
    public string MatchDateTime { get; set; }
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

