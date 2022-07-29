using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SSP.RegressionTest.Helper;

namespace SSP.RegressionTest.FrontEnd
{
    class SSPHomePage : DriverHelper
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
        [Description("Scenario: Unauthorized/Public user accessing SSP Portal." +
            "Only two modules should be visible, Knowledge Base and Status Page.")]
        public void SHP001_LimitedModules_should_VisibleForUnauthorizedUser()
        {
            //Arrange
            int expectedModulesCount = 2;
            string[] expectedModules = { "Knowledge Base", "Status Page" };

            //Act
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow(false);

            Pages.SSPHomePage ssphomepage = new();

            //Assert
            Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            Assert.That(expectedModules, Is.AnyOf(ssphomepage.Modules), "Incorrect modules name");
        }

        [Test]
        public void SHP002_AllModules_should_VisibleForCTC()
        {
            // Arrange
            int expectedModulesCount = 4;
            string[] expectedModules = { "Knowledge Base", "Status Page", "Support Cases", "Upload Files" };

            //Act
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow("Kirk Wolfe", "kirk.wolfe@westernsouthernlife.com", true);

            Thread.Sleep(3000);
            Pages.SSPHomePage ssphomepage = new();

            //Assert
            Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            Assert.That(expectedModules, Is.AnyOf(ssphomepage.Modules), "Incorrect modules name");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
