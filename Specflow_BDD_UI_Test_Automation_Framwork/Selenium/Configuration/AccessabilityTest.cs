using System;
using System.Collections.Generic;
using System.Text;
using Selenium.Axe;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System.IO;
using System.Reflection;

namespace Selenium.Configuration
{
    public static class AccessabilityTest
    {
        public static AxeResult AnalyzePage(IWebDriver driver,string Name)
        {
            var result = new AxeBuilder(driver).WithTags("wcag21aa", "wcag2aa").Analyze();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            outPutDirectory = outPutDirectory.Substring(0, outPutDirectory.IndexOf("bin"));
            outPutDirectory = outPutDirectory.Substring(outPutDirectory.IndexOf("\\") + 1);
            String path = Path.Combine(outPutDirectory, "TestResults\\"+Name + "-AxeReport.html");
            driver.CreateAxeHtmlReport(result, path);
            return result;
        }

    }
}
