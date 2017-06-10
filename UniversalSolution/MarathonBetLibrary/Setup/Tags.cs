using MarathonBetLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonBetLibrary.Setup
{
    public class Tags
    {
        private static readonly string contentRegix = "(.*?)";
        private static readonly string contentRegix_DoubleQuotes = "=\"" + contentRegix + "\""; // ="...." 
        private static readonly string contentRegix_DoubleQuotes2 = "\"" + contentRegix + "\""; //  "...."
        private static readonly string contentRegix_OneQuotes = "='" + contentRegix + "'";      //  '....'

        private static readonly string eventID = "data-event-treeId";
        private static readonly string nameEvent = "data-event-name";
        private static readonly string data_sel = "data-sel";
        private static readonly string selection_key = "data-selection-key";
        private static readonly string category_id = "data-category-treeid";
        private static readonly string live = "data-live";

        #region[AUTO PLAY]
        public static string SN { get { return contextRegexAutoPlay("sn"); } }
        public static string MN { get { return contextRegexAutoPlay("mn"); } }
        public static string EWC { get { return contextRegexAutoPlay("ewc"); } }
        public static string CID { get { return contextRegexAutoPlay("cid"); } }
        public static string PRT { get { return contextRegexAutoPlay("prt"); } }
        public static string EWF { get { return contextRegexAutoPlay("ewf"); } }
        public static string EPR { get { return contextRegexAutoPlay("epr"); } }
        public static string PRICES { get { return contextRegexAutoPlay("prices"); } }

        public static string contextRegexAutoPlay(string value)
        {
            if (value.Equals("cid")) return "\"" + value + "\":" + contentRegix + ",";
            else if (value.Equals("prices")) { return "\"" + value + "\":{" + contentRegix + "}"; }
            return "\"" + value + "\":" + contentRegix_DoubleQuotes2;
        }
        #endregion

        public static readonly string EventID = eventID + contentRegix_DoubleQuotes;
        public static readonly string NameEvent = nameEvent + contentRegix_DoubleQuotes;
        public static readonly string DataSel = data_sel + contentRegix_OneQuotes;
        public static readonly string SelectionKey = selection_key + contentRegix_DoubleQuotes;
        public static readonly string CategoryId = "(" + category_id + contentRegix_DoubleQuotes + ")";
        public static readonly string IsLive = live + contentRegix_DoubleQuotes;
        public static string GetNumTeamRegex { get { return "member-number\">" + contentRegix + "<"; } }
        public static string DateTime { get { return "([0-9]{2}.*)((янв|фев|мар|апр|мая|июн|июл|авг|сен|окт|ноя|дек).*)([0-9]{2}.*)(:)([0-9]{2}.*)"; } }
        public static string Time { get { return "([0-9]{2}.*)(:)([0-9]{2}.*)"; } }
        public static string Date { get { return "class=\"date \""; } }


    }
    public class FilterTypeCoef
    {
        private bool winsResult; //результат 1; 2; X; 1X; 2X
        private bool winWithFora;
        private bool winWithTotal;
        private bool summaryCountGame; // счет матча

        private List<string> limitResult;
        private List<string> limitFora;
        private List<string> limitTotal;
        private List<string> limitForAll;
        private bool Filter(SportType sportType, string value)
        {
            bool result = false;
            return result;
        }

        private bool WinsResult { get { return this.winsResult; } set { this.winsResult = value; } }
        private bool WinWithFora { get { return this.winWithFora; } set { this.winWithFora = value; } }
        private bool WinWithTotal { get { return this.winWithTotal; } set { this.winWithTotal = value; } }
        private bool SummaryCountGame { get { return this.summaryCountGame; } set { this.summaryCountGame = value; } }
    }
}
