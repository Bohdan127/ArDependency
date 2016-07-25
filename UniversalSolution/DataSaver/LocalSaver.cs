using DataParser.Enums;
using DataSaver.Models;
using DataSaver.RavenDB;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using Raven.Abstractions.Data;
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
        internal DocumentStore _store;

        private IDocumentSession _session;

        public const int PageSize = 128;

        public LocalSaver()
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8765",
                DefaultDatabase = "Parser",
                Conventions = { ShouldCacheRequest = url => false }
            };
            _store.Initialize();
            //_store.DatabaseCommands.DisableAllCaching();
        }

        internal IDocumentSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = _store.OpenSession();
                }

                // ReSharper disable once InvertIf
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
            var forks = GetAllForkRows().Where(fBase => fBase.Sport == sportType.ToString() &&
                                            fBase.Type == ForkType.Merged).ToArray();

            //removing all rows with status Merged from forkList
            foreach (var fBase in forks)
            {
                var fork = forkList.FirstOrDefault(fNew =>
                    fNew.Event == fBase.Event
                    && fNew.MatchDateTime == fBase.MatchDateTime);
                if (fork != null)
                    forkList.Remove(fork);
                else
                {//change all Forks in DB from Merged to Saved
                    var forkDocument = Session.Load<ForkRow>(fBase.Id);
                    forkDocument.Type = ForkType.Saved;
                }
            }
            Session.SaveChanges();
        }

        private void ClearForks(SportType sportType)
        {
            GetAllForkRows().Where(f => f.Sport == sportType.ToString() &&
                f.Type == ForkType.Current).
                ForEach(f => _store.DatabaseCommands.Delete(f.Id, null));
        }

        private IEnumerable<ForkRow> GetAllForkRows()
        {
            var jsonList = new List<JsonDocument>();
            using (_store.DatabaseCommands.DisableAllCaching())
            {
                for (var i = 0;
                    i <=
                    Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPageCount(PageSize);
                    i += PageSize)
                {
                    jsonList.AddRange(_store.DatabaseCommands.GetDocuments(i, PageSize));
                }
            }
            jsonList.RemoveAll(json => !json.Key.Contains("ForkRows/"));
            var resList = jsonList.Select(MapJsonDocumentToForkRow);
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

        public virtual void UpdateFork(ForkRow forkRow)
        {
            var forkDocument = Session.Load<ForkRow>(forkRow.Id);
            forkDocument.Type = forkRow.Type;
            Session.SaveChanges();
        }

        public virtual void DeleteFork(ForkRow forkRow)
        {
            var forkDocument = Session.Load<ForkRow>(forkRow.Id);
            Session.Delete(forkDocument);
            Session.SaveChanges();
        }

        public virtual void UpdateFork(Fork fork)
        {
            UpdateFork(MapForkToForkRow(fork));
        }

        public virtual void DeleteFork(Fork fork)
        {
            DeleteFork(MapForkToForkRow(fork));
        }

        protected virtual ForkRow MapJsonDocumentToForkRow(JsonDocument json)
        {
            var result = new ForkRow
            {
                Id = json.Key,
                Event = json.DataAsJson.Value<string>("Event"),
                Sport = json.DataAsJson.Value<string>("Sport"),
                TypeFirst = json.DataAsJson.Value<string>("TypeFirst"),
                CoefFirst = json.DataAsJson.Value<string>("CoefFirst"),
                TypeSecond = json.DataAsJson.Value<string>("TypeSecond"),
                CoefSecond = json.DataAsJson.Value<string>("CoefSecond"),
                MatchDateTime = json.DataAsJson.Value<string>("MatchDateTime"),
                BookmakerFirst = json.DataAsJson.Value<string>("BookmakerFirst"),
                BookmakerSecond = json.DataAsJson.Value<string>("BookmakerSecond"),
                Type = (ForkType)Enum.Parse(typeof(ForkType), json.DataAsJson.Value<string>("Type"))
            };
            return result;
        }
        protected virtual User MapJsonDocumentToUsers(JsonDocument json)
        {
            var result = new User
            {
                Id = json.Key,
                Login = json.DataAsJson.Value<string>("Login"),
                Password = json.DataAsJson.Value<string>("Password")
            };
            return result;
        }

        protected virtual ForkRow MapForkToForkRow(Fork fork)
        {
            var result = new ForkRow
            {
                Id = fork.Id,
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
            return result;
        }


        protected virtual Fork MapForkRowToFork(ForkRow forkRow)
        {
            var result = new Fork
            {
                Id = forkRow.Id,
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
            return result;
        }

        public virtual void UpdateUser(User user)
        {
            var userDocument = Session.Load<User>(user.Id);
            userDocument.Login = user.Login;
            userDocument.Password = user.Password;
            Session.SaveChanges();
        }

        public virtual bool AddUserToDB(string login, string password)
        {
            var user = new User { Login = login, Password = password };

            try
            {
                Session.Store(user);
            }
            catch (Exception)
            {
                //ignored
            }
            Session.SaveChanges();
            return true;
        }
        public virtual User FindUser()
        {
            var jsonList = new List<JsonDocument>();
            using (_store.DatabaseCommands.DisableAllCaching())
            {
                for (var i = 0;
                    i <=
                    Session.Query<ForkRow>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).GetPageCount(PageSize);
                    i += PageSize)
                {
                    jsonList.AddRange(_store.DatabaseCommands.GetDocuments(i, PageSize));
                }
            }
            jsonList.RemoveAll(json => !json.Key.Contains("users/"));
            var resList = jsonList.Select(MapJsonDocumentToUsers);
            return resList.FirstOrDefault();
        }
    }
}
