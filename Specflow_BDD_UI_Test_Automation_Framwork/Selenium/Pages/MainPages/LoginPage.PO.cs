using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using Selenium.Configuration;
using TechTalk.SpecFlow;
using Selenium.Elements;


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
        public void EnterLoginDetail(string email, string password)
        {

           type(email,By.Id(elements.ID[Reference.LogInPage.EmailTextFeild]));
           type(password, By.Id(elements.ID[Reference.LogInPage.PasswordTextFeild]));
        }

        public void Enter2FAPasscode()
        {
            Thread.Sleep(3000);  
            type(context.Get<string>("2FAPassCode"), By.Name(elements.Name[Reference.TwoFactorAuthenticationPage.PassCodeTextFeild]));
        }
        public void SelectLogginButton()
        {
            click(By.Id(elements.ID[Reference.LogInPage.ButtonLogIn]));
        }
        public void SelectVerifyButton()
        {
            click(By.XPath(elements.Xpath[Reference.TwoFactorAuthenticationPage.ButtonVerify]));
            Thread.Sleep(5000);
        }

        public bool IsloggedIn()
        {
            bool Loggedin = false;
            if (_context.Get<IWebDriver>().Url.Contains("admin/getting-started"))
            {
                Loggedin = true;
            }

            return Loggedin;
        }

    }
}
