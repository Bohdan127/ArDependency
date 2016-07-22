using DataParser.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebCrawler.SeleniumCrawler
{
    public static class MarathonCrawler
    {
        private const string coefClass = "selection-link.normal"; //class="selection-link.normal"
        private const string footballUrl = "https://www.marathonbet.com/su/popular/Football/?menu=true";
        private const string tennisUrl = "https://www.marathonbet.com/su/popular/Tennis/?menu=true";
        private const string basketballUrl = "https://www.marathonbet.com/su/popular/Basketball/?menu=true";
        private const string hockeyUrl = "https://www.marathonbet.com/su/popular/Ice+Hockey/?menu=true";
        /*
         * ?????????????????????????
         * Volleyball wasn't found in marathon 
         * ?????????????????????????
         */
        private const string volleyballUrl = "https://www.marathonbet.com/su/popular/Volleyball/?menu=true";
        public static void SearchAndOpenEvent(SportType sportType, string eventId, string coefValue)
        {
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
            var driver = new FirefoxDriver(prof);
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (sportType)
            {
                case SportType.Soccer:
                    driver.Navigate().GoToUrl(footballUrl);
                    break;
                case SportType.Basketball:
                    driver.Navigate().GoToUrl(basketballUrl);
                    break;
                case SportType.Hockey:
                    driver.Navigate().GoToUrl(hockeyUrl);
                    break;
                case SportType.Tennis:
                    driver.Navigate().GoToUrl(tennisUrl);
                    break;
                case SportType.Volleyball:
                    driver.Navigate().GoToUrl(volleyballUrl);
                    break;
            }
            var eventRow = driver.FindElement(By.Id(eventId));
            var elements = eventRow.FindElements(By.ClassName(coefClass));
            foreach (var element in elements)
            {
                if (!element.Text.Contains(coefValue))
                    continue;

                element.Click();
                break;
            }
        }
    }
}
