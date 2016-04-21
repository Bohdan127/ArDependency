namespace FormulasCollection
{
    public interface ICalculatorFormulas
    {
        object CalculateIncome(double coef, double rate);
        object CalculateSummaryRate(params double[] rates);
        object CalculateSummaryIncome(params string[] incomes);

        string Formatter { get; set; }
    }
}
