using FormulasCollection.Interfaces;
using System;

namespace FormulasCollection.Realizations
{
    public class ConverterFormulas : IConverterFormulas
    {
        public double IncorrectAmericanOdds => -127;

        public double ConvertAmericanToDecimal(double? american) => american == null
                ? IncorrectAmericanOdds
                : (american.Value > 0
                    ? PositiveConvertationFormula(american.Value)
                    : NegativeConvertationFormula(american.Value));


        public double PositiveConvertationFormula(double american)
        {
            var r = american <= 0
                  ? IncorrectAmericanOdds
                  : (american / 100) + 1;
            return r;
        }

        public double NegativeConvertationFormula(double american)
        {
            var r = american >= 0 ? IncorrectAmericanOdds
              : (100 / Math.Abs(american)) + 1;
            return r;
        }
    }
}
