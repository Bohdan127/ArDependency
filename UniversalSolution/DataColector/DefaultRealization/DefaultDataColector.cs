using DataColector.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace DataColector.DefaultRealization
{
    public class DefaultDataColector : IDataColector
    {
        public JArray GetJsonArray(string url)
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
    }
}