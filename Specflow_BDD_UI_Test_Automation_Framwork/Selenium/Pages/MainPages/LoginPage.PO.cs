using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using Selenium.Configuration;
using TechTalk.SpecFlow;
using Selenium.Elements;
using Selenium.Support.Browser;

namespace Selenium.Pages.MainPages
{
   public class LoginPage : BasePage
    {
        ScenarioContext context;
        IWebDriver driver;
        public LoginPage(ScenarioContext context):base(context)
        {
            this.context = context;
            
        }
        /// <summary>
        /// This method is used to get looged in to the site 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public void EnterLoginDetail(string email, string password)
        {
           type(email,By.Id(Elements.Elements.ID[Reference.LogInPage.EmailTextFeild]));
           type(password, By.Id(Elements.Elements.ID[Reference.LogInPage.PasswordTextFeild]));
        }

        public void Enter2FAPasscode()
        {
            Thread.Sleep(3000);  
            type(context.Get<string>("2FAPassCode"), By.Name(Elements.Elements.Name[Reference.TwoFactorAuthenticationPage.PassCodeTextFeild]));
        }
        public void SelectLogginButton()
        {
            click(By.Id(Elements.Elements.ID[Reference.LogInPage.ButtonLogIn]));
        }
        public void SelectVerifyButton()
        {
           
            click(By.XPath(Reference.Navigation.NavItem("Home")));
            click(By.XPath(Elements.Elements.Xpath[Reference.TwoFactorAuthenticationPage.ButtonVerify]));
            Thread.Sleep(5000);
        }

        public bool IsloggedIn()
        {
            bool Loggedin = false;
            if (BrowserDriverFactory.driver.Url.Contains("admin/getting-started"))
            {
                Loggedin = true;
            }

            return Loggedin;
        }

    }
}
