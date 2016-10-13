using Common.Modules.AntiCaptha;
using DataParser.DefaultRealization;
using DataParser.Enums;
using DataSaver;
using DataSaver.Models;
using FormulasCollection.Enums;
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
        private static User _currentUser { get; set; }

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

        }

        private static void GetUserData()
        {
            _currentUser = _localSaver.FindUser();
            if (_currentUser == null)
            {
                _currentUser = new User();
                Console.WriteLine("Please enter Pinnacle user login");//for test "VB794327", "artem89@"
                _currentUser.LoginPinnacle = Console.ReadLine();

                Console.WriteLine("Please enter Pinnacle user password");
                _currentUser.PasswordPinnacle = Console.ReadLine();

                Console.WriteLine("Please enter Marathon user login");//for test "2127864", "Artemgus88"
                _currentUser.LoginMarathon = Console.ReadLine();

                Console.WriteLine("Please enter Marathon user password");
                _currentUser.PasswordMarathon = Console.ReadLine();

                Console.WriteLine("Please enter Anti Gate Code");
                _currentUser.AntiGateCode = Console.ReadLine();

                _localSaver.AddUserToDb(_currentUser);
            }
            //while (_defRate <= 0.0)
            //{
            //    Console.WriteLine("Please enter user default rate (Пожалуйста, введите ставку по умолчанию)");
            //    try
            //    {
            //        // ReSharper disable once PossibleInvalidOperationException
            //        _defRate = Console.ReadLine().ConvertToDoubleOrNull().Value;
            //    }
            //    catch
            //    {
            //        // ignored
            //    }
            //}
        }

        private static void StartLoadDictionary()
        {
            while (true)
            {

                Console.WriteLine("Start Loading with Dictionary");
                Console.WriteLine($"Pinnacle Login = '{_currentUser.LoginPinnacle}'");
                Console.WriteLine($"Pinnacle Password = '{_currentUser.PasswordPinnacle}'");
                Console.WriteLine($"Marathon Login = '{_currentUser.LoginMarathon}'");
                Console.WriteLine($"Marathon Password = '{_currentUser.PasswordMarathon}'");
                Console.WriteLine($"Anti Gate Code = '{_currentUser.AntiGateCode}'");

                //always loading all sports
                var sportsToLoading = new[] { SportType.Basketball, SportType.Hockey, SportType.Soccer, SportType.Tennis };

                foreach (var sportType in sportsToLoading)
                {
                    var pinSport = LoadPinacleDictionary(sportType);
                    var marSport = LoadMarathon(sportType);
                    var forks = GetForksDictionary(sportType, pinSport, marSport);

                    ClearForks(forks, sportType);
                    if (forks.Count > 0)
                    {
                        //PlaceAllBet(forks, sportType);
                        SaveNewForks(forks, sportType);
                    }
                }

            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void ClearForks(List<Fork> forks,
            SportType sportType)
        {
            Console.WriteLine($"Starting remove old forks. Sport type {sportType}");
            if (forks == null || forks.Count == 0) return;

            var rowsForDelete = _localSaver.MoveForks(forks, sportType);
            _localSaver.ClearForks(rowsForDelete);
            Console.WriteLine($"{forks.Count} forks left for place and save. Sport type {sportType}");
        }

        private static void PlaceAllBet(List<Fork> forks, SportType sportType)
        {
            Console.WriteLine($"Starting place bets for new {forks.Count} forks. Sport type {sportType}");

            PlaceMarathon(forks);
            PlacePinnacle(forks);

            Console.WriteLine($"End placing bet. Was placed {forks.Count(f => f.Type == ForkType.Saved)} forks. Sport type {sportType}");
        }

        private static void PlaceMarathon(List<Fork> forks)
        {
            var marath = new MarathonAccess(new AntiGate(_currentUser.AntiGateCode));
            marath.Login(_currentUser.LoginMarathon, _currentUser.PasswordMarathon);

            foreach (var fork in forks.Where(f => f.Profit > 1.0).OrderBy(f => Convert.ToDateTime(f.MatchDateTime)))
            {
                var recomendedRates = _calculatorFormulas.GetRecommendedRates(_defRate,
                  fork.CoefFirst.ConvertToDoubleOrNull(),
                  fork.CoefSecond.ConvertToDoubleOrNull());
                var bet = new MarathonBet
                {
                    Id = fork.MarathonEventId,
                    Name = fork.Event,
                    // ReSharper disable once PossibleInvalidOperationException
                    Stake = recomendedRates.Item1.ConvertToDoubleOrNull().Value,
                    AddData = $"{{\"sn\":\"{fork.sn}\"," +
                              $"\"mn\":\"{fork.mn}\"," +
                              $"\"ewc\":\"{fork.ewc}\"," +
                              $"\"cid\":{fork.cid}," +
                              $"\"prt\":\"{fork.prt}\"," +
                              $"\"ewf\":\"{fork.ewf}\"," +
                              $"\"epr\":\"{fork.epr}\"," +
                              "\"prices\"" +
                                    $":{{\"0\":\"{fork.prices[0]}\"," +
                                        $"\"1\":\"{fork.prices[1]}\"," +
                                        $"\"2\":\"{fork.prices[2]}\"," +
                                        $"\"3\":\"{fork.prices[3]}\"," +
                                        $"\"4\":\"{fork.prices[4]}\"," + $"\"5\":\"{fork.prices[5]}\"}}}}"
                };
                marath.MakeBet(bet);
                if (fork.Type != ForkType.Saved)
                {
                    fork.Type = ForkType.Saved;
                }
            }
        }

        private static void PlacePinnacle(List<Fork> forks)
        {
            var pinn = new PinncaleAccess();
            pinn.Login(_currentUser.LoginPinnacle, _currentUser.PasswordPinnacle);

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
                if (fork.Type != ForkType.Saved)
                {
                    fork.Type = ForkType.Saved;
                }
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

            _localSaver.InsertForks(forks);

            Console.WriteLine($"End Saving.");
        }

        private static List<ResultForForks> LoadMarathon(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Marathon");

            var resList = _marathon.Initi(sportType);
            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {resList.Count} {sportType} Events from Marathon");

            return resList;
        }

        private static Dictionary<string, ResultForForksDictionary> LoadPinacleDictionary(SportType sportType)
        {
            Console.WriteLine($"Start Loading {sportType} Events from Pinnacle");

            var newList = _pinnacle.GetAllPinacleEventsDictionary(sportType,
                _currentUser.LoginPinnacle,
                _currentUser.PasswordPinnacle);

            Console.WriteLine("Loading finished");
            Console.WriteLine($"Was founded {newList.Count} {sportType} Events from Pinnacle");

            return newList;
        }
    }
}