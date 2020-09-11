using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Selenium.Intilaizer;
using Selenium.Configuration;
using Selenium.Support.Extensions;
using Selenium.Support.Browser;

namespace Selenium.Pages
{
    [Binding]
    public abstract class BasePage
    {
        public readonly ScenarioContext _context;
        IWebDriver driver;
        public BasePage(ScenarioContext _context)
        {
            this._context = _context;
            driver = BrowserDriverFactory.driver;
        }
        public void type(String inputText, By locator)
        {
            find(locator).Clear();
            find(locator).SendKeys(inputText);
        }
       
        public IWebElement find(By locator)
        {
         
            driver.WaitUntilAvailable(locator);
            
            return driver.FindElement(locator);
        }

        public void click(By locator)
        {
            driver.WaitUntilClickable(locator, TimeSpan.FromSeconds(20),"Element is not clickable");
            find(locator).Click();
        }

        public String getText(By locator)
        {
            return find(locator).Text;
        }

        public Boolean isDisplayed(By locator)
        {
            try
            {
                return find(locator).Displayed && find(locator).Enabled;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public void submit(By locator)
        {
            find(locator).Submit();
        }
    }
}

