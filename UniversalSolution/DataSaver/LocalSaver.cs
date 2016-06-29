using DataParser.Enums;
using DataSaver.Models;
using DataSaver.RavenDB;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataSaver
{
    public class LocalSaver
    {
        //internal ForkTableAdapter TableAdapter;
        //internal ForksDataSet DataSet;
        internal DocumentStore _store;

        private IDocumentSession _session;

        public const int PageSize = 128;

        public LocalSaver()
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8765",
                DefaultDatabase = "Parser"
            };
            _store.Initialize();
            _store.DatabaseCommands.DisableAllCaching();
        }

        internal IDocumentSession Session
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

        public virtual void InsertForks(List<Fork> forkList)
        {
            if (forkList == null) return;

            foreach (var fork in forkList)
            {
                try
                {
                    Session.Store(MapForkToForkRow(fork));
                }
                catch (Exception)
                {
                    //ignored
                }
            }
            Session.SaveChanges();
        }

        public virtual void ClearAndInsertForks(List<Fork> forkList, SportType sportType)
        {
            if (forkList == null || forkList.Count == 0) return;

            MoveForks(forkList, sportType);
            ClearForks(sportType);
            InsertForks(forkList);
        }

        private void MoveForks(List<Fork> forkList, SportType sportType)
        {
            //GetAllForkRows().Where(fBase => fBase.Sport == sportType.ToString() &&
            //                                forkList.Any(fNew => fNew.Event == fBase.Event) &&
            //                                forkList.Any(fNew => fNew.MatchDateTime == fBase.MatchDateTime)).
            //ForEach(fBase => _store.DatabaseCommands.UpdateAttachmentMetadata(fBase.Id, Etag.Empty, RavenJObject.FromObject(fBase)));
        }

        private void ClearForks(SportType sportType)
        {
            GetAllForkRows().Where(f => f.Sport == sportType.ToString() &&
                f.Type == ForkType.Current).
                ForEach(f => _store.DatabaseCommands.Delete(f.Id, null));
        }

        private List<ForkRow> GetAllForkRows()
        {
            var resList = new List<ForkRow>();
            for (int i = 1; i <= Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPageCount(PageSize); i++)
            {
                resList.AddRange(Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPage(i, PageSize));
            }
            return resList;
        }

        public virtual async Task<List<Fork>> GetForksAsync(Filter searchCriteria, ForkType forkType)
        {
            return await Task.Run(() => GetForks(searchCriteria, forkType));
        }

        public virtual List<Fork> GetForks(Filter searchCriteria, ForkType forkType)
        {
            return GetAllForkRows().Where(f => f.Type == forkType &&
                ((searchCriteria.Basketball && f.Sport == SportType.Basketball.ToString()) ||
                (searchCriteria.Football && f.Sport == SportType.Soccer.ToString()) ||
                (searchCriteria.Hockey && f.Sport == SportType.Hockey.ToString()) ||
                (searchCriteria.Volleyball && f.Sport == SportType.Volleyball.ToString()) ||
                (searchCriteria.Tennis && f.Sport == SportType.Tennis.ToString())))
                .Select(MapForkRowToFork).ToList();
        }

        protected virtual ForkRow MapForkToForkRow(Fork fork) => new ForkRow
        {
            BookmakerFirst = fork.BookmakerFirst,
            BookmakerSecond = fork.BookmakerSecond,
            CoefFirst = fork.CoefFirst,
            CoefSecond = fork.CoefSecond,
            Event = fork.Event,
            MatchDateTime = fork.MatchDateTime,
            Sport = fork.Sport,
            TypeFirst = fork.TypeFirst,
            TypeSecond = fork.TypeSecond,
            Type = fork.Type
        };


        protected virtual Fork MapForkRowToFork(ForkRow forkRow) => new Fork
        {
            BookmakerFirst = forkRow.BookmakerFirst,
            BookmakerSecond = forkRow.BookmakerSecond,
            CoefFirst = forkRow.CoefFirst,
            CoefSecond = forkRow.CoefSecond,
            Event = forkRow.Event,
            MatchDateTime = forkRow.MatchDateTime,
            Sport = forkRow.Sport,
            TypeFirst = forkRow.TypeFirst,
            TypeSecond = forkRow.TypeSecond,
            Type = forkRow.Type
        };
    }
}
