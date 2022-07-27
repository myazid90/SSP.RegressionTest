using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;

namespace SSP.RegressionTest.TestSandbox
{
    class SimpleTestWithHelper : DriverHelper
    {
        [Test]
        public void BasicOpenBrowserWithHelper()
        {
            //Implemented appsettings.json file, replacement for app.config file
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //To retrieve the value 
            var userdatadir = config.GetValue<string>("datadir");
            var userprofile = config.GetValue<string>("chromesettings:user-profile");
            var customargs = config.GetValue<string>("chromesettings:customargs:insideargs");

            ChromeOptions options = new();
            options.AddArgument(userdatadir);
            options.AddArgument(userprofile);
            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl(@"http://selenium.dev");

            var title = driver.Title;

            Console.WriteLine($"printing to console: {title} , custom message {customargs}");
            Thread.Sleep(2000);

            driver.Quit();
        }
    }
}
