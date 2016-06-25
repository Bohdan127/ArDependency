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

        private static PinnacleSportsDataParser _pinnacle;
        private static MarathonParser _marathon;
        private static TwoOutComeForkFormulas _forkFormulas;
        private static LocalSaver _localSaver;

        private static void Main()
        {
            Console.WriteLine("DataLoader Start");
            _pinnacle = new PinnacleSportsDataParser(new ConverterFormulas());
            _marathon = new MarathonParser();
            _localSaver = new LocalSaver();
            _forkFormulas = new TwoOutComeForkFormulas();
            GetUserData();
            StartLoadDictionary();
        }

        private static void GetUserData()
        {
            Console.WriteLine("Please enter user login");
            UserLogin = Console.ReadLine();

            Console.WriteLine("Please enter user password");
            UserPass = Console.ReadLine();
        }

        private static void StartLoadDictionary()
        {
            while (true)
            {
                Console.WriteLine("Start Loading with Dictionary");
                Console.WriteLine($"User Login = '{UserLogin}'");
                Console.WriteLine($"User Password = '{UserPass}'");

                //always loading all sports
                var sportsToLoading = new[]
                {SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis, SportType.Volleyball};

                foreach (var sportType in sportsToLoading)
                {
                    var pinSport = LoadPinacleDictionary(sportType);
                    var marSport = LoadMarathon(sportType);
                    var forks = GetForksDictionary(sportType, pinSport, marSport);

                    SaveNewForks(forks, sportType);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static List<Fork> GetForksDictionary(SportType sportType, Dictionary<string, ResultForForksDictionary> pinSport, List<ResultForForks> marSport)
        {
            Console.WriteLine($"Start Calculate Forks for {sportType} sport type");

            var resList = _forkFormulas.GetAllForksDictionary(pinSport, marSport);

            Console.WriteLine("Calculate finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Forks");

            return resList;
        }

        private static void SaveNewForks(List<Fork> forks, SportType sportType)
        {
            Console.WriteLine($"Start Saving Forks for {sportType} sport type");

            _localSaver.ClearAndInsertForks(forks, sportType);

            Console.WriteLine($"End Saving. {forks.Count} forks was saved");
        }

        private static List<ResultForForks> LoadMarathon(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Marathon");

            var resList = _marathon.InitiAsync(sportType).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Events from Marathon");

            return resList;
        }

        private static Dictionary<string, ResultForForksDictionary> LoadPinacleDictionary(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Pinnacle");

            var newList = _pinnacle.GetAllPinacleEventsForRequestDictionaryAsync(sportType, UserLogin, UserPass).Result;

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {newList.Count} {sportType} Events from Pinnacle");

            return newList;
        }
    }
}