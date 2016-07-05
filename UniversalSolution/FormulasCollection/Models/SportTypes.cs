using DataParser.Enums;
using System;
using System.Collections.Generic;

namespace FormulasCollection.Models
{
    public static class SportTypes
    {
        private static List<Tuple<string, string>> _listSoccer;
        private static List<Tuple<string, string>> _listBasketBall;
        private static List<Tuple<string, string>> _listVolleyBall;
        private static List<Tuple<string, string>> _listHockey;
        private static List<Tuple<string, string>> _listTennis;
        private static Dictionary<string, List<Tuple<string, string>>> _listAll;
        private static Dictionary<string, string> Coefs;

        public static List<Tuple<string, string>> TypeListSoccer => _listSoccer;
        public static List<Tuple<string, string>> TypeListBasketBall => _listBasketBall;
        public static List<Tuple<string, string>> TypeListVolleyBall => _listVolleyBall;
        public static List<Tuple<string, string>> TypeListHockey => _listHockey;
        public static List<Tuple<string, string>> TypeListTennis => _listTennis;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListAll => _listAll;
        public static Dictionary<string, string> TypeCoefs => Coefs;
        static SportTypes()
        {
            initCoefs();
            initListSoccer();
            initListTennis();
        }
        private static void initCoefs()
        {
            Coefs = new Dictionary<string, string>
            {
                { "12", "X"},
                { "1X", "2"},
                { "X2", "1"},
                { "1", "X2"},
                { "2", "1X"},
                { "X", "12"}
            };
        }
        private static void initListSoccer()
        {
            _listSoccer = new List<Tuple<string, string>>
            {
                #region Wins
                { new Tuple<string,string>("12", "X" ) },
                { new Tuple<string,string>("X2", "1" ) },
                { new Tuple<string,string>("1X", "2" ) },
                { new Tuple<string,string>("X", "12" ) },
                { new Tuple<string,string>("1", "X2" ) },
                { new Tuple<string,string>("2", "1X" ) },
                #endregion Wins

                #region Odds
                { new Tuple<string,string>("F1(0)", "F2(0)" ) },
                { new Tuple<string,string>("F2(0)", "F1(0)" ) },

                { new Tuple<string,string>("F1(-0.25)", "F2(0.25)" ) },
                { new Tuple<string,string>("F1(0.25)", "F2(-0.25)" ) },
                { new Tuple<string,string>("F2(0.25)", "F1(-0.25)" ) },
                { new Tuple<string,string>("F2(-0.25)", "F1(0.25)" ) },

                
                { new Tuple<string,string>("F1(0.5)", "F2(-0.5)" ) },
                { new Tuple<string,string>("F1(-0.5)", "F2(0.5)" ) },
                { new Tuple<string,string>("F2(0.5)", "F1(-0.5)" ) },
                { new Tuple<string,string>("F2(-0.5)", "F1(0.5)" ) },

                
                { new Tuple<string,string>("F1(0.75)", "F2(-0.75)" ) },
                { new Tuple<string,string>("F1(-0.75)", "F2(0.75)" ) },
                { new Tuple<string,string>("F2(0.75)", "F1(-0.75)" ) },
                { new Tuple<string,string>("F2(-0.75)", "F1(0.75)" ) },

                
                { new Tuple<string,string>("F1(1)", "F2(-1)" ) },
                { new Tuple<string,string>("F1(-1)", "F2(1)" ) },
                { new Tuple<string,string>("F2(1)", "F1(-1)" ) },
                { new Tuple<string,string>("F2(-1)", "F1(1)" ) },

                
                { new Tuple<string,string>("F1(1.25)", "F2(-1.25)" ) },
                { new Tuple<string,string>("F1(-1.25)", "F2(1.25)" ) },
                { new Tuple<string,string>("F2(1.25)", "F1(-1.25)" ) },
                { new Tuple<string,string>("F2(-1.25)", "F1(1.25)" ) },

                
                { new Tuple<string,string>("F1(1.5)", "F2(-1.5)" ) },
                { new Tuple<string,string>("F1(-1.5)", "F2(1.5)" ) },
                { new Tuple<string,string>("F2(1.5)", "F1(-1.5)" ) },
                { new Tuple<string,string>("F2(-1.5)", "F1(1.5)" ) },

                
                { new Tuple<string,string>("F1(1.75)", "F2(-1.75)" ) },
                { new Tuple<string,string>("F1(-1.75)", "F2(1.75)" ) },
                { new Tuple<string,string>("F2(1.75)", "F1(-1.75)" ) },
                { new Tuple<string,string>("F2(-1.75)", "F1(1.75)" ) },

                
                { new Tuple<string,string>("F1(2)", "F2(-2)" ) },
                { new Tuple<string,string>("F1(-2)", "F2(2)" ) },
                { new Tuple<string,string>("F2(2)", "F1(-2)" ) },
                { new Tuple<string,string>("F2(-2)", "F1(2)" ) },

                
                { new Tuple<string,string>("F1(2.25)", "F2(-2.25)" ) },
                { new Tuple<string,string>("F1(-2.25)", "F2(2.25)" ) },
                { new Tuple<string,string>("F2(2.25)", "F1(-2.25)" ) },
                { new Tuple<string,string>("F2(-2.25)", "F1(2.25)" ) },

                
                { new Tuple<string,string>("F1(2.5)", "F2(-2.5)" ) },
                { new Tuple<string,string>("F1(-2.5)", "F2(2.5)" ) },
                { new Tuple<string,string>("F2(2.5)", "F1(-2.5)" ) },
                { new Tuple<string,string>("F2(-2.5)", "F1(2.5)" ) },

                
                { new Tuple<string,string>("F1(2.75)", "F2(-2.75)" ) },
                { new Tuple<string,string>("F1(-2.75)", "F2(2.75)" ) },
                { new Tuple<string,string>("F2(2.75)", "F1(-2.75)" ) },
                { new Tuple<string,string>("F2(-2.75)", "F1(2.75)" ) },

                
                { new Tuple<string,string>("F1(3)", "F2(-3)" ) },
                { new Tuple<string,string>("F1(-3)", "F2(3)" ) },
                { new Tuple<string,string>("F2(3)", "F1(-3)" ) },
                { new Tuple<string,string>("F2(-3)", "F1(3)" ) },

                
                { new Tuple<string,string>("F1(3.25)", "F2(-3.25)" ) },
                { new Tuple<string,string>("F1(-3.25)", "F2(3.25)" ) },
                { new Tuple<string,string>("F2(3.25)", "F1(-3.25)" ) },
                { new Tuple<string,string>("F2(-3.25)", "F1(3.25)" ) },

                
                { new Tuple<string,string>("F1(3.5)", "F2(-3.5)" ) },
                { new Tuple<string,string>("F1(-3.5)", "F2(3.5)" ) },
                { new Tuple<string,string>("F2(3.5)", "F1(-3.5)" ) },
                { new Tuple<string,string>("F2(-3.5)", "F1(3.5)" ) },

                
                { new Tuple<string,string>("F1(3.75)", "F2(-3.75)" ) },
                { new Tuple<string,string>("F1(-3.75)", "F2(3.75)" ) },
                { new Tuple<string,string>("F2(3.75)", "F1(-3.75)" ) },
                { new Tuple<string,string>("F2(-3.75)", "F1(3.75)" ) },
                #endregion Odds

                #region Totals
                
                { new Tuple<string,string>("TO(0)", "TU(0)" ) },
                { new Tuple<string,string>("TU(0)", "TO(0)" ) },

                
                { new Tuple<string,string>("TO(0.25)", "TU(-0.25)" ) },
                { new Tuple<string,string>("TO(-0.25)", "TU(0.25)" ) },
                { new Tuple<string,string>("TU(0.25)", "TO(-0.25)" ) },
                { new Tuple<string,string>("TU(-0.25)", "TO(0.25)" ) },

                
                { new Tuple<string,string>("TO(0.5)", "TU(-0.5)" ) },
                { new Tuple<string,string>("TO(-0.5)", "TU(0.5)" ) },
                { new Tuple<string,string>("TU(0.5)", "TO(-0.5)" ) },
                { new Tuple<string,string>("TU(-0.5)", "TO(0.5)" ) },

                
                { new Tuple<string,string>("TO(0.75)", "TU(-0.75)" ) },
                { new Tuple<string,string>("TO(-0.75)", "TU(0.75)" ) },
                { new Tuple<string,string>("TU(0.75)", "TO(-0.75)" ) },
                { new Tuple<string,string>("TU(-0.75)", "TO(0.75)" ) },

                
                { new Tuple<string,string>("TO(1)", "TU(-1)" ) },
                { new Tuple<string,string>("TO(-1)", "TU(1)" ) },
                { new Tuple<string,string>("TU(1)", "TO(-1)" ) },
                { new Tuple<string,string>("TU(-1)", "TO(1)" ) },

                
                { new Tuple<string,string>("TO(1.25)", "TU(-1.25)" ) },
                { new Tuple<string,string>("TO(-1.25)", "TU(1.25)" ) },
                { new Tuple<string,string>("TU(1.25)", "TO(-1.25)" ) },
                { new Tuple<string,string>("TU(-1.25)", "TO(1.25)" ) },

                
                { new Tuple<string,string>("TO(1.5)", "TU(-1.5)" ) },
                { new Tuple<string,string>("TO(-1.5)", "TU(1.5)" ) },
                { new Tuple<string,string>("TU(1.5)", "TO(-1.5)" ) },
                { new Tuple<string,string>("TU(-1.5)", "TO(1.5)" ) },

                
                { new Tuple<string,string>("TO(1.75)", "TU(-1.75)" ) },
                { new Tuple<string,string>("TO(-1.75)", "TU(1.75)" ) },
                { new Tuple<string,string>("TU(1.75)", "TO(-1.75)" ) },
                { new Tuple<string,string>("TU(-1.75)", "TO(1.75)" ) },

                
                { new Tuple<string,string>("TO(2)", "TU(-2)" ) },
                { new Tuple<string,string>("TO(-2)", "TU(2)" ) },
                { new Tuple<string,string>("TU(2)", "TO(-2)" ) },
                { new Tuple<string,string>("TU(-2)", "TO(2)" ) },

                
                { new Tuple<string,string>("TO(2.25)", "TU(-2.25)" ) },
                { new Tuple<string,string>("TO(-2.25)", "TU(2.25)" ) },
                { new Tuple<string,string>("TU(2.25)", "TO(-2.25)" ) },
                { new Tuple<string,string>("TU(-2.25)", "TO(2.25)" ) },

                
                { new Tuple<string,string>("TO(2.5)", "TU(-2.5)" ) },
                { new Tuple<string,string>("TO(-2.5)", "TU(2.5)" ) },
                { new Tuple<string,string>("TU(2.5)", "TO(-2.5)" ) },
                { new Tuple<string,string>("TU(-2.5)", "TO(2.5)" ) },

                
                { new Tuple<string,string>("TO(2.75)", "TU(-2.75)" ) },
                { new Tuple<string,string>("TO(-2.75)", "TU(2.75)" ) },
                { new Tuple<string,string>("TU(2.75)", "TO(-2.75)" ) },
                { new Tuple<string,string>("TU(-2.75)", "TO(2.75)" ) },

                
                { new Tuple<string,string>("TO(3)", "TU(-3)" ) },
                { new Tuple<string,string>("TO(-3)", "TU(3)" ) },
                { new Tuple<string,string>("TU(3)", "TO(-3)" ) },
                { new Tuple<string,string>("TU(-3)", "TO(3)" ) },

                
                { new Tuple<string,string>("TO(3.25)", "TU(-3.25)" ) },
                { new Tuple<string,string>("TO(-3.25)", "TU(3.25)" ) },
                { new Tuple<string,string>("TU(3.25)", "TO(-3.25)" ) },
                { new Tuple<string,string>("TU(-3.25)", "TO(3.25)" ) },

                
                { new Tuple<string,string>("TO(3.5)", "TU(-3.5)" ) },
                { new Tuple<string,string>("TO(-3.5)", "TU(3.5)" ) },
                { new Tuple<string,string>("TU(3.5)", "TO(-3.5)" ) },
                { new Tuple<string,string>("TU(-3.5)", "TO(3.5)" ) },

                
                { new Tuple<string,string>("TO(3.75)", "TU(-3.75)" ) },
                { new Tuple<string,string>("TO(-3.75)", "TU(3.75)" ) },
                { new Tuple<string,string>("TU(3.75)", "TO(-3.75)" ) },
                { new Tuple<string,string>("TU(-3.75)", "TO(3.75)" ) },
                #endregion Totals
            };
        }
        private static void initListTennis()
        {
            _listTennis = new List<Tuple<string, string>>
            {
                #region Wins
                { new Tuple<string,string>("1", "2" ) },
                { new Tuple<string,string>("2", "1" ) },
                #endregion Wins
            };
        }
    }
}
