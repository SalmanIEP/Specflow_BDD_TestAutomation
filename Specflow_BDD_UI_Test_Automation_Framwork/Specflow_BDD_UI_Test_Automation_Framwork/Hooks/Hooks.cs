using Selenium.Intilaizer;
using System;
using Selenium.Support;
using TechTalk.SpecFlow;
using Selenium.Configuration;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using BoDi;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;
using AventStack.ExtentReports.Reporter;
using System.Reflection;
using Selenium.Support.Browser;
using System.Diagnostics;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Hooks
{
    [Binding]
    public sealed class Hooks : Pages
    {
        private readonly ScenarioContext _scenarioContext;
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        [ThreadStatic]
        public static ExtentTest test;

        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public Hooks(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //------------------------------------------------------------------

        [AfterFeature]
        public static void AfterFeature()
        {
           BrowserDriverFactory.driver.Quit();
           BrowserDriverFactory.driver = null;
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var config = ConfigurationManager.Configuration();
            var isRemote = config["IsRemoteDriver"];
            var NewInstance = config["NewInstanceofBrowserForeachScenario"];
            if (config["Is2FEnabled"].Equals("true"))
            {
                _scenarioContext.Add("2FAPassCode", _2FAuthentication.GetGoogleAuthenticationPassCode());
            }
            if (NewInstance.Equals("false"))
            {
                if (BrowserDriverFactory.driver == null)
                {
                    BrowserDriverFactory factory = new BrowserDriverFactory(_scenarioContext);
                    factory.CreateWebDriver(TestSetting.Options);
                    InitPages();
                }
                InitPages();
            }
            else
            {
                BrowserDriverFactory factory = new BrowserDriverFactory(_scenarioContext);
                factory.CreateWebDriver(TestSetting.Options);
                InitPages();
            }
            
        }
      
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {

        }
        
        [AfterScenario]
        public void AfterScenario()
        {
            var config = ConfigurationManager.Configuration();
            var NewInstance = config["NewInstanceofBrowserForeachScenario"];
            if (NewInstance.Equals("true"))
            {
                _scenarioContext.Get<IWebDriver>().Quit();
                _scenarioContext.Clear();
            }
           
        }
        public static void SetFeature(string Page)
        {
           // test = extent.CreateTest<Feature>("Acceability Report for: " + Page);
        }
        public static void Createlogs(string message)
        {
           // test.CreateNode<Scenario>(message).Fail("");
        }


    }
}