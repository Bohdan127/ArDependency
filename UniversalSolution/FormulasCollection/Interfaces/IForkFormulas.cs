namespace FormulasCollection.Interfaces
{
    public interface IForkFormulas
    {
        bool CheckIsFork(double? coef1, double? coef2);

        double GetProfit(double rate, double? kof1, double? kof2);

    }
}