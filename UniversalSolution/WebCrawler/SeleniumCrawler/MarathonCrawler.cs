using DataParser.Enums;
using DevExpress.XtraBars.Alerter;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Drawing;
using System.Windows.Forms;

namespace WebCrawler.SeleniumCrawler
{
    public static class MarathonCrawler
    {
        private const string CoefClass = "height-column-with-price";
        private const string FootballUrl = "https://www.marathonbet.com/su/betting/Football/";
        private const string TennisUrl = "https://www.marathonbet.com/su/betting/Tennis/";
        private const string BasketballUrl = "https://www.marathonbet.com/su/betting/Basketball/";
        private const string HockeyUrl = "https://www.marathonbet.com/su/betting/Ice+Hockey/";
        private const string VolleyballUrl = "https://www.marathonbet.com/su/betting/Volleyball/";

        private static readonly AlertControl AlertControl;
        private static bool _busy = false;

        static MarathonCrawler()
        {
            AlertControl = new AlertControl();
            AlertControl.BeforeFormShow += _alertControl_BeforeFormShow;
        }

        private static void _alertControl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2, (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        public static void SearchAndOpenEvent(SportType sportType, string eventId, string coefValue)
        {
            if (_busy) return;

            var found = false;
            try
            {
                _busy = true;
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
                driver.Close();
            }
            catch
            {
                //found = false;
            }
            finally
            {
                AlertControl.Show(null, found ? "Событие найдено" : "Событие не было найдено", "");
                _busy = false;
            }
        }
    }
}
