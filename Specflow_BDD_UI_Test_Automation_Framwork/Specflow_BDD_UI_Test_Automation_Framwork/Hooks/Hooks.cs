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
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentTest test;
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public Hooks(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        //------------------------------------------------------------------

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            var config = ConfigurationManager.Configuration();
            var isRemote = config["IsRemoteDriver"];
            if (config["Is2FEnabled"].Equals("true"))
            {
                _scenarioContext.Add("2FAPassCode", _2FAuthentication.GetGoogleAuthenticationPassCode());
            }
            DriverFactory factory = new DriverFactory(_scenarioContext);
            factory.Init(Browsers.Chrome, bool.Parse(isRemote));
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
        public void InsertReportingSteps()
        {


            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);
            //Pass status
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            //Failure Staus
            if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    ScenarioContext.Current.Pending();
                else if (stepType == "When")
                    ScenarioContext.Current.Pending();
                else if (stepType == "Then")
                    ScenarioContext.Current.Pending();
                else if (stepType == "And")
                    ScenarioContext.Current.Pending();

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