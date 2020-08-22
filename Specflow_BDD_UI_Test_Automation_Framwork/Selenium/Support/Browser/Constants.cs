using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Support
{
    public static partial class Constants
    {
        /// <summary>
        /// The default amount of time to wait for an operation to complete by the Selenium driver.
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 0, 30);

        /// <summary>
        /// The default amount of time to wait between retrying command executions if they fail.
        /// Value is expressed in miliseconds.
        /// </summary>
        public const int DefaultRetryDelay = 5000;

        /// <summary>
        /// The default number of retry attempts for a command execution if it fails.
        /// </summary>
        public const int DefaultRetryAttempts = 2;

        /// <summary>
        /// The default page to direct a user to if none other is specified.
        /// </summary>
        public const string DefaultLoginUri = "https://portal.office.com";

        /// <summary>
        /// The default tracing source for browser automation.
        /// </summary>
        public const string DefaultTraceSource = "BrowserAutomation";

        /// <summary>
        /// The default tracing source for browser automation.
        /// </summary>
        public const int DefaultThinkTime = 2000;

        /// <summary>
        /// Constants and defaults related to the InteractiveBrowser.
        /// </summary>
    }
}
