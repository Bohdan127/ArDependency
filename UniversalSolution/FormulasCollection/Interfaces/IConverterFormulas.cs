namespace FormulasCollection.Interfaces
{
    public interface IConverterFormulas
    {
        /// <summary>
        /// Value returns for all null-able or incorrect Americans odds
        /// </summary>
        double IncorrectAmericanOdds { get; }

        /// <summary>
        /// Converting from all American odds to Decimal odds
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's null</returns>
        double ConvertAmericanToDecimal(double? american);

        /// <summary>
        /// Convert  Only Positive odds from American to Decimal
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's negative</returns>
        double PositiveConvertationFormula(double american);

        /// <summary>
        /// Convert Only Negative odds from American to Decimal
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's positive</returns>
        double NegativeConvertationFormula(double american);
    }
}