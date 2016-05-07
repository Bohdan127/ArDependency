using DataParser.DefaultRealization;
using DataParser.Enums;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;

namespace DataLoader
{
    class Program
    {
        public static string UserLogin { get; set; }
        public static string UserPass { get; set; }

        private static PinnacleSportsDataParser pinnacle;

        static void Main(string[] args)
        {
            Console.WriteLine("DataLoader Start");
            pinnacle = new PinnacleSportsDataParser(new ConverterFormulas());
            GetUserData();
            StartLoad();
        }

        private static void GetUserData()
        {
            Console.WriteLine("Please enter user login");
            UserLogin = Console.ReadLine();

            Console.WriteLine("Please enter user password");
            UserPass = Console.ReadLine();
        }

        private static void StartLoad()
        {
            while (true)
            {
                Console.WriteLine("Start Loading");
                Console.WriteLine($"User Login = '{UserLogin}'");
                Console.WriteLine($"User Password = '{UserPass}'");


                var piSoc = LoadPinacleSoccers();
                var marSoc = LoadMarathonSoccers();
                var forks = GetSoccersForks();

            }
        }

        private static List<Fork> GetSoccersForks()
        {
            throw new NotImplementedException();
        }

        private static List<ResultForForks> LoadMarathonSoccers()
        {
            throw new NotImplementedException();
        }

        private static List<ResultForForks> LoadPinacleSoccers()
        {
            Console.WriteLine("Start Loading Soccer Events");

            var resList = pinnacle.GetAllPinacleEventsForRequestAsync(SportType.Soccer, UserLogin, UserPass).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {resList.Count} Soccers Events");

            return resList;
        }
    }
}
