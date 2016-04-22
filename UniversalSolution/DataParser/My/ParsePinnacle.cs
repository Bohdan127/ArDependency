#region OldVersion
/*using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Tools;

namespace DataParser.MY
{
    public class ParsePinnacle
    {
        // "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
        private const string link = "https://www.marathonbet.com/su/";

        public ParsePinnacle()
        {
            //this.WriteToFile();

            //Ця метода вертає лісту команд з коффами GetNameTeamsAndDate
            // var a = GetNameTeamsAndDate(link);
            // this.Show(a);
        }
        public void WriteToFile()
        {
            var webClient = new System.Net.WebClient();
            webClient.Credentials = new System.Net.NetworkCredential("ZZ868380", "mko)9ijn");
            string HTML = webClient.DownloadString(link);

            StreamWriter sw = new StreamWriter("HTML5.txt");
            sw.WriteLine(HTML);
            sw.Close();

            Console.WriteLine(HTML);
        }

        /*WebClient client = new WebClient();
        Stream rssFeedStream = client.OpenRead(link);
        XmlReader reader = XmlReader.Create(rssFeedStream);
        reader.MoveToContent();

        StreamWriter sw = new StreamWriter("HTML3.html");
        sw.WriteLine(reader.ReadContentAsString());
        sw.Close();
        /*
        //GetHtmlDocument(this.link);
    }*/
     /*   public static void GetHtmlDocument(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(true);
            StreamReader reader = new StreamReader(response.GetResponseStream());
            List<string> result = new List<string>();
            string HTML = reader.ReadToEnd();
            reader.Close();
            StreamWriter sw = new StreamWriter("HTML.html");
            sw.WriteLine(HTML);
            sw.Close();
        }

        //data-selection-price=
        public async Task<List<Teams>> GetNameTeamsAndDateAsync(string url = link)
        {
            await GetHtmlDocumentAsync(url).ConfigureAwait(true);
            List<Teams> teams = new List<Teams>();

            List<string> koff = new List<string>();
            string[] lines = File.ReadAllLines("HTML.html");
            string name1 = null;
            string name2 = null;
            string date = null;
            bool isDate = false;
            int i = 0;
            foreach (var line in lines)
            {
                try
                {


                    #region [NameTeams and Date]
                    if (line.Contains("<div class=\"member-name nowrap \" data-ellipsis='{}'>"))
                    {
                        string findElements = "<div class=\"member-name nowrap \" data-ellipsis='{}'>";
                        int startIndex = line.LastIndexOf(findElements);
                        int endIndex = line.LastIndexOf("</");
                        if (startIndex < endIndex && startIndex != -1 && endIndex != -1)
                        {
                            if (name1 == null)
                                name1 = line.Substring(findElements.Length + 1, endIndex - findElements.Length - 1);
                            else name2 = line.Substring(findElements.Length + 1, endIndex - findElements.Length - 1);
                            i++;
                        }

                    }
                    if (isDate)
                    {
                        date = line;
                        isDate = false;
                    }
                    if (line.Contains("<td class=\"date\">"))
                    {
                        isDate = true;
                    }

                    #endregion

                    if (line.Contains("data-selection-price="))
                    {
                        int indexStart = line.IndexOf("data-selection-price=") + ("data-selection-price=").Length;
                        var res = line.Substring(indexStart).Trim('\"');
                        koff.Add(res);

                    }
                    if (name1 != null && name2 != null && date != null && koff.Count == 10)
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

                        teams.Add(new Teams(name1, name2, date, win1, x, win2, x_win1, x_win2, win1_win2, fora1, fora2, less, more));
                        name1 = null;
                        name2 = null;
                        date = null;
                        koff = new List<string>();
                    }
                    if (koff.Count > 10)
                    {
                        koff = new List<string>();
                    }

                    if (teams.Count == 10)
                    {
                        i++;
                    }
                }
                catch
                {
                }
            }

            //Forks will be here like a tmp solution
            var resList = new List<Teams>();
            for (int j = 0; j < teams.Count; j++)
            {
                try
                {
                    if (CheckIsFork(teams[j].win1, teams[j].win2))
                    {
                        teams[j].fork = 1 / teams[j].win1 + 1 / teams[j].win2;
                        resList.Add(teams[j]);
                    }
                }
                catch { }
            }
            return resList;

        }
        public void Show(List<Teams> teams)
        {
            foreach (var team in teams)
            {
                Console.WriteLine(team.NameTeame1 + " - " + team.NameTeame2 + " - " + team.Date + " - " + team.win1 + " - " + team.x + " - " + team.win2 + " - " +
                    team.x_win1 + " - " + team.x_win2 + " - " + team.win1_win2 + " - " + team.fora1 + " - " + team.fora2 + " - " + team.less + " - " + team.more);
            }
        }

        public static bool CheckIsFork(double kof1, double kof2)
            => 1 < (1 / kof1 + 1 / kof2);

        /*public List<Teams> PinnacleSports() {
            //<span class="text"><span class="trigger" назви команд
        }*/

    //}
/*public class Teams
{
    private string nameTeam1;
    private string nameTeam2;
    private string date;
    public string win1;
    public string x;
    public string win2;
    public string x_win1;
    public string x_win2;
    public string win1_win2;
    public string fora1;
    public string fora2;
    public string less;
    public string more;
    public Teams() { }
    public Teams(string nameTeam1, string nameTeam2, string date,
        string win1, string x, string win2,
        string x_win1, string x_win2, string win1_win2,
        string fora1, string fora2, string less, string more)
    {
        this.nameTeam1 = nameTeam1;
        this.nameTeam2 = nameTeam2;
        this.date = date;
        this.win1 = win1;
        this.x = x;
        this.win2 = win2;
        this.x_win1 = x_win1;
        this.x_win2 = x_win2;
        this.win1_win2 = win1_win2;
        this.fora1 = fora1;
        this.fora2 = fora2;
        this.less = less;
        this.more = more;
    }

    public string NameTeame1 { get { return this.nameTeam1; } set { this.nameTeam1 = value; } }
    public string NameTeame2 { get { return this.nameTeam2; } set { this.nameTeam2 = value; } }
    public string Date { get { return this.date; } set { this.date = value; } }

}
}
*/

#endregion

using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using WatiN.Core;

namespace DataParser.MY
{
    public class ParsePinnacle
    {
        // "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
        private readonly string link = "https://www.marathonbet.com/su/popular/Football/?menu=true";
        public enum Type { football, basketball, hokey, tenis, volleyball }
        private string oldLine = "";



        //твоя метода GetNameTeamsAndDate(type);

        public ParsePinnacle()
        {
            //this.WriteToFile();
            var a = this.GetResult(Type.basketball);

        }

        public List<ResultForVilki> GetResult(Type type)
        {
            List<ResultForVilki> result = new List<ResultForVilki>();
            var a = GetNameTeamsAndDate(type/*, ref result*/);
            this.Show(a);
            return result;
        }
        public void WriteToFile()
        {
            var webClient = new System.Net.WebClient();
            webClient.Credentials = new System.Net.NetworkCredential("ZZ868380", "mko)9ijn");
            string HTML = webClient.DownloadString(link);

            StreamWriter sw = new StreamWriter("HTML5.txt");
            sw.WriteLine(HTML);
            sw.Close();

            Console.WriteLine(HTML);
        }
        #region
        /*WebClient client = new WebClient();
        Stream rssFeedStream = client.OpenRead(link);
        XmlReader reader = XmlReader.Create(rssFeedStream);
        reader.MoveToContent();

        StreamWriter sw = new StreamWriter("HTML3.html");
        sw.WriteLine(reader.ReadContentAsString());
        sw.Close();
        /*
        //GetHtmlDocument(this.link);
    }*/
        #endregion
        public static void GetHtmlDocument(string url, string namefile)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            List<string> result = new List<string>();
            string HTML = reader.ReadToEnd();
            reader.Close();
            StreamWriter sw = new StreamWriter(namefile);
            sw.WriteLine(HTML);
            sw.Close();
        }

        //data-selection-price=
        //                              <div class="member-name nowrap " data-ellipsis='{}'>
        //                              <div class="member-name nowrap " data-ellipsis='{}'>
        private const string forTeam = "<div class=\"member-name nowrap \" data-ellipsis='{}'>";
        private const string forDate = "<td class=\"date\">";
        private const int countCoff1 = 10;
        private const int countCoff2 = 6;


        public List<Teams> GetNameTeamsAndDate(Type type/*, ref List<ResultForVilki> result*/)
        {
            string url = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";
            string namefile = "Default.html";
            #region
            switch (type)
            {
                case Type.football:
                    namefile = "Football.html";
                    url = "https://www.marathonbet.com/su/popular/Football/?menu=true";
                    break;
                case Type.basketball:
                    namefile = "Basketball.html";
                    url = "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
                    break;
                case Type.hokey:
                    namefile = "Hokey.html";
                    url = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";
                    break;
                case Type.tenis:
                    namefile = "Tenis.html";
                    url = "https://www.marathonbet.com/su/popular/Tennis/?menu=true";
                    break;
                case Type.volleyball:
                    namefile = "Volleyball.html";
                    url = "https://www.marathonbet.com/su/popular/Volleyball/?menu=true";
                    break;
            }
            #endregion
            GetHtmlDocument(url, namefile);
            List<Teams> teams = new List<Teams>();

            List<string> koff = new List<string>();
            string[] lines = File.ReadAllLines(namefile);
            string name1 = null;
            string name2 = null;
            string coff = null;
            string date = null;
            bool isDate = false;

            List<string> countTypeCoff = new List<string>();

            int i = 0;
            int index = 0;
            foreach (var line in lines)
            {
                if (oldLine.Contains("<span class=\"hint\">"))
                {
                    string t = line.Trim(' ');
                    int start = t.IndexOf(">") + 1;
                    int end = t.IndexOf("</");
                    end = end - start;
                    string typeCoff = "";
                    if (end != -1 && start != -1)
                        typeCoff = t.Substring(start, end);
                    else typeCoff = t;
                    if (this.is_Football_Hokey(type))
                    {
                        if (countTypeCoff.Count < countCoff1)
                        {
                            countTypeCoff.Add(typeCoff);
                        }
                    }
                    else
                    {
                        if (countTypeCoff.Count < countCoff2)
                        {
                            countTypeCoff.Add(typeCoff);
                        }
                    }
                }

                #region [NameTeams and Date]
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
                        i++;
                    }

                }
                if (isDate)
                {
                    date = line;
                    isDate = false;
                }
                if (this.Get(line, forDate))
                {
                    isDate = true;
                }

                #endregion

                string res = null;
                if (line.Contains("data-selection-price=") /*&& line.Contains("Match_Result")*/)
                {
                    int indexStart = line.IndexOf("data-selection-price=") + ("data-selection-price=").Length;
                    res = line.Substring(indexStart).Trim('\"');
                    koff.Add(res);

                }

                /*if (name1 != null && name2 != null && date != null && res != null)
                {
                    if (index >= countTypeCoff.Count)
                        index = 0;
                    var a=countTypeCoff[index];
                    result.Add(new ResultForVilki(name1, name2, date, a, res));
                    res = null;
                    i++;
                }*/

                if (name1 != null && name2 != null && date != null)
                {
                    if (this.is_Football_Hokey(type) && (koff.Count == 10))
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

                        teams.Add(new Teams_Football_Hokey(name1, name2, date, win1, x, win2, x_win1, x_win2, win1_win2, fora1, fora2, less, more));
                        name1 = null;
                        name2 = null;
                        date = null;
                        koff = new List<string>();
                    }
                    if (!this.is_Football_Hokey(type) && (koff.Count == 6))
                    {
                        string win1 = koff[0];
                        string win2 = koff[1];
                        string fora1 = koff[2];
                        string fora2 = koff[3];
                        string less = koff[4];
                        string more = koff[5];

                        teams.Add(new Teams_Tenis_Volleyball_Basketball(name1, name2, date, win1, win2, fora1, fora2, less, more));
                        name1 = null;
                        name2 = null;
                        date = null;
                        koff = new List<string>();
                    }

                }

                if (koff.Count > (is_Football_Hokey(type) ? countCoff1 : countCoff2))
                {
                    koff = new List<string>();
                }

                if (teams.Count == (is_Football_Hokey(type) ? countCoff1 : countCoff2))
                {
                    i++;
                }
                this.oldLine = line;
            }
            return teams;

        }

        private bool is_Football_Hokey(Type type)
        {
            if (type == Type.football || type == Type.hokey)
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
        public void Show(List<Teams> teams)
        {
            foreach (var team in teams)
            {
                Console.WriteLine(team.NameTeame1 + " - " + team.NameTeame2 + " - " + team.Date + " - " + team.win1 + " - " + (team.x != null ? team.x : "") + " - " + team.win2 + " - " +
                    (team.x_win1 != null ? team.x_win1 : "") + " - " + (team.x_win2 != null ? team.x_win2 : "") + " - " + (team.win1_win2 != null ? team.win1_win2 : "") + " - " + team.fora1 + " - " + team.fora2 + " - " + team.less + " - " + team.more);
            }
        }

        /*public List<Teams> PinnacleSports() {
            //<span class="text"><span class="trigger" назви команд
        }*/
        //Total_Goals
    }


    public class ResultForVilki
    {
        private string _event;
        private string name_Coff;
        private string value;

        //  X1 X2 1 2 
        public ResultForVilki(string nameTeam1, string nameTeam2, string date, string nameCoff, string value)
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

        public double win1 { get; set; }
        public double win2 { get; set; }
        public string fora1;
        public string fora2;
        public string less;
        public string more;

        public string x;
        public string x_win1;
        public string x_win2;
        public string win1_win2;
        public Teams() { }
        public Teams(string nameTeam1, string nameTeam2, string date,
            string win1, string win2,
            string fora1, string fora2, string less, string more)
        {
            this.nameTeam1 = nameTeam1;
            this.nameTeam2 = nameTeam2;
            this.date = date;

            this.win1 = win1;
            this.win2 = win2.ConvertToDouble();
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
        public Teams_Football_Hokey(string nameTeam1, string nameTeam2, string date,
            string win1, string x, string win2,
            string x_win1, string x_win2, string win1_win2,
            string fora1, string fora2, string less, string more)
            : base(nameTeam1, nameTeam2, date, win1, win2, fora1, fora2, less, more)
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
        public Teams_Tenis_Volleyball_Basketball(string nameTeam1, string nameTeam2, string date,
            string win1, string win2,
            string fora1, string fora2, string less, string more)
            : base(nameTeam1, nameTeam2, date, win1, win2, fora1, fora2, less, more)
        {
            this.x = null;
            this.x_win1 = null;
            this.x_win2 = null;
            this.win1_win2 = null;
        }
    }
}
