using FormulasCollection.Realizations;
using System.Collections.Generic;

namespace FormulasCollection.Interfaces
{
    public interface IForkFormulas
    {
        bool CheckIsFork(double? coef1, double? coef2);

        double GetProfit(double rate, double? kof1, double? kof2);

        bool checkForType(string type1, string type2);

        bool isTheSame(string marafon, string pinacle);

        List<Fork> GetAllForks(List<ResultForForks> marafon, List<ResultForForks> pinacle);
    }
}