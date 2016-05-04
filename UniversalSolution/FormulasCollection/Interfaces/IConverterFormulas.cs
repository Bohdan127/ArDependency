namespace FormulasCollection.Interfaces
{
    public interface IConverterFormulas
    {
        /// <summary>
        /// Value returns for all null-able or incorrect Americans odds
        /// </summary>
        decimal IncorrectAmericanOdds { get; }

        /// <summary>
        /// Converting from all American odds to Decimal odds 
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's null</returns>
        decimal ConvertAmericanToDecimal(int? american);

        /// <summary>
        /// Convert  Only Positive odds from American to Decimal
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's negative</returns>
        decimal PositiveConvertationFormula(int american);

        /// <summary>
        /// Convert Only Negative odds from American to Decimal
        /// </summary>
        /// <param name="american">American odds</param>
        /// <returns>Decimal odds or IncorrectAmericanOdds if it's positive</returns>
        decimal NegativeConvertationFormula(int american);
    }
}
