using DataSaver;
using DevExpress.XtraBars.Alerter;
using ExchangeAPI.Data;
using ExchangeAPI.Providers.Pinnacle;
using NLog;
using ToolsPortable;

namespace WebCrawler.SeleniumCrawler
{
    public static class PinnacleCrawler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly AlertControl AlertControl;
        private static bool _busy = false;

        private const string MainUrl = "https://www.pinnacle.com/en/rtn"; //https://www.pinnacle.com/en/rtn
        private const string LoginButtonClass = "blue-bg";//class="button blue-bg login"
        private const string LoginFieldClass = "customerId";//class="customerId"
        private const string PassFieldClass = "password";//name="CustomerId"
        private const string ButtonLoginClass = "loginSubmit";
        public static LocalSaver LocalSaver { get; private set; }


        static PinnacleCrawler()
        {
            LocalSaver = new LocalSaver();
            AlertControl = new AlertControl();
        }

        public static void SearchAndOpenEvent(string lineId)
        {
            _logger.Trace($"start SearchAndOpenEvent. lineId = {lineId}");

            if (lineId.IsBlank())
            {
                _logger.Warn("lineId is Blank");
                return;
            }

            var pinProvider = new PinnacleProvider();

            pinProvider.Login("login",
                "password");
            var marketData = new MarketData("id",
                pinProvider,
                "locate");
            var order = new Order();//todo here we have a lot of parameters
            pinProvider.PlaceBet(marketData, order);

            _logger.Trace("End SearchAndOpenEvent.");
        }
    }
}