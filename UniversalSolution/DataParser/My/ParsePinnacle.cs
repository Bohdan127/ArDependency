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
        public static async Task GetHtmlDocumentAsync(string url)
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
            return teams;

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

    }
    public class Teams
    {
        private string nameTeam1;
        private string nameTeam2;
        private string date;
        public double win1 { get; set; }
        public string x;
        public double win2 { get; set; }
        public string x_win1;
        public string x_win2;
        public string win1_win2;
        public string fora1;
        public string fora2;
        public string less;
        public string more;
        public double fork { get; set; }
        public Teams() { }
        public Teams(string nameTeam1, string nameTeam2, string date,
            string win1, string x, string win2,
            string x_win1, string x_win2, string win1_win2,
            string fora1, string fora2, string less, string more)
        {
            this.nameTeam1 = nameTeam1;
            this.nameTeam2 = nameTeam2;
            this.date = date;
            this.win1 = win1.ConvertToDouble();
            this.x = x;
            this.win2 = win2.ConvertToDouble();
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
