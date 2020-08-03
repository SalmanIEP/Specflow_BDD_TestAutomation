using System;
using TechTalk.SpecFlow;
using Selenium.Intilaizer;
using NUnit.Framework;
using Selenium.Pages.MainPages;

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

        [Given(@"User must need to login before purchasing")]
        public void GivenUserMustNeedToLoginBeforePurchasing()
        {
            _contex.Get<HomePage>().GoToTheSite();
            _contex.Get<HomePage>().SelectSignInTab();
        }

        [Then(@"User must be Logged in to Sample Site Successfully")]
        public void ThenUserMustBeLoggedInToSampleSiteSuccessfully()
        {
            Assert.IsTrue(_contex.Get<LoginPage>().IsloggedIn(),"Unable to login to the site");
        }

        [Given(@"User Try to login with invalid credentials")]
        public void GivenUserTryToLoginWithInvalidCredentials()
        {
            _contex.Get<HomePage>().GoToTheSite();
            _contex.Get<HomePage>().SelectSignInTab();
        }

        [When(@"User provide InValid ""(.*)"" and ""(.*)""")]
        public void WhenUserProvideInValidAnd(string email, string password)
        {
            _contex.Get<LoginPage>().EnterLoginDetail(email, password);
        }

        [Then(@"User must be not allowed to logged in to the sample site")]
        public void ThenUserMustBeNotAllowedToLoggedInToTheSampleSite()
        {
            Assert.IsFalse(_contex.Get<LoginPage>().IsloggedIn(), "Unable to login to the site");
        }

    }
}
