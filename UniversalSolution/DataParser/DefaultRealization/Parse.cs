using DataParser.Enums;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DataParser.DefaultRealization
{
    public static class Tags
    {
        public static string BOOKER = "class=\"booker\"";
        public static string TIME = "class=\"time\"";
        public static string EVENT = "class=\"event";
        public static string COEFF = "class=\"coeff\"";
        public static string VALUE = "class=\"value odd_record_";

    }
    public class Surebet
    {
        public string Booker { get; set; }
        public string Time { get; set; }
        public string Event { get; set; }
        public string Coeff { get; set; }
        public string Value { get; set; }

        public Surebet() { }
        public Surebet(string booker, string time, string _event, string coeff, string value)
        {
            this.Booker = booker;
            this.Time = time;
            this.Event = _event;
            this.Coeff = coeff;
            this.Value = value;
        }
    }
    public class TwoBooker
    {
        public Surebet Pinnacle { get; set; }
        public Surebet Marathon { get; set; }
        public TwoBooker() { }
        public TwoBooker(Surebet pinnacle, Surebet marathon)
        {
            this.Pinnacle = pinnacle;
            this.Marathon = marathon;
        }
    }
    public class Parse
    {
        public List<Fork> GetForks(SportType sportType)
        {
            List<TwoBooker> forks = new List<TwoBooker>();
            string url = UrlAndNameFile(sportType);
            string html = HtmlAsync(url).Result;
            string[] lines = html.Split('\n');


            string booker = null;
            string time = null;
            string _event = null;
            string coeff = null;
            string value = null;

            Surebet pinnacle = null;
            Surebet marathon = null;

            foreach (var line in lines)
            {
                if (line.Contains(Tags.BOOKER))
                    booker = GetAttribut(line);
                else if (line.Contains(Tags.TIME))
                    time = GetAttribut(line,true);
                else if (line.Contains(Tags.EVENT))
                    _event = GetAttribut(line);
                else if (line.Contains(Tags.COEFF))
                    coeff = GetAttribut(line);
                else if (line.Contains(Tags.VALUE))
                    value = GetAttribut(line);
                if (!String.IsNullOrEmpty(booker)
                    && !String.IsNullOrEmpty(time)
                    && !String.IsNullOrEmpty(_event)
                    && !String.IsNullOrEmpty(coeff)
                    && !String.IsNullOrEmpty(value))
                {
                    if (pinnacle == null)
                    {
                        pinnacle = new Surebet(booker, time, _event, ClearCoeff(coeff), value);
                    }
                    else if (marathon == null)
                        marathon = new Surebet(booker, time, _event, ClearCoeff(coeff), value);
                    booker = null;
                    time = null;
                    _event = null;
                    coeff = null;
                    value = null;
                }

                if (pinnacle != null && marathon != null)
                {
                    forks.Add(new TwoBooker(pinnacle, marathon));
                    pinnacle = null;
                    marathon = null;
                }
            }
            return ConvertToForks(forks, sportType);
        }


        private List<Fork> ConvertToForks(List<TwoBooker> listToConcert, SportType sportType)
        {
            return listToConcert.Select(twoBooker => new Fork
            {
                Event = twoBooker.Pinnacle.Event,
                TypeFirst = twoBooker.Marathon.Coeff,
                CoefFirst = twoBooker.Marathon.Value,
                TypeSecond = twoBooker.Pinnacle.Coeff,
                CoefSecond = twoBooker.Pinnacle.Value,
                Sport = sportType.ToString(),
                MatchDateTime = twoBooker.Marathon.Time,
                BookmakerFirst = "https://www.marathonbet.com/",
                BookmakerSecond = "http://www.pinnaclesports.com/",
                Type = ForkType.Current
            }).ToList();
        }

        public void Show(List<TwoBooker> forks)
        {
            int index = 1;
            foreach (var fork in forks)
            {
                Console.Write(index + "     ");
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4}",
                    fork.Pinnacle.Booker,
                    fork.Pinnacle.Time,
                    fork.Pinnacle.Event,
                    fork.Pinnacle.Coeff,
                    fork.Pinnacle.Value);
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4}",
                   fork.Marathon.Booker,
                   fork.Marathon.Time,
                   fork.Marathon.Event,
                   fork.Marathon.Coeff,
                   fork.Marathon.Value);
                Console.WriteLine();
                index++;
            }
        }
        private static string ClearCoeff(string line)
        {
            if (line.Contains(' '))
                line =  line.Split(' ')[0];
            if (line.Contains("&minus;"))
                line = line.Replace("&minus;", "-");
            if (line.Contains("&plus;"))
                line = line.Replace("&plus;", "+");
            if (line.Contains("&nbsp;/&nbsp;DNB"))
                line = line.Replace("&nbsp;/&nbsp;DNB", "");
            if (line.Contains("Тм"))
                line = line.Replace("Тм", "TU");
            if (line.Contains("Тб"))
                line = line.Replace("Тб", "TO");
            if (line.Contains("Ф"))
                line = line.Replace("Ф", "F");
            if (line.Contains("П"))
                line = line.Replace("П", "");
            var index = line.IndexOf(')');

            if (index != -1)
                line = line.Substring(0, index + 1);
            return line;
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
            using (StreamWriter sw = new StreamWriter("XXX.txt"))
            {
                sw.WriteLine(HTML);
                sw.Close();
            }
            return HTML;
        }
        private static string GetAttribut(string line, bool date = false)
        {
            bool isStartTag = false;
            bool isFinish = false;
            string result = "";
            foreach (var l in line)
            {
                if (l == '<')
                    isStartTag = false;
                if (isStartTag)
                    result += l;
                if (l == '>')
                    isStartTag = true;
                if (String.IsNullOrEmpty(result.Trim()) && !isStartTag)
                    result = "";
                if (!String.IsNullOrEmpty(result.Trim()) && !isStartTag)
                {
                    isFinish = true;
                    result += (date && !result.Contains("/2016") )? "/2016 ":"";
                    
                }
                if (isFinish && !date)
                    return result;
            }
            return !date?"":result;
        }

        private string UrlAndNameFile(SportType sportType)
        {
            string url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=0+1+10+11+12+13+14+16+17+18+19+2+20+21+22+23+24+25+26+27+28+29+3+30+31+32+33+4+5+6+7+8+9&selector%5Bmonitor%5D=off&commit=Фильтровать";
            switch (sportType)
            {
                case SportType.Soccer:
                    url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=0+1+10+11+12+13+14+16+17+18+19+2+20+21+22+23+24+25+26+27+28+29+3+30+31+32+33+4+5+6+7+8+9&selector%5Bmonitor%5D=off&commit=Фильтровать";
                    break;

                case SportType.Basketball:
                    url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=32+31+0+5+1+6+7+3+20+28+33+25+21+24+22+17+27+13+15+30+16+12+18+8+14+10+2+11+29+19+26+23+9&selector%5Bmonitor%5D=off";
                    break;

                case SportType.Hockey:
                    url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=32+31+0+5+1+6+4+7+3+20+28+33+25+21+24+22+17+27+13+15+30+16+12+8+14+10+2+11+29+19+26+23+9&selector%5Bmonitor%5D=off";
                    break;

                case SportType.Tennis:
                    url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=32+31+0+5+1+6+4+7+3+20+28+33+25+21+24+17+27+13+15+30+16+12+18+8+14+10+2+11+29+19+26+23+9&selector%5Bmonitor%5D=off";
                    break;

                case SportType.Volleyball:
                    url = "https://ru.surebet.com/surebets?utf8=✓&order=created_at&selector%5Boutcomes%5D%5B%5D=2&selector%5Bmin_profit%5D=0.0&selector%5Bmax_profit%5D=&selector%5Bsettled_in%5D=0&selector%5Bbookies_settings%5D=0%3A20%3A%3A%3B0%3A52%3A%3A%3B0%3A13%3A%3A%3B0%3A19%3A%3A%3B0%3A34%3A%3A%3B0%3A54%3A%3A%3B0%3A42%3A%3A%3B0%3A21%3A%3A%3B0%3A26%3A%3A%3B0%3A23%3A%3A%3B0%3A0%3A%3A%3B0%3A32%3A%3A%3B0%3A29%3A%3A%3B0%3A10%3A%3A%3B0%3A45%3A%3A%3B0%3A58%3A%3A%3B0%3A14%3A%3A%3B0%3A56%3A%3A%3B0%3A11%3A%3A%3B0%3A38%3A%3A%3B0%3A55%3A%3A%3B0%3A33%3A%3A%3B0%3A49%3A%3A%3B0%3A62%3A%3A%3B0%3A12%3A%3A%3B0%3A46%3A%3A%3B0%3A24%3A%3A%3B0%3A5%3A%3A%3B0%3A6%3A%3A%3B0%3A4%3A%3A%3B0%3A22%3A%3A%3B0%3A30%3A%3A%3B0%3A15%3A%3A%3B0%3A50%3A%3A%3B0%3A9%3A%3A%3B0%3A41%3A%3A%3B4%3A3%3A%3A%3B0%3A8%3A%3A%3B0%3A63%3A%3A%3B0%3A61%3A%3A%3B0%3A47%3A%3A%3B0%3A39%3A%3A%3B0%3A31%3A%3A%3B0%3A57%3A%3A%3B0%3A51%3A%3A%3B0%3A2%3A%3A%3B4%3A7%3A%3A%3B0%3A1%3A%3A%3B0%3A25%3A%3A%3B0%3A35%3A%3A%3B0%3A16%3A%3A%3B0%3A40%3A%3A%3B0%3A37%3A%3A%3B0%3A18%3A%3A%3B0%3A59%3A%3A%3B0%3A64%3A%3A%3B0%3A17%3A%3A%3B0%3A53%3A%3A%3B0%3A28%3A%3A%3B0%3A44%3A%3A%3B0%3A36%3A%3A%3B0%3A27%3A%3A&selector%5Bexclude_sports_ids_str%5D=32+31+0+5+1+6+4+7+3+20+28+33+25+21+22+17+27+13+15+30+16+12+18+8+14+10+2+11+29+19+26+23+9&selector%5Bmonitor%5D=off";
                    break;
            }
            return url;
        }
    }
}
