using DataSaver.Models;
using DataSaver.RavenDB;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Globalization;
using ToolsPortable;

namespace ParseAPI
{
    public static class ext
    {
        public static double? ConvertToDoubleOrNull2(this object data)
        {
            if (Extentions.IsBlank(data != null ? data.ToString() : (string)null))
                return new double?();
            double result;
            if (double.TryParse(data != null ? data.ToString() : (string)null, NumberStyles.Any, (IFormatProvider)CultureInfo.CurrentCulture, out result))
                return new double?(result);
            return new double?();
        }
    }

    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            #region Marathon
            {
                var mainUrl = "https://www.marathonbet.com/su/"; //https://www.marathonbet.com/su/
                var mainId = "event_4305884"; //id="event_4305884"
                var dataEventNameCssClass = "data -event-name"; //data -event-name="Тяньцзинь Тэда - Чанчунь Ятай"
                var coefClass = "selection-link.normal"; //class="selection-link.normal"
                var footballUrl = "https://www.marathonbet.com/su/popular/Football/?menu=true";//https://www.marathonbet.com/su/popular/Football/?menu=true
                var tennisUrl = "https://www.marathonbet.com/su/popular/Tennis/?menu=true";//https://www.marathonbet.com/su/popular/Tennis/?menu=true
                var basketballUrl = "https://www.marathonbet.com/su/popular/Basketball/?menu=true";//https://www.marathonbet.com/su/popular/Basketball/?menu=true
                var hockeyUrl = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";//https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true
                /*
                 * ?????????????????????????
                 * Volleyball wasn't found in marathon 
                 * ?????????????????????????
                 */
                var volleyballUrl = "https://www.marathonbet.com/su/popular/Volleyball/?menu=true";//https://www.marathonbet.com/su/popular/Volleyball/?menu=true
            }
            #endregion

            #region Pinnacle
            {
                var mainUrl = "https://www.pinnacle.com/en/rtn"; //https://www.pinnacle.com/en/rtn
                var loginButtonClass = "button blue-bg login";//class="button blue-bg login"
                var loginFieldClass = "customerId";//class="customerId"
                var loginFieldName = "CustomerId";//name="CustomerId"
                var passFieldClass = "password";//class="password"
                var passFieldName = "Password";//name="Password"
                var buttonLoginClass = "loginSubmit";//class="loginSubmit"
                var buttonLoginValue = "Login";//value="Login"
                var mainId = "event_4305884"; //id="event_4305884"
                var dataEventNameCssClass = "data -event-name"; //data -event-name="Тяньцзинь Тэда - Чанчунь Ятай"
                var coefClass = "selection-link.normal"; //class="selection-link.normal"
            }
            #endregion
            //var start = "7/10/2016 5:30:00 AM"; //10/07/2016 16:30

            //var splitOne = start.Split(' ');

            //var splitDate = splitOne[0].Split('/');

            //var splitTime = splitOne[1].Split(':');

            //var time = splitOne[2][0] == 'A' ? splitOne[1] : $"{Convert.ToInt16(splitTime[0]) + 12}:{splitTime[1]}";

            //var res = $"{splitDate[1]}/{splitDate[0]}/{splitDate[2]} {time}";


            //var res = "-144.0".ConvertToDoubleOrNull2();

            //// Create a new object, representing the German culture.
            //CultureInfo culture = new CultureInfo("en-US");

            //// The following line provides localization for the application's user interface.
            //Thread.CurrentThread.CurrentUICulture = culture;

            //// The following line provides localization for data formats.
            //Thread.CurrentThread.CurrentCulture = culture;

            //// Set this culture as the default culture for all threads in this application.
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;

            //res = "-144.0".ConvertToDoubleOrNull2();

            //_store = new DocumentStore
            //{
            //    Url = "http://localhost:8765",
            //    DefaultDatabase = "Parser"
            //};
            //_store.Initialize();
            //_store.DatabaseCommands.DisableAllCaching();
            //MarathonParser p = new MarathonParser();
            //p.ShowForks(p.InitiAsync(SportType.Basketball).Result);
            //Console.ReadKey();
            // GetAllForkRows().
            //ForEach(fBase => _store.DatabaseCommands.UpdateAttachmentMetadata(fBase.Id, Etag.Empty, RavenJObject.FromObject(fBase)));
        }




        private static List<ForkRow> GetAllForkRows()
        {
            var resList = new List<ForkRow>();
            for (int i = 1; i <= Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPageCount(PageSize); i++)
            {
                resList.AddRange(Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPage(i, PageSize));
            }
            return resList;
        }

        public const int PageSize = 128;
        private static IDocumentSession _session;
        internal static DocumentStore _store;

        internal static IDocumentSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = _store.OpenSession();
                }

                if (_session.Advanced.NumberOfRequests ==
                    _session.Advanced.MaxNumberOfRequestsPerSession)
                {
                    _session.Dispose();
                    _session = _store.OpenSession();
                }
                return _session;
            }
        }
    }
}