using DataParser.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ParseAPI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var request =
                (HttpWebRequest)
                    WebRequest.Create(
                         "https://api.pinnaclesports.com/v1/odds?sportid=" + (int)SportType.Volleyball);   //for totals
                                                                                                           // "https://api.pinnaclesports.com/v1/fixtures?sportid=" + (int)SportType.Tennis); //for team name
            string credentials = String.Format("{0}:{1}", "VB794327", "artem89@");
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.Headers.Add("Authorization", authorization);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)";
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            //string postJson =
            //"{\"uniqueRequestId\":\"3ca3e7a7-12e1-4907-8b84-00f02e814b1d\"," +
            //"\"acceptBetterLine\":\"TRUE\"," +
            //"\"stake\":150," +
            //"\"winRiskStake\":\"WIN\"," +
            //"\"lineId\":104520034," +
            //"\"sportId\":29," +
            //"\"eventId\":311458946," +
            //"\"periodNumber\":0," +
            //"\"betType\":\"SPREAD\"," +
            //"\"team\":\"TEAM1\"," +
            //"\"oddsFormat\":\"AMERICAN\"" +
            //"}";

            //byte[] byteArray = Encoding.UTF8.GetBytes(postJson);
            //Stream dataStream = request.GetRequestStream();
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            //var stream = response.GetResponseStream();
            //string responseBody;
            //using (var reader = new StreamReader(stream))
            //{
            //    var s = reader.ReadToEnd();
            //    new StreamWriter(@"C:\Users\Lenovo-PCv3\Desktop\Totals_Volleyball.txt").Write(s);
            //}
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