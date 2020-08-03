using Selenium.Intilaizer;
using System;
using Selenium.Support.Extensions;
using TechTalk.SpecFlow;
using Selenium.Configuration;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using BoDi;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;
using AventStack.ExtentReports.Reporter;
using System.Reflection;
using Selenium.Support;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Hooks
{
    [Binding]
    public sealed class Hooks : Pages
    {
        private readonly ScenarioContext _scenarioContext;
        public Hooks(ScenarioContext scenarioContext):base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        //------------------------------------------------------------------


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
           
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var config = ConfigurationManager.Configuration();
            var isRemote = config["IsRemoteDriver"];
            DriverFactory factory = new DriverFactory(_scenarioContext);
            factory.Init(Browsers.Chrome, bool.Parse(isRemote));
            InitPages();
        }
        [AfterScenario]
        public void AfterScenario()
        {
            _scenarioContext.Get<IWebDriver>().Quit();
            _scenarioContext.Clear();
        }
    }
}
