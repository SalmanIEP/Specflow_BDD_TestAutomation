using OpenQA.Selenium;
using Selenium.Intilaizer;
using System;
using System.Collections.Generic;
using Selenium.Configuration;
using System.Text;
using TechTalk.SpecFlow;

namespace Selenium.Pages.MainPages
{
    public class HomePage : BasePage
    {
        public HomePage(ScenarioContext _context) : base(_context)
        {

        }

        By Tab_SignIn = By.ClassName("login");
        
        public void GoToTheSite()
        {
            var config = ConfigurationManager.Configuration();
            var Url = config["TestEnviornmentUrl"];
            _context.Get<IWebDriver>().Navigate().GoToUrl(Url);
            
        }
        public void SelectSignInTab()
        {
            click(Tab_SignIn);
        }
    }
}
