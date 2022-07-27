using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SSP.RegressionTest.Modal;
using SSP.RegressionTest.Pages;

namespace SSP.RegressionTest.BackOffice
{
    class BackOfficeMainPage : DriverHelper
    {
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new();
            options.AddArgument("user-data-dir=C:/Users/myt/AppData/Local/Google/Chrome/User Data");
            options.AddArgument("profile-directory=Profile 2");

            driver = new ChromeDriver(options);
        }

        [Test]
        public void BOM001_Impersonate_CloudOps_should_successful()
        {
            driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/");
            //Thread.Sleep(5000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(e => e.Url.Contains("dashboard"));

            Pages.BackOffice backOffice = new Pages.BackOffice();
            backOffice.ClickUserInfo();
            backOffice.ClickImpersonate();

            ImpersonateDialogBox impersonateDialogBox = new ImpersonateDialogBox();
            impersonateDialogBox.EnterName("Balaji Punniyakodi");
            impersonateDialogBox.ClickUser();

            Thread.Sleep(3000);
            Assert.That(backOffice.GetUserInfoAttribute("aria-label"), 
                Is.SupersetOf("Balaji Punniyakodi"), "Balaji Punniyakodi impersonation fail");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
