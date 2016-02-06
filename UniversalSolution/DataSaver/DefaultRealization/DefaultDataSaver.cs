using DataParser.Interfaces;
using DataSaver.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataSaver.DefaultRealization
{
    public class DefaultDataSaver : IDataSaver
    {
        public const string EventColumn = "Events";

        public DataTable MakeTable(List<IDataMatch> dataList)
        {
            var resTable = new DataTable();
            if (dataList != null && dataList.Count > 0)
            {
                PrepareDataColumn(ref resTable, dataList[0]);
                InsertData(ref resTable, dataList);
            }

            return resTable;
        }

        private void InsertData(ref DataTable resTable, List<IDataMatch> dataList)
        {
            foreach (var matchItem in dataList)
            {
                var row = resTable.NewRow();

                foreach (DataColumn col in resTable.Columns)
                {
                    var prop = matchItem.GetType().GetProperty(col.Caption);

                    if (prop != null)
                    {
                        row[col] = prop.GetValue(this, null);
                    }
                    else
                    {
                        //prop = matchItem.Events.GetType().GetProperty(col.Caption);

                        //if (prop != null)
                        //{
                        //    row[col] = prop.GetValue(this, null);
                        //}
                    }
                }

                resTable.Rows.Add(row);
            }
        }

        public void PrepareDataColumn(ref DataTable resTable, IDataMatch matchData)
        {
            foreach (PropertyInfo propertyInfo in matchData.GetType().GetProperties())
            {
                if (propertyInfo.Name != EventColumn)
                    resTable.Columns.Add(propertyInfo.Name);
            }
            //foreach (PropertyInfo propertyInfo in matchData.Events.GetType().GetProperties())
            //{
            //    resTable.Columns.Add(propertyInfo.Name);
            //}
        }
    }
}