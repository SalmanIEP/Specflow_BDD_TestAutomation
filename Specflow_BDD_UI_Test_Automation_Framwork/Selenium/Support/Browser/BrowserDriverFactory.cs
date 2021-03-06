﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Support.Browser
{
   public class BrowserDriverFactory
    {
        ScenarioContext context;
        public BrowserDriverFactory(ScenarioContext context)
        {
            this.context = context;
        }
        public static IWebDriver driver;
        /// <summary>
        /// This method will a launch a browser with option that you can set in testsetting file in your test project
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IWebDriver CreateWebDriver(BrowserOptions options)
        {

            switch (options.BrowserType)
            {
                case BrowserType.Chrome:
                    var chromeService = ChromeDriverService.CreateDefaultService(options.DriversPath);
                    chromeService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new ChromeDriver(chromeService, options.ToChrome());
                    context.Set(driver);
                    break;
                case BrowserType.IE:
                    var ieService = InternetExplorerDriverService.CreateDefaultService(options.DriversPath);
                    ieService.SuppressInitialDiagnosticInformation = options.HideDiagnosticWindow;
                    driver = new InternetExplorerDriver(ieService, options.ToInternetExplorer(), TimeSpan.FromMinutes(20));
                    context.Set(driver);
                    break;
                case BrowserType.Firefox:
                    var ffService = FirefoxDriverService.CreateDefaultService(options.DriversPath);
                    ffService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new FirefoxDriver(ffService);
                    driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
                    context.Set(driver);
                    break;
                case BrowserType.Edge:
                    var edgeService = EdgeDriverService.CreateDefaultService(options.DriversPath);
                    edgeService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new EdgeDriver(edgeService, options.ToEdge(), TimeSpan.FromMinutes(20));
                    context.Set(driver);
                    break;
                case BrowserType.Remote:
                    ICapabilities capabilities = null;
                    ChromeOptions option = new ChromeOptions();
                    switch (options.RemoteBrowserType)
                    {
                        case BrowserType.Chrome:
                            
                            option.AddAdditionalCapability("os", options.os.ToString(),true);
                            option.AddAdditionalCapability("os_version", options.os_version.ToString(),true);
                            option.AddAdditionalCapability("browser_version", options.browser_version.ToString(), true);
                            option.AddAdditionalCapability("browserstack.user", options.browserstackuser.ToString(), true);
                            option.AddAdditionalCapability("browserstack.key", options.browserstackkey.ToString(), true);
                            option.AddAdditionalCapability("name", "FirstTest",true);
                            option.AddAdditionalCapability("browser",options.RemoteBrowserType.ToString(), true);
                            capabilities = option.ToCapabilities();
                            break;
                        case BrowserType.Firefox:
                            capabilities = options.ToFireFox().ToCapabilities();
                            break;
                    }
                    driver = new RemoteWebDriver(options.RemoteHubServer, option);
                    context.Set(driver);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"The browser type '{options.BrowserType}' is not recognized.");
            }

            driver.Manage().Timeouts().PageLoad = options.PageLoadTimeout;

            // StartMaximized overrides a set width & height
            if (options.StartMaximized && options.BrowserType != BrowserType.Chrome) //Handle Chrome in the Browser Options
                driver.Manage().Window.Maximize();
            else if (!options.StartMaximized && options.Width.HasValue && options.Height.HasValue)
                driver.Manage().Window.Size = new System.Drawing.Size(options.Width.Value, options.Height.Value);

            if (options.FireEvents || options.EnableRecording)
            {
                // Wrap the newly created driver.
                driver = new EventFiringWebDriver(driver);
            }

            return driver;
        }

    }
}
