using DataSaver.Models;
using DataSaver.RavenDB;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;

namespace ParseAPI
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8765",
                DefaultDatabase = "Parser"
            };
            _store.Initialize();
            _store.DatabaseCommands.DisableAllCaching();
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