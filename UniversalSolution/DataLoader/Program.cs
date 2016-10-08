using Common.Modules.AntiCaptha;
using DataParser.DefaultRealization;
using DataParser.Enums;
using DataSaver;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
using SiteAccess.Access;
using SiteAccess.Enums;
using SiteAccess.Model.Bets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ToolsPortable;

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
        private static TwoOutComeCalculatorFormulas _calculatorFormulas;

        private static double _defRate;

        private static void Main()
        {
            // Create a new object, representing the German culture.
            CultureInfo culture = new CultureInfo("en-US");

            // The following line provides localization for the application's user interface.
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            _calculatorFormulas = new TwoOutComeCalculatorFormulas();
            _pinnacle = new PinnacleSportsDataParser();
            _marathon = new MarathonParser();
            _localSaver = new LocalSaver();
            _forkFormulas = new TwoOutComeForkFormulas();
            GetUserData();
            StartLoadDictionary();


            MarathonParser.WriteToDocument(MarathonParser.winik);
            MarathonParser.winik = new List<ResultForForks>();
        }

        private static void GetUserData()
        {
            var user = _localSaver.FindUser();
            if (user != null)
            {
                UserLogin = user.Login;
                UserPass = user.Password;
            }
            else
            {
                Console.WriteLine("Please enter user login");
                UserLogin = Console.ReadLine();

                Console.WriteLine("Please enter user password");
                UserPass = Console.ReadLine();
                _localSaver.AddUserToDB(UserLogin, UserPass);
            }
            while (_defRate <= 0.0)
            {
                Console.WriteLine("Please enter user default rate (Пожалуйста, введите ставку по умолчанию)");
                try
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    _defRate = Console.ReadLine().ConvertToDoubleOrNull().Value;
                }
                catch
                {
                    // ignored
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

                //always loading all sports
                var sportsToLoading = new[] { SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis };

                foreach (var sportType in sportsToLoading)
                {
                    var pinSport = LoadPinacleDictionary(sportType);
                    var marSport = LoadMarathon(sportType);
                    var forks = GetForksDictionary(sportType, pinSport, marSport);

                    SaveNewForks(forks, sportType);
                    PlaceAllBet(forks, sportType);
                }

            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void PlaceAllBet(List<Fork> forks, SportType sportType)
        {
            Console.WriteLine($"Starting place bets for new {forks.Count} forks. Sport type {sportType}");

            PlaceMarathon(forks);
            PlacePinnacle(forks);

            Console.WriteLine($"Starting place bets for new {forks.Count} forks. Sport type {sportType}");
        }

        private static void PlaceMarathon(List<Fork> forks)
        {
            var marath = new MarathonAccess(new AntiGate("<your code>"));
            marath.Login("2127864", "Artemgus88");

            foreach (var fork in forks.Where(f => f.Profit > 1.0).OrderBy(f => Convert.ToDateTime(f.MatchDateTime)))
            {
                var bet = new MarathonBet
                {
                    Id = "4050540@Match_Result.1",
                    Name = "Bolton Wanderers vs Blackpool",
                    Stake = 1.0,
                    AddData = "{\"sn\":\"Bolton Wanderers To Win\",\"mn\":\"Match Result\",\"ewc\":\"1/1 1\",\"cid\":10381455169,\"prt\":\"CP\",\"ewf\":\"1.0\",\"epr\":\"1.6600000000000001\",\"prices\":{\"0\":\"33/50\",\"1\":\"1.66\",\"2\":\"-152\",\"3\":\"0.66\",\"4\":\"0.66\",\"5\":\"-1.52\"}"
                };
                marath.MakeBet(bet);
            }
        }

        private static void PlacePinnacle(List<Fork> forks)
        {
            var pinn = new PinncaleAccess();
            pinn.Login("VB794327", "artem89@");

            //https://www.pinnacle.com/ru/api/manual#pbet
            foreach (var fork in forks.Where(f => f.Profit > 1.0).OrderBy(f => Convert.ToDateTime(f.MatchDateTime)))
            {
                var recomendedRates = _calculatorFormulas.GetRecommendedRates(_defRate,
                    fork.CoefFirst.ConvertToDoubleOrNull(),
                    fork.CoefSecond.ConvertToDoubleOrNull());
                var bet = new PinnacleBet
                {
                    AcceptBetterLine = true,
                    BetType = BetType.MONEYLINE,
                    Eventid = Convert.ToInt64(fork.PinnacleEventId),
                    Guid = Guid.NewGuid().ToString(),
                    OddsFormat = OddsFormat.DECIMAL,
                    LineId = Convert.ToInt64(fork.LineId),
                    /*
                     * This represents the period of the match. For example, for soccer we have:
                     * 0 - Game
                     * 1 - 1st Half
                     * 2 - 2nd Half
                     */
                    PeriodNumber = 0,
                    WinRiskRate = WinRiskType.WIN,
                    // ReSharper disable once PossibleInvalidOperationException
                    Stake = recomendedRates.Item2.ConvertToDecimalOrNull().Value,
                    SportId = (int)(SportType)Enum.Parse(typeof(SportType), fork.Sport, false)
                };
                pinn.MakeBet(bet);
            }
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

            var newList = _pinnacle.GetAllPinacleEventsDictionary(sportType, UserLogin, UserPass);

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {newList.Count} {sportType} Events from Pinnacle");

            return newList;
        }
    }
}