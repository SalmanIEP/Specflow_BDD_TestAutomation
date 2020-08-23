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
using Selenium.Support;
using Selenium.Support.Browser;
using System.Diagnostics;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Hooks
{
    [Binding]
    public sealed class Hooks : Pages
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _feature;
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

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            Debugger.Launch();
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            var config = ConfigurationManager.Configuration();
            var isRemote = config["IsRemoteDriver"];
            if (config["Is2FEnabled"].Equals("true"))
            {
                _scenarioContext.Add("2FAPassCode", _2FAuthentication.GetGoogleAuthenticationPassCode());
            }
            BrowserDriverFactory factory = new BrowserDriverFactory(_scenarioContext);
            factory.CreateWebDriver(TestSetting.Options);
            InitPages();
        }
        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            outPutDirectory = outPutDirectory.Substring(0, outPutDirectory.IndexOf("bin"));

            outPutDirectory = outPutDirectory.Substring(outPutDirectory.IndexOf("\\") + 1);
            String path = Path.Combine(outPutDirectory, "TestResults\\index.html");
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {


            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(scenarioContext, null);
            //Pass status
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
            }
            //Failure Staus
            if (scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
            }

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenarioContext.Pending();
                else if (stepType == "When")
                    scenarioContext.Pending();
                else if (stepType == "Then")
                    scenarioContext.Pending();
                else if (stepType == "And")
                    scenarioContext.Pending();

            }
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }
        [AfterScenario]
        public void AfterScenario()
        {
            _scenarioContext.Get<IWebDriver>().Quit();
            _scenarioContext.Clear();
        }
        public static void SetFeature(string Page)
        {
            test = extent.CreateTest<Feature>("Acceability Report for: " + Page);
        }
        public static void Createlogs(string message)
        {
            test.CreateNode<Scenario>(message).Fail("");
        }


    }
}