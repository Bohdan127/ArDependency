using FormulasCollection.Realizations;
using System.Collections.Generic;

namespace FormulasCollection.Interfaces
{
    public interface IForkFormulas
    {
        bool CheckIsFork(double? coef1, double? coef2);
        double getRate(double rate, double kof1, double kof2);
        List<Fork> GetAllForks(List<ResultForForks> events, int defaultRate);
    }
}
