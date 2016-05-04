using FormulasCollection.Interfaces;
using System;

namespace FormulasCollection.Realizations
{
    public class ConverterFormulas : IConverterFormulas
    {
        public double IncorrectAmericanOdds => -127;

        public double ConvertAmericanToDecimal(int? american) => american == null && !american.HasValue && american.Value != 0
                ? IncorrectAmericanOdds
                : (american.Value > 0
                    ? PositiveConvertationFormula(american.Value)
                    : NegativeConvertationFormula(american.Value));


        public double PositiveConvertationFormula(int american) => american <= 0
            ? IncorrectAmericanOdds
            : (american / 100) + 1;

        public double NegativeConvertationFormula(int american) => american >= 0
            ? IncorrectAmericanOdds
            : (100 / Math.Abs(american)) + 1;
    }
}
