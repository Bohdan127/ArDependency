using DataParser.Enums;
using DataSaver.RavenDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DataParser.MY;

namespace ParseAPI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ParsePinnacle p = new ParsePinnacle();
            p.ShowForks(p.InitiAsync(SportType.Basketball).Result);
            Console.ReadKey();
        }
    }
}