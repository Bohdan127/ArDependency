using DataParser.Enums;
using FormulasCollection.Models;
using System.Collections.Generic;
using ToolsPortable;

namespace FormulasCollection.Helpers
{
    public class SportsConverterTypes
    {
        public static List<string> TypeParseAll(string typeEvent, SportType st)
        {
            string typeEventTrim = typeEvent.Trim();

            string typeCoefForPinnacle = string.Empty;
            string number = string.Empty;
            bool isTotal = false;
            bool isFora = false;
            if (typeEvent.Contains("TU"))
            {
                typeEventTrim = "TU";
                isTotal = true;
            }
            else if (typeEvent.Contains("TO"))
            {
                typeEventTrim = "TO";
                isTotal = true;
            }
            bool totalNum = typeEvent.Contains("(") && typeEvent.Contains(")");
            if (totalNum)
            {
                number = typeEvent.Split('(', ')')[1];
            }

            if (typeEvent.Contains("F1"))
            {
                typeEventTrim = "F1";
                isFora = true;
            }
            else if (typeEvent.Contains("F2"))
            {
                typeEventTrim = "F2";
                isFora = true;
            }
            if (isFora)
            {
                if (number[0] == '+')
                {
                    number = "-" + number.Substring(1);
                }
                else if (number[0] == '-')
                {
                    number = "+" + number.Substring(1);
                }
            }

            if (SportTypes.TypeCoefsSoccer.ContainsKey(typeEventTrim) && st == SportType.Soccer)
            {
                if (isTotal || isFora)
                {
                    return CheckAsiatType(SportTypes.TypeCoefsSoccer[typeEventTrim] + "(" + number + ")");
                }
                else
                    return CheckAsiatType(SportTypes.TypeCoefsSoccer[typeEventTrim]);
            }
            if (SportTypes.TypeCoefsTennis.ContainsKey(typeEventTrim) && st == SportType.Tennis)
            {
                if (isTotal || isFora)
                {
                    return CheckAsiatType(SportTypes.TypeCoefsTennis[typeEventTrim] + "(" + number + ")");
                }
                else
                    return CheckAsiatType(SportTypes.TypeCoefsTennis[typeEventTrim]);
            }
            if (SportTypes.TypeCoefsBasketBall.ContainsKey(typeEventTrim) && st == SportType.Basketball)
            {
                if (isTotal || isFora)
                {
                    return CheckAsiatType(SportTypes.TypeCoefsBasketBall[typeEventTrim] + "(" + number + ")");
                }
                else
                    return CheckAsiatType(SportTypes.TypeCoefsBasketBall[typeEventTrim]);
            }
            if (SportTypes.TypeCoefsHockey.ContainsKey(typeEventTrim) && st == SportType.Hockey)
            {
                if (isTotal || isFora)
                {
                    return CheckAsiatType(SportTypes.TypeCoefsHockey[typeEventTrim] + "(" + number + ")");
                }
                else
                    return CheckAsiatType(SportTypes.TypeCoefsHockey[typeEventTrim]);
            }
            if (SportTypes.TypeCoefsVolleyBall.ContainsKey(typeEventTrim) && st == SportType.Volleyball)
            {
                if (isTotal || isFora)
                {
                    return CheckAsiatType(SportTypes.TypeCoefsVolleyBall[typeEventTrim] + "(" + number + ")");
                }
                else
                    return CheckAsiatType(SportTypes.TypeCoefsVolleyBall[typeEventTrim]);
            }
            return null;
        }
        public static List<string> CheckAsiatType(string _type)
        {
            // перевірити азіатскі типи на баскетбол 
            const double delta = 0.25;

            List<string> result = new List<string>();
            result.Add(_type);
            string znak = string.Empty;

            if (!(_type.Contains("(") && _type.Contains(")")))
                return result;
            string name = _type.Split('(', ')')[0];
            string number = _type.Split('(', ')')[1];
            if (!string.IsNullOrEmpty(number) && number.Contains("+") || number.Contains("-"))
            {
                znak = number[0].ToString();
                number = number.Substring(1);
            }
            if (!string.IsNullOrEmpty(number))
            {
                double num = number.ConvertToDoubleOrNull() ?? 0;
                double num1 = num - delta;
                double num2 = num + delta;

                string val1 = name.Trim() + "(" + (num1 == 0 ? "" : znak) + num1.ToString() + ")";
                string val2 = name.Trim() + "(" + (num1 == 0 ? "" : znak) + num2.ToString() + ")";
                result.Add(val1);
                result.Add(val2);
            }
            return result;
        }
    }
}
