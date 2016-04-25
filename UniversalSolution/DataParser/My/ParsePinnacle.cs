
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
        // "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
        private readonly string link = "https://www.marathonbet.com/su/popular/Football/?menu=true";
        private string oldLine = "";



        //твоя метода this.GetResult(Type.basketball);

        public ParsePinnacle()
        {
            //this.WriteToFile();
            //var a = this.GetResult(Type.football);

        }

        public List<ResultForForks> GetResult(SportType sportType)
        {
            List<ResultForForks> result = new List<ResultForForks>();
            //var a = GetNameTeamsAndDateAsync(sportType).Result;
            //this.Show(a);
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
        public static async Task GetHtmlDocumentAsync(string url, string namefile)
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
        }

        //data-selection-price=
        //                              <div class="member-name nowrap " data-ellipsis='{}'>
        //                              <div class="member-name nowrap " data-ellipsis='{}'>
        private const string forTeam = "<div class=\"member-name nowrap \" data-ellipsis='{}'>";
        private const string forDate = "<td class=\"date\">";
        private const int countCoff1 = 10;
        private const int countCoff2 = 6;


        public async Task<List<ResultForForks>> GetNameTeamsAndDateAsync(SportType sportType, Site site)
        {
            List<ResultForForks> result = new List<ResultForForks>();
            string url = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";
            string namefile = "Default.html";
            #region     switch

            switch (site)
            {
                case Site.MarathonBet:
                    switch (sportType)//bad way for bad code
                    {
                        case SportType.Football:
                            namefile = "Football.html";
                            url = "https://www.marathonbet.com/su/popular/Football/?menu=true";
                            break;
                        case SportType.Basketball:
                            namefile = "Basketball.html";
                            url = "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
                            break;
                        case SportType.Hockey:
                            namefile = "Hokey.html";
                            url = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";
                            break;
                        case SportType.Tennis:
                            namefile = "Tenis.html";
                            url = "https://www.marathonbet.com/su/popular/Tennis/?menu=true";
                            break;
                        case SportType.Volleyball:
                            namefile = "Volleyball.html";
                            url = "https://www.marathonbet.com/su/popular/Volleyball/?menu=true";
                            break;
                    }
                   // await GetHtmlDocumentAsync(url, namefile).ConfigureAwait(false);
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

                        if (name1 != null && name2 != null && date != null && res != null)
                        {
                            if (index >= countTypeCoff.Count)
                                index = 0;
                            //var a=countTypeCoff[index];
                            result.Add(new ResultForForks(name1, name2, date, " - ", res));
                            res = null;
                            i++;
                        }

                        if (name1 != null && name2 != null && date != null)
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

                                teams.Add(new Teams_Football_Hokey(name1, name2, date, win1, x, win2, x_win1, x_win2, win1_win2, fora1, fora2, less, more));
                                name1 = null;
                                name2 = null;
                                date = null;
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

                                teams.Add(new Teams_Tenis_Volleyball_Basketball(name1, name2, date, win1, win2, fora1, fora2, less, more));
                                name1 = null;
                                name2 = null;
                                date = null;
                                koff = new List<string>();
                            }

                        }

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


    public class ResultForForks
    {
        public string Event { get; set; }
        public string Type { get; set; }
        public string Coef { get; set; }

        public ResultForForks()
        { }
        //  X1 X2 1 2 
        public ResultForForks(string nameTeam1, string nameTeam2, string date, string nameCoff, string value)
        {
            this.Event = nameTeam1 + "-" + nameTeam2 + "-" + date;
            this.Type = nameCoff;
            this.Coef = value;
        }
    }

    public class Teams
    {
        protected string nameTeam1;
        protected string nameTeam2;
        protected string date;

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
        public Teams(string nameTeam1, string nameTeam2, string date,
            string win1, string win2,
            string fora1, string fora2, string less, string more)
        {
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
