using DataSaver.DataBase;
using DataSaver.DataBase.ForksDataSetTableAdapters;
using DataSaver.Models;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaver
{
    public class LocalSaver
    {
        internal ForkTableAdapter TableAdapter;
        internal ForksDataSet DataSet;

        public LocalSaver()
        {
            InitializeComponent();
            TableAdapter.Fill(DataSet.Fork);
        }

        private void InitializeComponent()
        {
            TableAdapter = new ForkTableAdapter();
            DataSet = new ForksDataSet();
            ((System.ComponentModel.ISupportInitialize)DataSet).BeginInit();

            // 
            // instancesTableAdapter
            // 
            TableAdapter.ClearBeforeFill = true;
            // 
            // DataSet
            // 
            DataSet.DataSetName = "ForksDataSet";
            DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            ((System.ComponentModel.ISupportInitialize)DataSet).EndInit();
        }

        public virtual async Task InsertForksAsync(List<Fork> forkList)
        {
            await Task.Delay(1).ConfigureAwait(false);
            InsertForks(forkList);
        }

        public virtual void InsertForks(List<Fork> forkList)
        {
            Contract.Requires<ArgumentNullException>(forkList != null);

            foreach (var fork in forkList)
            {
                try
                {
                    DataSet.Fork.Rows.Add(MapForkToForkRow(fork));
                }
                catch (Exception)
                {
                    //ignored
                }
            }
            TableAdapter.Update(DataSet);
        }

        public virtual void ClearForks()
        {
            DataSet.Fork.Rows.Clear();
            TableAdapter.Update(DataSet);
        }

        public virtual async Task ClearAndInsertForksAsync(List<Fork> forkList)
        {
            await Task.Delay(1).ConfigureAwait(false);
            ClearAndInsertForks(forkList);
        }


        public virtual void ClearAndInsertForks(List<Fork> forkList)
        {
            Contract.Requires<ArgumentNullException>(forkList != null);

            ClearForks();
            InsertForks(forkList);
        }

        public virtual async Task<List<Fork>> GetForksAsync(Filter searchCriteria)
        {
            await Task.Delay(1).ConfigureAwait(false);
            return GetForks(searchCriteria);
        }

        public virtual List<Fork> GetForks(Filter searchCriteria)
        {
            var dataTable = TableAdapter.GetData();

            var query = GetQueryFromFilter(searchCriteria);
            var resRows = dataTable.Select(query).Cast<ForksDataSet.ForkRow>();

            return resRows.Select(forkRow => MapForkRowToFork(forkRow)).ToList();
        }

        private string GetQueryFromFilter(Filter searchCriteria)
        {
            //todo this function will be rewrite to 3-4 small query creation functions
            var query = new StringBuilder();
            var isAnySportType = false;
            //var isAnyBookmaker = false;
            var isAnyPreviousValue = false;

            if (searchCriteria.Basketball)
            {
                isAnySportType = true;
                query.Append($"( Sport = 'Basketball' ");
            }

            if (searchCriteria.Football)
            {
                if (!isAnySportType)
                {
                    isAnySportType = true;
                    query.Append(" ( ");
                }
                else
                    query.Append(" or ");

                query.Append($" Sport = 'Football' ");

            }

            if (searchCriteria.Hockey)
            {
                if (!isAnySportType)
                {
                    isAnySportType = true;
                    query.Append(" ( ");
                }
                else
                    query.Append(" or ");

                query.Append($" Sport = 'Hockey' ");
            }

            if (searchCriteria.Tennis)
            {
                if (!isAnySportType)
                {
                    isAnySportType = true;
                    query.Append(" ( ");
                }
                else
                    query.Append(" or ");

                query.Append($" Sport = 'Tennis' ");
            }

            if (searchCriteria.Volleyball)
            {
                if (!isAnySportType)
                {
                    isAnySportType = true;
                    query.Append(" ( ");
                }
                else
                    query.Append(" or ");

                query.Append($" Sport = 'Volleyball' ");
            }

            if (isAnySportType)
            {
                isAnyPreviousValue = true;
                query.Append(" ) ");
            }

            //todo looks like unneeded but please not remove it for now
            //if (searchCriteria.PinnacleSports)
            //{
            //    isAnyBookmaker = true;
            //    if (isAnyPreviousValue)
            //        query.Append(" and ");
            //    query.Append($"( BookmakerFirst = 'PinnacleSports' or BookmakerSecond = 'PinnacleSports' ");
            //}

            //if (searchCriteria.MarathonBet)
            //{
            //    if (!isAnyBookmaker)
            //    {
            //        isAnyBookmaker = true;
            //        if (isAnyPreviousValue)
            //            query.Append(" and ");
            //        query.Append(" ( ");
            //    }
            //    else
            //        query.Append(" or ");

            //    query.Append($" BookmakerFirst = 'MarathonBet' or BookmakerSecond = 'MarathonBet' ");

            //}
            //if (isAnyBookmaker)
            //{
            //    isAnyPreviousValue = true;
            //    query.Append(" ) ");
            //}

            if (searchCriteria.Min != null)
            {
                if (isAnyPreviousValue)
                {
                    isAnyPreviousValue = true;
                    query.Append(" and ");
                }
                query.Append($" Profit >= {searchCriteria.Min.Value} ");
            }

            if (searchCriteria.Max != null)
            {
                if (isAnyPreviousValue)
                {
                    isAnyPreviousValue = true;
                    query.Append(" and ");
                }
                query.Append($" Profit <= {searchCriteria.Max.Value} ");
            }

            return !isAnyPreviousValue ? string.Empty : query.ToString();
        }

        protected virtual ForksDataSet.ForkRow MapForkToForkRow(Fork fork)
        {
            var resRow = DataSet.Fork.NewForkRow();

            resRow.BookmakerFirst = fork.BookmakerFirst;
            resRow.BookmakerSecond = fork.BookmakerSecond;
            resRow.CoefFirst = fork.CoefFirst;
            resRow.CoefSecond = fork.CoefSecond;
            resRow.Event = fork.Event;
            resRow.MatchDateTime = fork.MatchDateTime;
            resRow.Profit = fork.Profit;
            resRow.Sport = fork.Sport;
            resRow.TypeFirst = fork.TypeFirst;
            resRow.TypeSecond = fork.TypeSecond;

            return resRow;
        }

        protected virtual Fork MapForkRowToFork(ForksDataSet.ForkRow forkRow)
        {
            var fork = new Fork();

            fork.BookmakerFirst = forkRow.BookmakerFirst;
            fork.BookmakerSecond = forkRow.BookmakerSecond;
            fork.CoefFirst = forkRow.CoefFirst;
            fork.CoefSecond = forkRow.CoefSecond;
            fork.Event = forkRow.Event;
            fork.MatchDateTime = forkRow.MatchDateTime;
            fork.Profit = forkRow.Profit;
            fork.Sport = forkRow.Sport;
            fork.TypeFirst = forkRow.TypeFirst;
            fork.TypeSecond = forkRow.TypeSecond;

            return fork;
        }


    }
}
