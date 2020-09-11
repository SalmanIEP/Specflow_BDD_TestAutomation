using Newtonsoft.Json;
using OpenQA.Selenium;
using Selenium.Configuration;
using Selenium.Support.Browser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
namespace Specflow_BDD_UI_Test_Automation_Framwork.Helper
{
   public class helper
    {
        ScenarioContext _context;
        IWebDriver driver;
        public helper(ScenarioContext _context)
        {
            this._context = _context;
            driver = BrowserDriverFactory.driver;
        }

        /// <summary>
        /// This method will perform accessability test on the drive page
        /// </summary>
        /// <param name="PageName"></param>
        public void PerformAccessabilityTest(String PageName)
        {
            var _Driver = driver;
            Hooks.Hooks.SetFeature(PageName);
            Hooks.Hooks.Createlogs("Performing Accessabilty testing on  ......"+PageName);
            Thread.Sleep(5000);
            string currentPageName = _Driver.Url;
            var Violations = AccessabilityTest.AnalyzePage(_Driver, PageName).Violations;
            if (Violations.Length == 0)
            {
                Hooks.Hooks.Createlogs("No violation found on : " + currentPageName);
            }
            else
            {
                Hooks.Hooks.Createlogs("Please find below violations on PageName :" + currentPageName);
                foreach (var violation in Violations)
                {
                    string tag = "";
                    string Html = "";
                    string json = JsonConvert.SerializeObject(Violations);
                    string Violation = "<b>Violation ID:</b> " + violation.Id + "<br/>" + "<b>Description:</b> " + violation.Description + "<br/>" + "<b>Help: </b>" + violation.Help + "<br/>" + "<b>HelpURL: </b>" + violation.HelpUrl + "<br/>" + "<b>Impact: </b>" + violation.Impact;
                    foreach (var tags in violation.Tags)
                    {
                        tag = tag + " , "+tags;
                      
                    }
                    foreach (var node in violation.Nodes)
                    {
                        string html = node.Html.ToString();
                        Html = Html+"  ,  "+ html;
                    }

                    Violation = Violation + "<br/>" + "Tags: "+tag + "<br/>" +"<b>Html: </b>"+ Html;
                    Hooks.Hooks.Createlogs(Violation);
                    var js = JsonConvert.DeserializeObject(json);
                }

            }
        }

    }
}
