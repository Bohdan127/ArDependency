using DataParser.Enums;
using System;
using System.Collections.Generic;

namespace FormulasCollection.Models
{
    public static class SportTypes
    {
        private static Dictionary<string, List<Tuple<string, string>>> _listSoccer;
        private static Dictionary<string, List<Tuple<string, string>>> _listBasketBall;
        private static Dictionary<string, List<Tuple<string, string>>> _listVolleyBall;
        private static Dictionary<string, List<Tuple<string, string>>> _listHockey;
        private static Dictionary<string, List<Tuple<string, string>>> _listTennis;
        private static Dictionary<string, Dictionary<string, List<Tuple<string, string>>>> _listAll;

        public static Dictionary<string, List<Tuple<string, string>>> TypeListSoccer => _listSoccer;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListBasketBall => _listBasketBall;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListVolleyBall => _listVolleyBall;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListHockey => _listHockey;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListTennis => _listTennis;
        public static Dictionary<string, Dictionary<string, List<Tuple<string, string>>>> TypeListAll => _listAll;
        static SportTypes()
        {
            initListSoccer();
            initListTennis();
            _listAll["Soccer"] = _listSoccer;
            _listAll["Tennis"] = _listTennis;
        }
        private static void initListSoccer()
        {
            _listSoccer = new Dictionary<string, List<Tuple<string, string>>>
            {
                #region Wins
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("12", "X" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("X2", "1" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("1X", "2" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("X", "12" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("1", "X2" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("2", "1X" ) } },
                #endregion Wins

                #region Odds
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(0)", "F2(0)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(0)", "F1(0)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-0.25)", "F2(0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(0.25)", "F2(-0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(0.25)", "F1(-0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-0.25)", "F1(0.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(0.5)", "F2(-0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-0.5)", "F2(0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(0.5)", "F1(-0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-0.5)", "F1(0.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(0.75)", "F2(-0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-0.75)", "F2(0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(0.75)", "F1(-0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-0.75)", "F1(0.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(1)", "F2(-1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-1)", "F2(1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(1)", "F1(-1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-1)", "F1(1)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(1.25)", "F2(-1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-1.25)", "F2(1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(1.25)", "F1(-1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-1.25)", "F1(1.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(1.5)", "F2(-1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-1.5)", "F2(1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(1.5)", "F1(-1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-1.5)", "F1(1.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(1.75)", "F2(-1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-1.75)", "F2(1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(1.75)", "F1(-1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-1.75)", "F1(1.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(2)", "F2(-2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-2)", "F2(2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(2)", "F1(-2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-2)", "F1(2)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(2.25)", "F2(-2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-2.25)", "F2(2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(2.25)", "F1(-2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-2.25)", "F1(2.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(2.5)", "F2(-2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-2.5)", "F2(2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(2.5)", "F1(-2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-2.5)", "F1(2.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(2.75)", "F2(-2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-2.75)", "F2(2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(2.75)", "F1(-2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-2.75)", "F1(2.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(3)", "F2(-3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-3)", "F2(3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(3)", "F1(-3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-3)", "F1(3)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(3.25)", "F2(-3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-3.25)", "F2(3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(3.25)", "F1(-3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-3.25)", "F1(3.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(3.5)", "F2(-3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-3.5)", "F2(3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(3.5)", "F1(-3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-3.5)", "F1(3.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(3.75)", "F2(-3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F1(-3.75)", "F2(3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(3.75)", "F1(-3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("F2(-3.75)", "F1(3.75)" ) } },
                #endregion Odds

                #region Totals
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(0)", "TU(0)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(0)", "TO(0)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(0.25)", "TU(-0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-0.25)", "TU(0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(0.25)", "TO(-0.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-0.25)", "TO(0.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(0.5)", "TU(-0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-0.5)", "TU(0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(0.5)", "TO(-0.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-0.5)", "TO(0.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(0.75)", "TU(-0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-0.75)", "TU(0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(0.75)", "TO(-0.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-0.75)", "TO(0.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(1)", "TU(-1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-1)", "TU(1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(1)", "TO(-1)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-1)", "TO(1)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(1.25)", "TU(-1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-1.25)", "TU(1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(1.25)", "TO(-1.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-1.25)", "TO(1.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(1.5)", "TU(-1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-1.5)", "TU(1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(1.5)", "TO(-1.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-1.5)", "TO(1.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(1.75)", "TU(-1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-1.75)", "TU(1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(1.75)", "TO(-1.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-1.75)", "TO(1.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(2)", "TU(-2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-2)", "TU(2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(2)", "TO(-2)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-2)", "TO(2)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(2.25)", "TU(-2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-2.25)", "TU(2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(2.25)", "TO(-2.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-2.25)", "TO(2.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(2.5)", "TU(-2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-2.5)", "TU(2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(2.5)", "TO(-2.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-2.5)", "TO(2.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(2.75)", "TU(-2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-2.75)", "TU(2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(2.75)", "TO(-2.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-2.75)", "TO(2.75)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(3)", "TU(-3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-3)", "TU(3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(3)", "TO(-3)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-3)", "TO(3)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(3.25)", "TU(-3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-3.25)", "TU(3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(3.25)", "TO(-3.25)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-3.25)", "TO(3.25)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(3.5)", "TU(-3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-3.5)", "TU(3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(3.5)", "TO(-3.5)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-3.5)", "TO(3.5)" ) } },

                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(3.75)", "TU(-3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TO(-3.75)", "TU(3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(3.75)", "TO(-3.75)" ) } },
                { SportType.Soccer.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("TU(-3.75)", "TO(3.75)" ) } },
                #endregion Totals
            };
        }
        private static void initListTennis()
        {
            _listTennis = new Dictionary<string, List<Tuple<string, string>>>
            {
                #region Wins
                { SportType.Tennis.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("1", "2" ) } },
                { SportType.Tennis.ToString() , new List<Tuple<string,string>>()
                { new Tuple<string,string>("2", "1" ) } },
                #endregion Wins
            };
        }
    }
}
