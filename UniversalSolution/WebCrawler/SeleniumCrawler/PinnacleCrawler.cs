using DataSaver;
using DevExpress.XtraBars.Alerter;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

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

        public static void SearchAndOpenEvent()
        {
            _logger.Trace("start SearchAndOpenEvent.");
            if (_busy)
            {
                _logger.Info("_busy => return!!!");
                return;
            }

            var found = false;
            try
            {
                var prof = new FirefoxProfile();
                prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
                prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
                var driver = new FirefoxDriver(prof);

                var user = LocalSaver.FindUser();


                driver.Navigate().GoToUrl(MainUrl);
                Thread.Sleep(2000);
                var element = driver.FindElement(By.ClassName(LoginButtonClass));
                element.Click();
                Thread.Sleep(2000);
                element = driver.FindElement(By.ClassName(LoginFieldClass));
                element.SendKeys(user.Login);
                element = driver.FindElement(By.ClassName(PassFieldClass));
                element.SendKeys(user.Password);
                element = driver.FindElement(By.ClassName(ButtonLoginClass));
                element.Click();
                Thread.Sleep(2000);
                _busy = true;
                found = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            finally
            {
                _logger.Info(found ? "LogIn was successful" : "LogIn was not successful");
                AlertControl.Show(null, found ? "Вход был успешным" : "Не войти", "");
                _busy = false;
            }
            _logger.Trace("End SearchAndOpenEvent.");
        }
    }
}