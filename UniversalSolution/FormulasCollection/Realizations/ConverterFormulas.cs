using FormulasCollection.Interfaces;
using System;

namespace FormulasCollection.Realizations
{
    public class ConverterFormulas : IConverterFormulas
    {
        public decimal IncorrectAmericanOdds => -127;

        public decimal ConvertAmericanToDecimal(int? american) => american == null && !american.HasValue && american.Value != 0
                ? IncorrectAmericanOdds
                : (american.Value > 0
                    ? PositiveConvertationFormula(american.Value)
                    : NegativeConvertationFormula(american.Value));


        public decimal PositiveConvertationFormula(int american) => american <= 0
            ? IncorrectAmericanOdds
            : (american / 100) + 1;

        public decimal NegativeConvertationFormula(int american) => american >= 0
            ? IncorrectAmericanOdds
            : (100 / Math.Abs(american)) + 1;
    }
}
