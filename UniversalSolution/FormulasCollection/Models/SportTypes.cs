using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasCollection.Models
{
    public static class SportTypes
    {
        private static Dictionary<string,List<Tuple<string, string>>> _listSoccer;
        private static Dictionary<string,List<Tuple<string, string>>> _listBasketBall;
        private static Dictionary<string,List<Tuple<string, string>>> _listVolleyBall;
        private static Dictionary<string,List<Tuple<string, string>>> _listHockey;
        private static Dictionary<string,List<Tuple<string, string>>> _listTennis;

        public static Dictionary<string, List<Tuple<string, string>>> TypeListSoccer => _listSoccer;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListBasketBall => _listBasketBall;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListVolleyBall => _listVolleyBall;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListHockey => _listHockey;
        public static Dictionary<string, List<Tuple<string, string>>> TypeListTennis => _listTennis;

        static SportTypes()
        {
            _listSoccer = new Dictionary<string, List<Tuple<string, string>>>
            {
                #region Wins
                //new Tuple<string, string>("12","X"),
                //new Tuple<string, string>("X2","1"),
                //new Tuple<string, string>("1X","2"),
                //new Tuple<string, string>("X","12"),
                //new Tuple<string, string>("1","X2"),
                //new Tuple<string, string>("2","1X"),
                //#endregion Wins

                //#region Odds
                //new Tuple<string, string>("F1(0)","F2(0)"),
                //new Tuple<string, string>("F2(0)","F1(0)"),
                //new Tuple<string, string>("F1(-0.25)","F2(0.25)"),
                //new Tuple<string, string>("F1(0.25)","F2(-0.25)"),
                //new Tuple<string, string>("F2(0.25)","F1(-0.25)"),
                //new Tuple<string, string>("F2(-0.25)","F1(0.25)"),
                //#endregion Odds
            };

        }
    }
}
