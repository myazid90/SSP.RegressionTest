using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SSP.RegressionTest.TestSandbox
{
    class SimpleTest
    {
        private IWebDriver driver;

        [Test]
        public void BasicOpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://selenium.dev");

            var title = driver.Title;

            Console.WriteLine($"printing to console: {title}");
            Thread.Sleep(2000);

            driver.Quit();
        }

    }
}
