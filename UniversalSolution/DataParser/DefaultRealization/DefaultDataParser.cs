using DataParser.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System;
using WatiN.Core;
using System.Linq;
using System.Threading;
using System.Text;

namespace DataParser.DefaultRealization
{
    public class DefaultDataParser : IDataParser
    {
        //Результуючий масив матчів зпарсений з json
        private List<IDataMatch> matchesRes;

        private IDataMatch mainMatchInst;

        public DefaultDataParser(IDataMatch _mainMatchInst)
        {
            matchesRes = new List<IDataMatch>();
            mainMatchInst = _mainMatchInst;
        }

        public List<IDataMatch> ParseData(JArray matchesJson)
        {
            //Перебираєм всі матчі і запаковуєм в об'єкти
            while (matchesJson != null && matchesJson.HasValues)
            {
                IDataMatch match = mainMatchInst.GetInstance();
                //Мабуть це піде в окремий метод, але поітм
                JToken val = matchesJson.First;
                match.Id = (int)val["Num"];
                match.Champ = val["ChampEng"].ToString();
                match.Sportname = val["SportNameEng"].ToString();
                match.Opp1Name = val["Opp1Eng"].ToString();
                match.Opp2Name = val["Opp2Eng"].ToString();
                foreach (var ev in val["Events"])
                {
                    switch ((int)ev["T"])
                    {
                        case 1:
                            match.P1 = (double)(ev["C"] ?? 0);
                            break;

                        case 2:
                            match.X = (double)(ev["C"] ?? 0);
                            break;

                        case 3:
                            match.P2 = (double)(ev["C"] ?? 0);
                            break;

                        case 4:
                            match.X1 = (double)(ev["C"] ?? 0);
                            break;

                        case 5:
                            match.I2 = (double)(ev["C"] ?? 0);
                            break;

                        case 6:
                            match.X2 = (double)(ev["C"] ?? 0);
                            break;

                        case 7:
                            match.I = (double)(ev["C"] ?? 0);
                            match.Fora1 = (double)(ev["P"] ?? 0);
                            break;

                        case 8:
                            match.II = (double)(ev["C"] ?? 0);
                            match.Fora2 = (double)(ev["P"] ?? 0);
                            break;

                        case 9:
                            match.B = (double)(ev["C"] ?? 0);
                            match.Total = (double)(ev["P"] ?? 0);
                            break;

                        case 10:
                            match.M = (double)(ev["C"] ?? 0);
                            match.Total = (double)(ev["P"] ?? 0);
                            break;
                    }
                }
                matchesRes.Add(match);
                matchesJson.Remove(val);           
            }
            return matchesRes;
        }

        public List<GenericMatch> OlimpDataParser(HtmlDocument doc)
        {
            List <GenericMatch> matchResult = new List<GenericMatch>();
            string sportName;
            string tournamentName;

            //todo мож регуляркою шукати зразу потрібне
            //string allHTML = browser.Body.OuterHtml; 

            //todo При деяких запусках чомусь масив елементів пустий  ??????????????
            var liveMatchList = doc.GetElementbyId("srlive_matchlist").ChildNodes.Where(x => x.Name == "div");
            var loadNode = liveMatchList.FirstOrDefault();
            if (loadNode == null || loadNode.InnerText.Trim() == "Loading")
            {
                liveMatchList = GetHtmlDocument().GetElementbyId("srlive_matchlist").ChildNodes.Where(x => x.Name == "div");
            }
            //todo Деколи коли сторінка повільно прогружається в mainDiv присвоюється не список діві
            //а Lading елемент, і тоді при обході цього масива отримуєм екзепшин
            foreach (var sportSection in liveMatchList)
            {
                //Назва спорту до якої відносяться матчі
                sportName = sportSection.ChildNodes[1].ChildNodes[3].InnerText;

                foreach (var tournament in sportSection.ChildNodes[3].ChildNodes.Where(x => x.Name == "div"))
                {
                    //Назва турніру
                    tournamentName = tournament.ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;

                    foreach (var match in tournament.ChildNodes[1].ChildNodes[5].ChildNodes[1].ChildNodes.Where(x => x.Name == "td"))
                    {
                    }
                }
            }
            return matchResult;
        }

        public static string GetHtml(string Url)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Settings.Instance.MakeNewIeInstanceVisible = false; //невідображати браузер
            Settings.WaitForCompleteTimeOut = 999999999; //todo хз нашо це було так в прикладі
            using (var browser = new IE(Url))
            {
                HtmlDocument doc = new HtmlDocument();
                //затримка 1 секунда, щоби не ловився блок Lading
                browser.WaitForComplete();
                Thread.Sleep(1000);
                string html = browser.Body.OuterHtml;
                browser.ForceClose();
                return html;
            }
        }
        public static HtmlDocument GetHtmlDocument()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetHtml("http://ls.betradar.com/ls/livescore/?/olimp/en/page#domain=olimp.kz"));
            return doc;
        }
    }
}