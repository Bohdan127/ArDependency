using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using WatiN.Core;
using DataParser.DefaultRealization;

namespace ParseAPI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DefaultDataParser dataParser = new DefaultDataParser();
            List<GenericMatch> resultList = new List<GenericMatch>();
            resultList = dataParser.OlimpDataParser();
            //resultList = dataParser.FonbetDataParser();

            Console.ReadLine();
        }
        public static string Update(int id = 0)
        {
              //  string uri = "https://ua.1xbet.com/LiveFeed/Get1x2?sportId=0&sports=&champId=0&tf=1000000&count=50&cnt=10&lng=ru&cfview=0";

              //var webClient = new WebClient();
            try
            {
                //string result = webClient.DownloadString(uri);

                //if (!string.IsNullOrEmpty(result))
                //{
                    ModelParser.GetModel((new StreamReader(@"..\..\1.txt")).ReadToEnd());
                //return result;
                //}
            }
            catch (Exception ex)
            {
                Console.Write("Error downloading content");
                Console.Write(ex.Message);
            }
            return "";
        }
    }
    public static class ScoreModel
    {
        public static List<string> SportTypes { get; set; }
        public static List<Dictionary<string, string>> ValueKeys { get; set; }
        public static List<Dictionary<string, string>> EventKeys { get; set; }

        static ScoreModel()
        {
            SportTypes = new List<string>();
            ValueKeys = new List<Dictionary<string, string>>();
            EventKeys = new List<Dictionary<string, string>>();
        }
    }

    public static class ModelParser
    {
        public static void ssss(string source, JArray arr)
        {
            var tmpDic = new Dictionary<string, string>();
            while (arr != null && arr.HasValues)
            {
                var val = arr.First;
                //func1((JArray)val["Events"]))
                ScoreModel.SportTypes.Add(val["Num"].ToString());
                arr.Remove(val);
            }

        }
        public static void GetModel(string info)
        {
            ScoreModel.SportTypes = new List<string>();
            //ScoreModel.SportTypes.Add(info);
            JObject o = (JObject)JsonConvert.DeserializeObject(info);
            if (o["Success"].ToString() == "True")
            {
                ssss("Value", (JArray)o["Value"]);

            }
            var wr = new StreamWriter(@"..\..\out.txt", false);
            foreach (var item in ScoreModel.SportTypes)
            {
                    wr.WriteLine(item);
            }
           
            wr.Close();
            Console.WriteLine("Done");
            //        foreach (var item in o)
            //{

            //}
            //var type = o["params"]["sport"];
            //var Item = o["sportItem"]["name"];
            //var tours = o["sportItem"]["tournaments"];
            //foreach (var el in tours)
            //{
            //    foreach (var ch in el["events"])
            //    {

            //    }
            //}
        }
    }
}
