using System;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {

            var f = "San Miguel Beermen - Barangay Ginebra San Miguel";
            var s = "San Miguel Beermen - Barangay Ginebra";
            Console.WriteLine("-----------------------------------------1");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Admiral - Torpedo Nizhniy Novgorod";
            s = "Admiral Vladivostok - Torpedo Nizhny Novgorod";
            Console.WriteLine("-----------------------------------------2");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Brugge - Porto";
            s = "Club Brugge - Porto";
            Console.WriteLine("-----------------------------------------3");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Sporting Lisboa - Borussia Dortmund";
            s = "Sporting Lisbon - Borussia Dortmund";
            Console.WriteLine("-----------------------------------------4");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Morecambe - Stoke City Reserves";
            s = "Morecambe - Stoke City U23";
            Console.WriteLine("-----------------------------------------5");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Russia U-19 - Finland U-19";
            s = "Iceland - Finland";
            Console.WriteLine("-----------------------------------------6");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Lithuania U-19 - Bosnia and Herzegovina U-19";
            s = "Belgium - Bosnia-Herzegovina";
            Console.WriteLine("-----------------------------------------7");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "China PR - Syria";
            s = "China - Syria";
            Console.WriteLine("-----------------------------------------8");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Republic of Macedonia - Israel";
            s = "FYR Macedonia - Israel";
            Console.WriteLine("-----------------------------------------9");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Olympiacos Pireas - Astana";
            s = "Olympiacos - FC Astana";
            Console.WriteLine("-----------------------------------------10");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Saint-Etienne - Gabala";
            s = "Saint Etienne - Qabala PFC";
            Console.WriteLine("-----------------------------------------11");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Feyenoord - Zorya Lugansk";
            s = "Feyenoord - Zorya Luhansk";
            Console.WriteLine("-----------------------------------------12");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Liverpool - West Bromwich Albion";
            s = "Liverpool - W.B.A"; Console.WriteLine("-----------------------------------------13");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Tottenham Hotspur - Leicester City";
            s = "W.B.A - Tottenham Hotspur";
            Console.WriteLine("-----------------------------------------14");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Peterborough United - Bury";
            s = "Peterborough United - Milton Keynes Dons";
            Console.WriteLine("-----------------------------------------15");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Bradford City - Shrewsbury Town";
            s = "Bradford City - Bury";
            Console.WriteLine("-----------------------------------------16");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Mansfield - Notts County";
            s = "Mansfield Town - Notts County";
            Console.WriteLine("-----------------------------------------17");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "North Ferriby United - Chester";
            s = "North Ferriby Utd - Chester";
            Console.WriteLine("-----------------------------------------18");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));
            f = "Atalanta - Inter";
            s = "Atalanta - Inter Milan";
            Console.WriteLine("-----------------------------------------19");
            Console.WriteLine(GetStringSimilarityInPercent(f, s, true));

            /* IWebDriver _driver = new InternetExplorerDriver();
             _driver.Navigate().GoToUrl("http://www.bing.com/");//_driver.FindElement(By.Id("sb_form_q")).SendKeys("Brian harry blog");
             _driver.FindElement(By.Id("sb_form_go")).Click();
             Console.WriteLine(_driver.FindElement(By.XPath("//ol[@id='b_results']/li/h2/a")).Text);*/

            Console.ReadKey();
        }

        public static short GetStringSimilarityInPercent(string first,string second,  bool clearSpecSymbols,string date_second= "04-10-2016", string date_first= "04-10-2016")
        {
            if (date_second != date_first)
                return 0;
            var rgx2 = new Regex(@"U|O^[A-Za-z]?\d+");
            if ((rgx2.IsMatch(first) && !rgx2.IsMatch(second)) || (!rgx2.IsMatch(first) && rgx2.IsMatch(second)))
                return 0;
           
            if (clearSpecSymbols)
            {
                var rgx = new Regex(@"[\%\/\\\&\?\,\'\;\:\!\-\|\.\,\@\#\(\)\s]");
                first = rgx.Replace(first.ToLower(), "");
                second = rgx.Replace(second.ToLower(), "");
            }
            else
            {
                first = first.ToLower().Trim();
                second = second.ToLower().Trim();
            }

            var isFirst = first.Length < second.Length;
            var sameLength = 0;

            if (isFirst)
                for (var i = 0; i < first.Length; i++)
                {
                    for (var j = 0; j < second.Length; j++)
                    {
                        if (i >= first.Length)
                            break;

                        while (first[i] == second[j])
                        {
                            i++;
                            j++;
                            if (i >= first.Length || j >= second.Length)
                                break;
                            if (first[i] == second[j])
                                sameLength++;
                        }
                    }
                }
            else
                for (var i = 0; i < second.Length; i++)
                {
                    for (var j = 0; j < first.Length; j++)
                    {
                        if (i >= second.Length)
                            break;

                        while (second[i] == first[j])
                        {
                            i++;
                            j++;
                            if (i >= second.Length || j >= first.Length)
                                break;
                            if (second[i] == first[j])
                                sameLength++;
                        }
                    }
                }
            if (sameLength != 0)
                sameLength++;
            double length = (isFirst
                ? first.Length
                : second.Length);
            return Convert.ToInt16(sameLength / length * 100);
        }

    }
}