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
        [Ignore("Shoule not be running as a test")]
        public void BOF001_Admin_login_shouldbe_successful()
        {
            //Arrange
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow();

            //Act
            Pages.BackOffice backOffice = new Pages.BackOffice();

            //Assert
            Assert.That(backOffice.GetUserInfoAttribute("aria-label"),
                Is.SupersetOf("Yazid Tahir"), "Yazid Tahir impersonation fail");
        }

        [Test]
        public void BOF002_Impersonate_CloudOps_shouldbe_successful()
        {
            //Arrange
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow("Balaji Punniyakodi","bal@sitecore.net");

            //Act
            Pages.BackOffice backOffice = new Pages.BackOffice();

            //Assert
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
