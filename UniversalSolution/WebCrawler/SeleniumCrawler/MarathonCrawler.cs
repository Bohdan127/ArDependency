using DataParser.Enums;
using DevExpress.XtraBars.Alerter;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebCrawler.SeleniumCrawler
{
    public static class MarathonCrawler
    {
        private const string CoefClass = "height-column-with-price";
        private const string FootballUrl = "https://www.marathonbet.com/su/popular/Football/";
        private const string TennisUrl = "https://www.marathonbet.com/su/popular/Tennis/";
        private const string BasketballUrl = "https://www.marathonbet.com/su/popular/Basketball/";
        private const string HockeyUrl = "https://www.marathonbet.com/su/popular/Ice+Hockey/";
        private const string VolleyballUrl = "https://www.marathonbet.com/su/popular/Volleyball/";

        private static AlertControl _alertControl;

        static MarathonCrawler()
        {
            _alertControl = new AlertControl();
        }

        public static void SearchAndOpenEvent(SportType sportType, string eventId, string coefValue)
        {
            var found = false;
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
            var driver = new FirefoxDriver(prof);
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (sportType)
            {
                case SportType.Soccer:
                    driver.Navigate().GoToUrl(FootballUrl);
                    break;
                case SportType.Basketball:
                    driver.Navigate().GoToUrl(BasketballUrl);
                    break;
                case SportType.Hockey:
                    driver.Navigate().GoToUrl(HockeyUrl);
                    break;
                case SportType.Tennis:
                    driver.Navigate().GoToUrl(TennisUrl);
                    break;
                case SportType.Volleyball:
                    driver.Navigate().GoToUrl(VolleyballUrl);
                    break;
            }
            var eventRow = driver.FindElement(By.Id(eventId));
            var elements = eventRow.FindElements(By.ClassName(CoefClass));
            foreach (var element in elements)
            {
                if (!element.Text.Contains(coefValue))
                    continue;

                element.Click();
                found = true;
                break;
            }
            _alertControl.Show(null, found ? "Событие найдено" : "Событие не было найдено", "");
        }
    }
}
