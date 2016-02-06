using DataParser.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataSaver.Interfaces
{
    public interface IDataSaver
    {
        DataTable MakeTable(List<IDataMatch> dataList);
    }
}
