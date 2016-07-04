using DataSaver.Models;
using DataSaver.RavenDB;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
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

            var res = "-144.0".ConvertToDoubleOrNull2();

            // Create a new object, representing the German culture.
            CultureInfo culture = new CultureInfo("en-US");

            // The following line provides localization for the application's user interface.
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            res = "-144.0".ConvertToDoubleOrNull2();

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