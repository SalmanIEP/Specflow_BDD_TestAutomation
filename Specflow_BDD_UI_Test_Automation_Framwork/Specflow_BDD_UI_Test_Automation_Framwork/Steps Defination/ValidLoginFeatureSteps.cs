using System;
using TechTalk.SpecFlow;
using Selenium.Intilaizer;
using NUnit.Framework;
using Selenium.Pages.MainPages;
using Selenium.Configuration;
using OpenQA.Selenium;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Steps_Defination
{
    [Binding]
    public class ValidLoginFeatureSteps
    {
        private readonly ScenarioContext _contex;
        public ValidLoginFeatureSteps(ScenarioContext _contex)
        {
            this._contex = _contex;
        }

        [Given(@"User must need to login before continue")]
        public void GivenUserMustNeedToLoginBeforeContinue()
        {
            var config = ConfigurationManager.Configuration();
            var Url = config["TestEnviornmentUrl"];
            _contex.Get<IWebDriver>().Navigate().GoToUrl(Url);
        }

        [Then(@"User need to provide Valid Passcode for (.*)FA to further allowed to move to the site")]
        public void ThenUserNeedToProvideValidPasscodeForFAToFurtherAllowedToMoveToTheSite(int p0)
        {
            _contex.Get<LoginPage>().Enter2FAPasscode();
        }

        [Then(@"Select Verfiy button")]
        public void ThenSelectVerfiyButton()
        {
            Helper.helper help = new Helper.helper(_contex);
            help.PerformAccessabilityTest("OKta 2FA Page");
            _contex.Get<LoginPage>().SelectVerifyButton();
        }
        [Then(@"User must be Logged in to Sample Site Successfully")]
        public void ThenUserMustBeLoggedInToSampleSiteSuccessfully()
        {
            Assert.IsTrue(_contex.Get<LoginPage>().IsloggedIn(),"Unable to login to the site");
            Helper.helper help = new Helper.helper(_contex);
            help.PerformAccessabilityTest("Home Page");
        }

    }
}
