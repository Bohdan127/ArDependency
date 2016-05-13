using DataParser.Enums;
using DataSaver.Models;
using DataSaver.RavenDB;
using FormulasCollection.Realizations;
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

        public virtual async Task InsertForksAsync(List<Fork> forkList)
        {
            await Task.Delay(1).ConfigureAwait(false);
            InsertForks(forkList);
        }

        public virtual void InsertForks(List<Fork> forkList)
        {
            if (forkList == null)
                throw new ArgumentNullException();

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

        public virtual async Task ClearAndInsertForksAsync(List<Fork> forkList, SportType sportType)
        {
            await Task.Delay(1).ConfigureAwait(false);
            ClearAndInsertForks(forkList, sportType);
        }


        public virtual void ClearAndInsertForks(List<Fork> forkList, SportType sportType)
        {
            if (forkList == null)
                throw new ArgumentNullException();

            ClearForks(sportType);
            InsertForks(forkList);
        }

        private void ClearForks(SportType sportType)
        {
            var delList = GetAllForkRows().Where(f => f.Sport == sportType.ToString());
            foreach (var forkRow in delList)
            {
                _store.DatabaseCommands.Delete(forkRow.Id, null);
            }
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

        public virtual async Task<List<Fork>> GetForksAsync(Filter searchCriteria)
        {
            await Task.Delay(1).ConfigureAwait(false);
            return GetForks(searchCriteria);
        }

        public virtual List<Fork> GetForks(Filter searchCriteria)
        {
            return GetAllForkRows().Where(f =>
                (searchCriteria.Basketball && f.Sport == SportType.Basketball.ToString()) ||
                (searchCriteria.Football && f.Sport == SportType.Soccer.ToString()) ||
                (searchCriteria.Hockey && f.Sport == SportType.Hockey.ToString()) ||
                (searchCriteria.Volleyball && f.Sport == SportType.Volleyball.ToString()) ||
                (searchCriteria.Tennis && f.Sport == SportType.Tennis.ToString()))
                .Select(MapForkRowToFork).ToList();
        }

        protected virtual ForkRow MapForkToForkRow(Fork fork)
        {
            ForkRow resRow = new ForkRow();

            resRow.BookmakerFirst = fork.BookmakerFirst;
            resRow.BookmakerSecond = fork.BookmakerSecond;
            resRow.CoefFirst = fork.CoefFirst;
            resRow.CoefSecond = fork.CoefSecond;
            resRow.Event = fork.Event;
            resRow.MatchDateTime = fork.MatchDateTime;
            resRow.Sport = fork.Sport;
            resRow.TypeFirst = fork.TypeFirst;
            resRow.TypeSecond = fork.TypeSecond;

            return resRow;
        }

        protected virtual Fork MapForkRowToFork(ForkRow forkRow)
        {
            var fork = new Fork();

            fork.BookmakerFirst = forkRow.BookmakerFirst;
            fork.BookmakerSecond = forkRow.BookmakerSecond;
            fork.CoefFirst = forkRow.CoefFirst;
            fork.CoefSecond = forkRow.CoefSecond;
            fork.Event = forkRow.Event;
            fork.MatchDateTime = forkRow.MatchDateTime;
            fork.Sport = forkRow.Sport;
            fork.TypeFirst = forkRow.TypeFirst;
            fork.TypeSecond = forkRow.TypeSecond;

            return fork;
        }
    }
}



