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
        public static string TypeParseBasketball(string value)
        {
            if (value.Trim()[0].Equals('F'))
            {
                    string val = value.Split('(', ')')[1].ToString();
                    return value.Trim()[1].Equals('1') 
                    ? "F2(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val) + ")" 
                    : "F1(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val) + ")";
            }
            if (value.Trim()[0].Equals('T'))
            {
                string val = value.Split('(', ')')[1].ToString();
                return value.Trim()[1].Equals('U')
                ? "TO(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val) + ")"
                : "TU(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val) + ")";
            }
            if (SportTypes.TypeCoefs.ContainsKey(value))
            {
                return SportTypes.TypeCoefs[value];
            }
            return null;
        }
    }
}
