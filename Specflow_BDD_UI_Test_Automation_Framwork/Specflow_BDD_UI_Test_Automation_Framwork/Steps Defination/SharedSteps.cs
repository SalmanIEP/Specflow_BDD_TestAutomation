using Selenium.Pages.MainPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Specflow_BDD_UI_Test_Automation_Framwork.Helper;

namespace Specflow_BDD_UI_Test_Automation_Framwork.Steps_Defination
{
    [Binding]
    public sealed class SharedSteps
    {
        ScenarioContext _context;

        public SharedSteps(ScenarioContext _context)
        {
            this._context = _context;
        }

        [When(@"Select the Login Button")]
        public void WhenSelectTheLoginButton()
        {
           _context.Get<LoginPage>().SelectLogginButton();
        }

        [When(@"User provide Valid ""(.*)"" and ""(.*)""")]
        public void WhenUserProvideValidAnd(string email, string password)
        {
            _context.Get<LoginPage>().EnterLoginDetail(email, password);
        }

    }
}
