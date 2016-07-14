using DataParser.Enums;
using FormulasCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Helpers
{
    public class SportsConverterTypes
    {
        public static string TypeParseAll(string value, SportType st)
        {
                if (value.Trim()[0].Equals('F'))
                {
                    string val = value.Split('(', ')')[1].ToString();
                    return value.Trim()[1].Equals('1')
                    ? "F2(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val.Substring(1)) + ")"
                    : "F1(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val.Substring(1)) + ")";
                }
                if (value.Trim()[0].Equals('T'))
                {
                    string val = value.Split('(', ')')[1].ToString();
                    return value.Trim()[1].Equals('U')
                    ? "TO(" + val + ")"
                    : "TU(" + val + ")";
                }
                if (SportTypes.TypeCoefsSoccer.ContainsKey(value) && st == SportType.Soccer)
                {
                    return SportTypes.TypeCoefsSoccer[value];
                }
                if (SportTypes.TypeCoefsTennis.ContainsKey(value) && st == SportType.Tennis)
                {
                    return SportTypes.TypeCoefsTennis[value];
                }
                if (SportTypes.TypeCoefsBasketBall.ContainsKey(value) && st == SportType.Basketball)
                {
                    return SportTypes.TypeCoefsBasketBall[value];
                }
                if (SportTypes.TypeCoefsHockey.ContainsKey(value) && st == SportType.Hockey)
                {
                    return SportTypes.TypeCoefsHockey[value];
                }
                if (SportTypes.TypeCoefsVolleyBall.ContainsKey(value) && st == SportType.Volleyball)
                {
                    return SportTypes.TypeCoefsVolleyBall[value];
                }
            return null;
        }
    }
}
