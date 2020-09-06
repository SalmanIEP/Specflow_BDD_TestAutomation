using Microsoft.Extensions.Configuration;
using Selenium.Configuration;
using Selenium.Support.Browser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Specflow_BDD_UI_Test_Automation_Framwork
{
    public static class TestSetting
    {
       static IConfiguration config= ConfigurationManager.Configuration();
        public static string InvalidAccountLogicalName = "accounts";
        private static readonly string Type = config["BrowserType"];
        private static readonly string RemoteType = config["RemoteBrowserType"];
        private static readonly string RemoteHubServerURL = config["RemoteHubServer"];
        private static readonly string DriversPath = config["DriversPath"] ?? string.Empty;
        private static readonly bool UsePrivateMode = Convert.ToBoolean(config["UsePrivateMode"] ?? bool.TrueString);
        private static readonly string browserstack_user = config["browserstack.user"];
        private static readonly string browserstack_Key = config["browserstack.key"];
        // Create a new options instance, copy of the share, to use just in the current test, modifications in test will not affect other tests
        public static BrowserOptions Options => new BrowserOptions
        {
            BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), Type),
            PrivateMode = UsePrivateMode,
            FireEvents = false,
            Headless = false,
            UserAgent = false,
            DefaultThinkTime = 2000,
            UCITestMode = true,
            UCIPerformanceMode = true,
            DriversPath = Path.IsPathRooted(DriversPath) ? DriversPath : Path.Combine(Directory.GetCurrentDirectory(), DriversPath),
            DisableExtensions = false,
            DisableFeatures = false,
            DisablePopupBlocking = false,
            DisableSettingsWindow = false,
            EnableJavascript = false,
            NoSandbox = false,
            DisableGpu = false,
            DumpDom = false,
            EnableAutomation = false,
            DisableImplSidePainting = false,
            DisableDevShmUsage = false,
            DisableInfoBars = false,
            TestTypeBrowser = false,
            RemoteHubServer= new Uri(RemoteHubServerURL),
            browserstackkey=browserstack_Key,
            browserstackuser=browserstack_user,
            os= "Windows",
            os_version= "10",
            browser_version= "85.0",
            RemoteBrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), RemoteType),
            
        };
    }
}
