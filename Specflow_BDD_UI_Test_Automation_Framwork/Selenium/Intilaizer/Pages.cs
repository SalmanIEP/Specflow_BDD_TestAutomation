using Selenium.Pages.MainPages;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Selenium.Intilaizer
{
    [Binding]
    public class Pages
    {
        ScenarioContext context;
        public Pages(ScenarioContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// This Method will Intialize the pages classes and add into the scenrio context
        /// you can add all your project pages classes in to this method
        /// </summary>
        public void InitPages()
        {
            context.Set(new LoginPage(context));
        }
    }
}
