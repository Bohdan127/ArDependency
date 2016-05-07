using System.Collections.Generic;
namespace FormulasCollection.Interfaces
{
    public interface ICalculatorFormulas
    {
        /// <summary>
        /// Calculate Income with default logic for 
        /// </summary>
        /// <param name="coef"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        string CalculateIncome(double coef, double rate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        string CalculateSummaryRate(params double[] rates);

        /// <summary>
        /// Calculate Summary Income using Formatter
        /// </summary>
        /// <param name="incomes">all incomes which need to be summarize</param>
        /// <returns>Summary Income like a string</returns>
        string CalculateSummaryIncome(params string[] incomes);

        List<string> getRates(double rate, double kof1, double kof2);

        string CalculateRate(double rateMain, double rateCurrent, double kof);
        /// <summary>
        /// Thing which used to join income values for total
        /// </summary>
        string Formatter { get; set; }
    }
}
