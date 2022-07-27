using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SSP.RegressionTest.Modal;
using SSP.RegressionTest.Helper;

namespace SSP.RegressionTest.BackOffice
{
    class BackOffice : DriverHelper
    {
        [SetUp]
        public void Setup()
        {
            ChromeSettings setting = new();
            ChromeOptions options = new();
            options.AddArgument(setting.Userdatadir);
            options.AddArgument(setting.Userprofile);

            driver = new ChromeDriver(options);
        }

        [Test]
        public void BOM001_Impersonate_CloudOps_should_successful()
        {
            driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/");
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
