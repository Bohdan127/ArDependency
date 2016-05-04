namespace FormulasCollection.Interfaces
{
    public interface IForkFormulas
    {
        bool CheckIsFork(double coef1, double coef2);
        //List<Fork> GetAllForks(List<ResultForForks> events);
        //Task<List<Fork>> GetAllForksAsync(List<ResultForForks> events);
    }
}
