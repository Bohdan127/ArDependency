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
        public static string TypeParseAll(string typeEvent, SportType st)
        {
            string typeEventTrim = typeEvent.Trim();
                if (typeEventTrim[0].Equals('F'))
                {
                string val = null;
                try
                {
                    val = typeEventTrim.Split('(', ')')[1].ToString();
                }
                catch(Exception ex)
                {
                    val = null;
                }
                if (val == null) return null;
                    return typeEventTrim[1].Equals('1')
                    ? "F2(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val.Substring(1)) + ")"
                    : "F1(" + (val[0].Equals('-') ? val.Substring(1) : "-" + val.Substring(1)) + ")";
                }
                if (typeEventTrim[0].Equals('T'))
                {
                string val = null;
                try
                {
                    val = typeEventTrim.Split('(', ')')[1].ToString();
                }
                catch(Exception ex)
                {
                    val = null;
                }
                if (val == null) return null;
                    return typeEventTrim[1].Equals('U')
                    ? "TO(" + val + ")"
                    : "TU(" + val + ")";
                }
                if (SportTypes.TypeCoefsSoccer.ContainsKey(typeEventTrim) && st == SportType.Soccer)
                {
                    return SportTypes.TypeCoefsSoccer[typeEventTrim];
                }
                if (SportTypes.TypeCoefsTennis.ContainsKey(typeEventTrim) && st == SportType.Tennis)
                {
                    return SportTypes.TypeCoefsTennis[typeEventTrim];
                }
                if (SportTypes.TypeCoefsBasketBall.ContainsKey(typeEventTrim) && st == SportType.Basketball)
                {
                    return SportTypes.TypeCoefsBasketBall[typeEventTrim];
                }
                if (SportTypes.TypeCoefsHockey.ContainsKey(typeEventTrim) && st == SportType.Hockey)
                {
                    return SportTypes.TypeCoefsHockey[typeEventTrim];
                }
                if (SportTypes.TypeCoefsVolleyBall.ContainsKey(typeEventTrim) && st == SportType.Volleyball)
                {
                    return SportTypes.TypeCoefsVolleyBall[typeEventTrim];
                }
            return null;
        }
    }
}
