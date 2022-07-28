using System;
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
            driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/csm");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(e => e.PageSource.Contains("Self-Service Portal"));
            Pages.SSPHomePage ssphomepage = new();

            //Assert
            Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            Assert.That(expectedModules, Is.AnyOf(ssphomepage.Modules), "Incorrect modules name");
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
