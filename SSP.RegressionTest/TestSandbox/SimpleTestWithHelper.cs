using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SSP.RegressionTest.Helper;
using System;
using System.Threading;

namespace SSP.RegressionTest.TestSandbox
{
    class SimpleTestWithHelper : DriverHelper
    {
        [Test]
        public void BasicOpenBrowserWithHelper()
        {
            ChromeSettings setting = new();
            ChromeOptions options = new();
            options.AddArgument(setting.Userdatadir);
            options.AddArgument(setting.Userprofile);

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl(@"http://selenium.dev");

            var title = driver.Title;

            Console.WriteLine($"printing to console: {title} ");
            Thread.Sleep(2000);

            driver.Quit();
        }
    }
}
