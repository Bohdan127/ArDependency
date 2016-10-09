using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {

            var patterns = CultureInfo.GetCultureInfo("ru-RU").
                        DateTimeFormat.
                        GetAllDateTimePatterns();
            var matchDateTime = "17окт11:30";
            var day = matchDateTime.Substring(0, 2);
            string month;
            switch (matchDateTime.Substring(2, 3))
            {
                case "янв":
                    month = "01";
                    break;
                case "фев":
                    month = "02";
                    break;
                case "мар":
                    month = "03";
                    break;
                case "апр":
                    month = "04";
                    break;
                case "май":
                    month = "05";
                    break;
                case "июн":
                    month = "06";
                    break;
                case "июл":
                    month = "07";
                    break;
                case "авг":
                    month = "08";
                    break;
                case "сен":
                    month = "09";
                    break;
                case "окт":
                    month = "10";
                    break;
                case "ноя":
                    month = "11";
                    break;
                case "дек":
                    month = "12";
                    break;
                default:
                    month = matchDateTime.Substring(2, 3);
                    break;
            }
            var time = matchDateTime.Substring(5);
            var fullTime = $"{day}/{month}/{DateTime.Now.Year - 2000} {time}";
            foreach (var format in patterns)
            {
                try
                {
                    var dt = DateTime.ParseExact(fullTime, "dd/MM/yy HH:mm",
                        new CultureInfo("ru-RU"));
                Console.WriteLine(format);
                    Console.WriteLine("Done = " + dt);
                }
                catch
                {
                   // Console.WriteLine("fail");
                }
            }
            Console.WriteLine("Done All");
            Console.ReadKey();
        }

        public static short GetStringSimilarityInPercent(string first, string second, bool clearSpecSymbols, string date_second = "04-10-2016", string date_first = "04-10-2016")
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