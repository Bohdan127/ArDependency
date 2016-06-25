using DataParser.DefaultRealization;
using DataParser.Enums;
using DataParser.MY;
using DataSaver;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;

namespace DataLoader
{
    internal class Program
    {
        public static string UserLogin { get; set; }
        public static string UserPass { get; set; }

        private static PinnacleSportsDataParser pinnacle;
        private static MarathonParser marathon;
        private static TwoOutComeForkFormulas forkFormulas;
        private static LocalSaver localSaver;

        private static void Main(string[] args)
        {
            Console.WriteLine("DataLoader Start");
            pinnacle = new PinnacleSportsDataParser(new ConverterFormulas());
            marathon = new MarathonParser();
            localSaver = new LocalSaver();
            forkFormulas = new TwoOutComeForkFormulas();
            GetUserData();

            Console.WriteLine("Please enter program type.");
            Console.WriteLine("1 for List type(slower but more stable)");
            Console.WriteLine("2 for Dictionary type(really fast but not so stable for now)");
            try
            {
                if (Console.ReadLine().Trim() == "1")
                    StartLoad();
                else
                    StartLoadDictionary();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                Console.WriteLine("Start Loading with List");
                Console.WriteLine($"User Login = '{UserLogin}'");
                Console.WriteLine($"User Password = '{UserPass}'");

                List<ResultForForks> pinSport;
                List<ResultForForks> marSport;
                List<Fork> forks = new List<Fork>();

                //always loading all sports
                var sportsToLoading = new[]
                {SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis, SportType.Volleyball,};

                foreach (var sportType in sportsToLoading)
                {
                    pinSport = LoadPinacle(sportType);
                    marSport = LoadMarathon(sportType);
                    forks = GetForks(sportType, pinSport, marSport);

                    SaveNewForks(forks, sportType);

                    pinSport.Clear();
                    marSport.Clear();
                    forks.Clear();
                }
            }
        }

        private static void StartLoadDictionary()
        {
            while (true)
            {
                Console.WriteLine("Start Loading with Dictionary");
                Console.WriteLine($"User Login = '{UserLogin}'");
                Console.WriteLine($"User Password = '{UserPass}'");

                Dictionary<string, ResultForForksDictionary> pinSport;
                List<ResultForForks> marSport;
                List<Fork> forks = new List<Fork>();

                //always loading all sports
                var sportsToLoading = new[]
                {SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis, SportType.Volleyball};

                foreach (var sportType in sportsToLoading)
                {
                    pinSport = LoadPinacleDictionary(sportType);
                    marSport = LoadMarathon(sportType);
                    forks = GetForksDictionary(sportType, pinSport, marSport);

                    SaveNewForks(forks, sportType);
                }
            }
        }

        private static List<Fork> GetForksDictionary(SportType sportType, Dictionary<string, ResultForForksDictionary> pinSport, List<ResultForForks> marSport)
        {
            Console.WriteLine($"Start Calculate Forks for {sportType} sport type");

            var resList = forkFormulas.GetAllForksDictionary(pinSport, marSport);

            Console.WriteLine("Calculate finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Forks");

            return resList;
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

            var resList = forkFormulas.GetAllForks(marathon, pinnacle);

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

        private static Dictionary<string, ResultForForksDictionary> LoadPinacleDictionary(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Pinnacle");

            var newList = pinnacle.GetAllPinacleEventsForRequestDictionaryAsync(sportType, UserLogin, UserPass).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {newList.Count} {sportType} Events from Pinnacle");

            return newList;
        }
    }
}