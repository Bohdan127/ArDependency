using DevExpress.XtraBars.Alerter;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace WebCrawler.SeleniumCrawler
{
    public static class PinnacleCrawler
    {
        private static readonly AlertControl AlertControl;
        private static bool _busy = false;

        private const string MainUrl = "https://www.pinnacle.com/en/rtn"; //https://www.pinnacle.com/en/rtn
        private const string LoginButtonClass = "blue-bg";//class="button blue-bg login"
        private const string LoginFieldClass = "customerId";//class="customerId"
        private const string PassFieldClass = "password";//name="CustomerId"
        private const string ButtonLoginClass = "loginSubmit";


        static PinnacleCrawler()
        {
            AlertControl = new AlertControl();
        }

        public static void SearchAndOpenEvent()
        {
            if (_busy) return;

            var found = false;
            try
            {
                var prof = new FirefoxProfile();
                prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
                prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
                var driver = new FirefoxDriver(prof);

                driver.Navigate().GoToUrl(MainUrl);
                Thread.Sleep(2000);
                var element = driver.FindElement(By.ClassName(LoginButtonClass));
                element.Click();
                Thread.Sleep(2000);
                element = driver.FindElement(By.ClassName(LoginFieldClass));
                element.SendKeys("VB794327");
                element = driver.FindElement(By.ClassName(PassFieldClass));
                element.SendKeys("artem89@");
                element = driver.FindElement(By.ClassName(ButtonLoginClass));
                element.Click();
                Thread.Sleep(2000);
                _busy = true;
                found = true;
            }
            catch
            {
                //found = false;
            }
            finally
            {
                AlertControl.Show(null, found ? "Вход был успешным" : "Не войти", "");
                _busy = false;
            }
        }
    }
}