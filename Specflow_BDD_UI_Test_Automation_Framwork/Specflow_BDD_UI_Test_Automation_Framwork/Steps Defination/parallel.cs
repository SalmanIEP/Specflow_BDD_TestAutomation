using OpenQA.Selenium;
using Selenium.Configuration;
using Selenium.Pages.MainPages;
using Selenium.Support.Browser;
using Specflow_BDD_UI_Test_Automation_Framwork.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Steps_Defination
{
    [Binding]
    public sealed class parallel
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        IWebDriver driver;
        public parallel(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            driver= BrowserDriverFactory.driver;
        }
        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int p0)
        {
            var config = ConfigurationManager.Configuration();
            var Url = config["TestEnviornmentUrl"];
            driver.Navigate().GoToUrl(Url);
            helper help = new helper(_scenarioContext);
            help.PerformAccessabilityTest("OKta 2FA Page");
        }

        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int p0)
        {
            _scenarioContext.Get<LoginPage>().EnterLoginDetail("youemail", "yourpassword");
            _scenarioContext.Get<LoginPage>().SelectLogginButton();
        }

        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _scenarioContext.Get<LoginPage>().Enter2FAPasscode();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            _scenarioContext.Get<LoginPage>().SelectVerifyButton();
        }


    }
}
