using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SSP.RegressionTest.FrontEnd
{
    class SSPHomePage : DriverHelper
    {
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new();
            options.AddArgument("user-data-dir=C:/Users/myt/AppData/Local/Google/Chrome/User Data");
            options.AddArgument("profile-directory=Profile 2");

            //options.AddArguments(args);
            driver = new ChromeDriver(options);
        }

        [Test]
        // Only two widgets should be visible. 
        // Knowledge Base and Status Page. 
        public void SHP001_LimitedModules_should_VisibleForUnauthorizedUser()
        {
            driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/csm");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(e => e.PageSource.Contains("Self-Service Portal"));

            string[] widgets = { "Knowledge Base", "Status Page" };
            Assert.That(widgets, Is.AnyOf(driver.PageSource));
        }

        public void SHP002_AllModules_should_VisibleForCTC()
        {

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
