using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Selenium.Configuration;
using TechTalk.SpecFlow;

namespace Selenium.Pages.MainPages
{
   public  class LoginPage : BasePage
    {
        public LoginPage(ScenarioContext context):base(context)
        {

        }
        By Txt_Email = By.CssSelector("#email");
        By Txt_Password = By.CssSelector("#passwd");
        By Btn_LogIn = By.CssSelector("#email");

        public void EnterLoginDetail(string email, string password)
        {
           type(email,Txt_Email);
           type(ConfigurationManager.DecodePassword(password), Txt_Password);
           
        }
        public void SelectLogginButton()
        {
            click(Btn_LogIn);
        }

        public bool IsloggedIn()
        {
            bool Loggedin = false;
            if (_context.Get<IWebDriver>().Url.Contains("my-account"))
            {
                Loggedin = true;
            }

            return Loggedin;
        }
    }
}
