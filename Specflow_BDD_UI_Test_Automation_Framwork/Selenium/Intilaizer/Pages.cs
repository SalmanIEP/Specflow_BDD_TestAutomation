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

        public void InitPages()
        {
            context.Set(new LoginPage(context));
        }
    }
}
