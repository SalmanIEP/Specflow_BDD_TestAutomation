using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Elements
{
  
    /// <summary>
    /// This Elements class contains the dictionary for all application selectors like id , css selectors etc
    /// </summary>
    public static class Elements
    {
        /// <summary>
        /// Elements Xpath's Dictionary 
        /// </summary>
        public static Dictionary<string, string> Xpath = new Dictionary<string, string>()
        {
            //2FA Authentication Page 
            { "Btn_Verify", "//*[contains(@value,'Verify')]"}

            //login

            // home
        };
        /// <summary>
        /// Elements ID's Dictionary
        /// </summary>
        public static Dictionary<string, string> ID = new Dictionary<string, string>()
        {
            //LogInPage
            { "Txt_Email", "okta-signin-username"},
            { "Txt_Password" , "okta-signin-password"},
            { "Btn_LogIn", "okta-signin-submit"},


        };
        /// <summary>
        /// Elements Css Selectors Dictionary
        /// </summary>
        public static Dictionary<string, string> Css = new Dictionary<string, string>()
        {
            //DumyPage
            { "BPF_NextStage", "id(\"stageAdvanceActionContainer\")/div"},
            { "BPF_NextStageMenu" , "id(\"stageNavigateActionContainer\")/div"},
            { "BPF_NextStageMenuOptions", "//div[@class=\"navigateMenuSection\"]"},
            { "BPF_PreviousStage", "id(\"stageBackActionContainer\")/div"}

        };
        /// <summary>
        /// Elements Class Disctionary 
        /// </summary>
        public static Dictionary<string, string> Class = new Dictionary<string, string>()
        {

            { "BPF_NextStage", "id(\"stageAdvanceActionContainer\")/div"},
            { "BPF_NextStageMenu" , "id(\"stageNavigateActionContainer\")/div"},
            { "BPF_NextStageMenuOptions", "//div[@class=\"navigateMenuSection\"]"},
            { "BPF_PreviousStage", "id(\"stageBackActionContainer\")/div"}

        };

        /// <summary>
        /// Elements Name dictionary 
        /// </summary>
        public static Dictionary<string, string> Name = new Dictionary<string, string>()
        {

            { "Txt_PassCode" , "answer"}

        };
       

    }
    /// <summary>
    /// This Class contains References for all pages Elements like login page.. etc 
    /// </summary>
    public static class Reference
    {
        public static class LogInPage
        {
            public static string EmailTextFeild = "Txt_Email";
            public static string PasswordTextFeild = "Txt_Password";
            public static string ButtonLogIn = "Btn_LogIn";

        }

        public static class TwoFactorAuthenticationPage
        {
            public static string PassCodeTextFeild = "Txt_PassCode";
            public static string ButtonVerify = "Btn_Verify";
        }
        
        public static class Navigation
        {
            public static string NavItem(string navigation) { return "//a[text()="+ navigation+ "]"; }
        }

    }
}

