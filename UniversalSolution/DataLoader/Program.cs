using DataParser.DefaultRealization;
using DataParser.Enums;
using DataParser.MY;
using DataSaver;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace DataLoader
{
    class Program
    {
        public static string UserLogin { get; set; }
        public static string UserPass { get; set; }

        private static PinnacleSportsDataParser pinnacle;
        private static ParsePinnacle marathon;
        private static TwoOutComeForkFormulas forkFormulas;
        private static LocalSaver localSaver;

        static void Main(string[] args)
        {
            Console.WriteLine("DataLoader Start");
            pinnacle = new PinnacleSportsDataParser(new ConverterFormulas());
            marathon = new ParsePinnacle();
            localSaver = new LocalSaver();
            forkFormulas = new TwoOutComeForkFormulas();
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

                List<ResultForForks> pinSoc;
                List<ResultForForks> marSoc;
                List<Fork> forks = new List<Fork>();

                //always loading all sports
                var sportsToLoading = new[]
                {SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis, SportType.Volleyball,};

                foreach (var sportType in sportsToLoading)
                {
                    pinSoc = LoadPinacle(sportType);
                    marSoc = LoadMarathon(sportType);
                    forks = GetForks(sportType, pinSoc, marSoc);    
                                 
                    SaveNewForks(forks, sportType);

                    pinSoc.Clear();
                    marSoc.Clear();
                    forks.Clear();
                }
            }
        }

        private static void SaveNewForks(List<Fork> forks, SportType sportType)
        {
            Console.WriteLine($"Start Saving Forks for {sportType} sport type");

            localSaver.ClearAndInsertForks(forks, sportType);

            Console.WriteLine($"End Saving. {forks.Count} forks was saved");
        }

        private static List<Fork> GetForks(SportType sportType, List<ResultForForks> pinnacle, List<ResultForForks> marathon)
        {
            Console.WriteLine($"Start Calculate Forks for {sportType} sport type");

            var resList = forkFormulas.GetAllForks(pinnacle, marathon);

            Console.WriteLine("Calculate finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Forks");

            return resList;
        }

        private static List<ResultForForks> LoadMarathon(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Marathon");

            var resList = marathon.InitiAsync(sportType).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Events from Marathon");

            return resList;
        }

        private static List<ResultForForks> LoadPinacle(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Pinnacle");

            var resList = pinnacle.GetAllPinacleEventsForRequestAsync(sportType, UserLogin, UserPass).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Events from Pinnacle");

            return resList;
        }
    }
}
