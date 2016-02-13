using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using WatiN.Core;

namespace DataParser.DefaultRealization
{
    public class DefaultDataParser
    {
        public const string olimpUrl = "http://ls.betradar.com/ls/livescore/?/olimp/en/page#domain=olimp.kz";
        public const string ua1xetUrl = "https://ua.1xbet.com/LiveFeed/Get1x2?sportId=0&sports=&champId=0&tf=1000000&count=50&cnt=10&lng=ru&cfview=0";
        public const string fonbetUrl = "https://live.fonbet.com/";
        public const string williamhill = "http://sports.williamhill.com/bet/ru/betlive/all"; 

        public List<GenericMatch> GetDataForSomeSites(List<Office> sites)
        {                 //   todo це гавнокод вищої степені але зарза на це немає часу
            var resList = new List<GenericMatch>();

            if (sites.Any(s => s == Office.fonbetCom))
            { }
            if (sites.Any(s => s == Office.olimpKz))
            {
                resList.AddRange(OlimpDataParser());
            }
            if (sites.Any(s => s == Office.ru10betCom))
            { }
            if (sites.Any(s => s == Office.ua1xetCom))
            {
                resList.AddRange(Ua1xetComDataParser());
            }
            if (sites.Any(s => s == Office.williamhillCom))
            { }

            return resList;
        }


        public List<GenericMatch> Ua1xetComDataParser()
        {
            JArray matchesJson = GetJsonArray(ua1xetUrl);
            List<GenericMatch> matchListResult = new List<GenericMatch>();
            //Перебираєм всі матчі і запаковуєм в об'єкти
            while (matchesJson != null && matchesJson.HasValues)
            {
                GenericMatch match = new GenericMatch();
                //Мабуть це піде в окремий метод, але поітм
                JToken val = matchesJson.First;
                match.Id = (int)val["Num"];
                match.Office = Office.ua1xetCom.ToString();
                match.SportName = val["SportNameEng"].ToString();
                match.Champ = val["ChampEng"].ToString();
                matchListResult.Add(match);
                matchesJson.Remove(val);
            }
            return matchListResult;
        }

        public List<GenericMatch> OlimpDataParser()
        {
            List<GenericMatch> matchListResult = new List<GenericMatch>();

            HtmlDocument doc = GetHtmlDocument(olimpUrl);

            //todo мож регуляркою шукати зразу потрібне
            //string allHTML = browser.Body.OuterHtml; 

            //todo При деяких запусках чомусь масив елементів пустий  ??????????????
            var liveMatchList = doc.GetElementbyId("srlive_matchlist").ChildNodes.Where(x => x.Name == "div");
            var loadNode = liveMatchList?.FirstOrDefault();
            if (loadNode != null && loadNode.InnerText.Trim() != "Loading")
            {
                //todo Деколи коли сторінка повільно прогружається в mainDiv присвоюється не список діві
                //а Lading елемент, і тоді при обході цього масива отримуєм екзепшин

                foreach (HtmlNode sportSection in liveMatchList)
                {
                    //Назва спорту до якої відносяться матчі
                    string sportName = sportSection.ChildNodes[1].ChildNodes[3].InnerText;

                    foreach (HtmlNode tournament in sportSection.ChildNodes[3].ChildNodes.Where(x => x.Name == "div"))
                    {
                        //Назва турніру
                        string champ = tournament.ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
                        foreach (HtmlNode match in tournament.ChildNodes[1].ChildNodes[5].ChildNodes.Where(x => x.Name == "tr"))
                        {
                            GenericMatch matchResult = new GenericMatch();
                            //todo Берем Id тегу tr де знаходиться 1 матч, і відкидаємо лишнє, 
                            //бо він має вигляд: match-8848438
                            matchResult.Id = int.Parse(match.Attributes["id"].Value.Substring(6));
                            matchResult.Office = Office.olimpKz.ToString();
                            matchResult.SportName = sportName;
                            matchResult.Champ = champ;
                            matchResult.GameTime = match.ChildNodes[1].InnerText;
                            matchListResult.Add(matchResult);
                        }
                    }
                }
            }
            return matchListResult;
        }

        public List<GenericMatch> FonbetDataParser()
        {
            List<GenericMatch> matchListResult = new List<GenericMatch>();

            HtmlDocument doc = GetHtmlDocument(fonbetUrl);

            //todo мож регуляркою шукати зразу потрібне
            //string allHTML = browser.Body.OuterHtml; 

            //todo При деяких запусках чомусь масив елементів пустий  ??????????????
            HtmlNode liveMatchList = doc.GetElementbyId("lineContainer");
            if (liveMatchList != null && liveMatchList.ChildNodes.Count != 0)
            {
                string sportName="";
                var list = liveMatchList.ChildNodes[0].ChildNodes[0].ChildNodes;
                foreach (HtmlNode tr in liveMatchList.ChildNodes[0].ChildNodes[0].ChildNodes)
                {
                    if(tr.Attributes["class"].Value == "trSegment")
                    {
                        if(tr.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText != sportName)
                        {
                            sportName = tr.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;
                        }
                    }
                    if (tr.Attributes["class"].Value.Substring(0, 7) == "trEvent")
                    {
                        GenericMatch matchResult = new GenericMatch();
                        matchResult.Id = int.Parse(tr.Attributes["id"].Value.Substring(5));
                        matchResult.Office = Office.fonbetCom.ToString();
                        //todo magic number
                        var index = sportName.IndexOf(".");
                        matchResult.SportName = sportName.Substring(0, index);
                        matchResult.Champ = sportName.Substring(index+1);
                        //todo Беру час гри просто в трінгу тобто не реальний чи це підходить хз ???????
                        matchResult.GameTime = tr.ChildNodes[2].ChildNodes[1].FirstChild.InnerText;
                        matchListResult.Add(matchResult);
                    }
                }
            }
            return matchListResult;
        }
        
        public List<GenericMatch> WilliamhillDataParser()
        {
            List<GenericMatch> matchListResult = new List<GenericMatch>();

            HtmlDocument doc = GetHtmlDocument(williamhill);
            
            var liveMatchList = doc.GetElementbyId("sports_holder").ChildNodes.Where(x=>x.Name == "div");
            foreach (var sport in liveMatchList)
            {
                var sportname = sport.ChildNodes[1].ChildNodes[1].InnerText;
                foreach(var champ in sport.ChildNodes[3].ChildNodes.Where(x => x.Name == "div"))
                {
                    var champName = champ.ChildNodes[1].InnerText;
                    try
                    {
                        foreach (var match in champ.ChildNodes[5].ChildNodes[3].ChildNodes[1].ChildNodes.FindFirst("tbody").ChildNodes
                        .Where(x => x.Name == "tr" && x.Attributes["class"].Value == "rowLive"))
                        //todo хз чи потрібна ще ця перевірка && x.Attributes["class"].Value == "rowLive"
                        //але вроді при ній менше екзепшинів, але не понятно чи потрібні дані з rowOdd
                        {
                            GenericMatch matchResult = new GenericMatch();
                            matchResult.Id = int.Parse(match.Attributes["id"].Value.Substring(7));
                            matchResult.GameTime = match.ChildNodes[1].ChildNodes[1].InnerText;
                            matchResult.SportName = sportname;
                            matchResult.Champ = champName;
                            matchResult.Office = Office.williamhillCom.ToString();
                            matchListResult.Add(matchResult);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return matchListResult;
        }

        #region AdditionalMethod
        public static string GetHtml(string Url)
        {
            Settings.Instance.MakeNewIeInstanceVisible = false; //невідображати браузер
            Settings.WaitForCompleteTimeOut = 999999999; //todo хз нашо це було так в прикладі
            using (var browser = new IE(Url))
            {
                //затримка 1 секунда, щоби не ловився блок Lading
                browser.WaitForComplete();
                Thread.Sleep(1000);
                string html = browser.Body.OuterHtml;
                browser.ForceClose();
                return html;
            }
        }
        public static HtmlDocument GetHtmlDocument(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetHtml(url));
            return doc;
        }
        public static JArray GetJsonArray(string url)
        {
            //Url що повертає json з необхідною інформацією
            string uri = url;

            var webClient = new WebClient();
            try
            {
                //Отримуємо json за запитом
                string json = webClient.DownloadString(uri);
                JObject matches = (JObject)JsonConvert.DeserializeObject(json);
                if (matches["Success"].ToString() == "True")
                {
                    return (JArray)matches["Value"];
                }
                return new JArray();//todo повернути щось якщо Success = folse
            }
            catch (Exception ex)
            {
                Console.Write("Error downloading content");
                throw ex; //todo Щось повертати якшо виник Exception
            }
        }
        #endregion
    }
}