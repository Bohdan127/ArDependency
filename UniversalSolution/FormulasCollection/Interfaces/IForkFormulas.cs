using FormulasCollection.Realizations;
using System.Collections.Generic;

namespace FormulasCollection.Interfaces
{
    public interface IForkFormulas
    {
        bool CheckIsFork(double? coef1, double? coef2);
        bool checkForType(string type1, string type2);
        bool isTheSame(string marafon, string pinacle);
        Dictionary<string, Fork> GetAllForks(Dictionary<string, ResultForForks> marafon, Dictionary<string, ResultForForks> pinacle);
        
    }
}
