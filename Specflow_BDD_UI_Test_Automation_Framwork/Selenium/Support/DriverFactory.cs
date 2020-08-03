using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Selenium.Support
{
    public enum Browsers
    {
        Chrome,
        FireFox,
        Edge
    }

    [Binding]
    public sealed class DriverFactory
    {
        ScenarioContext _context;

        public DriverFactory(ScenarioContext _context)
        {
            this._context = _context;
        }

        private IWebDriver _driver;

        public void Init(Browsers browser, bool IsRemote)
        {
            if (IsRemote == false)
            {
                if (browser == Browsers.Chrome)
                {
                    string dirpath = Directory.GetCurrentDirectory();
                    _driver = new ChromeDriver(dirpath);
                    _context.Set(_driver);
                }
                else if (browser == Browsers.FireFox)
                {
                    _driver = new FirefoxDriver();
                    _context.Set(_driver);
                }

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            }

            else
            {
                DesiredCapabilities capability = new DesiredCapabilities();
                capability.SetCapability("platform", "WINDOWS");
                if (browser == Browsers.Chrome)
                {
                    capability.SetCapability("browserName", "chrome");
                }
                else if (browser == Browsers.FireFox)
                {
                    capability.SetCapability(capability.BrowserName, "Firefox");
                }

                _driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                _context.Set(_driver);
            }
        }
    }

}
