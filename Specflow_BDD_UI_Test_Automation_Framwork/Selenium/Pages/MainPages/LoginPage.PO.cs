using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using Selenium.Configuration;
using TechTalk.SpecFlow;


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
        By Txt_Email = By.Id("okta-signin-username");
        By Txt_Password = By.Id("okta-signin-password");
        By Btn_LogIn = By.Id("okta-signin-submit");
        By Txt_PassCode = By.Name("answer");
        By Btn_Verify = By.XPath("//*[contains(@value,'Verify')]");

        public void EnterLoginDetail(string email, string password)
        {
           type(email,Txt_Email);
           type(password, Txt_Password);
        }

        public void Enter2FAPasscode()
        {
            type(context.Get<string>("2FAPassCode"), Txt_PassCode);
        }
        public void SelectLogginButton()
        {
            click(Btn_LogIn);
        }
        public void SelectVerifyButton()
        {
            click(Btn_Verify);
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
