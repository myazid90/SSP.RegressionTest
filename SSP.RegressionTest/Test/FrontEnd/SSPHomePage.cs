using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
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
        [Description("Scenario: Unauthenticated/Public user accessing SSP Portal. " +
            "Only two modules should be visible, Knowledge Base and Status Page.")]
        public void SHP001_LimitedModules_shouldbe_VisibleForUnauthorizedUser()
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
        [Description("Scenario: An authorized Cloud Customer Contact(CTC) user accessing SSP Portal. " +
            "All modules should be visible.")]
        public void SHP002_AllModules_shouldbe_VisibleForCTC()
        {
            // Arrange
            int expectedModulesCount = 8;
            string[] expectedModules = { 
                "Knowledge Base", "Status Page", "Support Cases", "Upload Files",
                "View Service Requests", "Create Service Requests", "Cloud Consumption", "Cloud Availability"};

            //Act
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow("Kirk Wolfe", "kirk.wolfe@westernsouthernlife.com", true);

            Thread.Sleep(3000);
            Pages.SSPHomePage ssphomepage = new();

            //Assert
            Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            Assert.That(expectedModules, Is.AnyOf(ssphomepage.Modules), "Incorrect modules name");
        }

        [Test]
        [Description("Scenario: An authorized External user accessing SSP Portal. " +
            "Cloud Consumption & Cloud Availability modules should NOT be visible.")]
        public void SHP003_LimitedModules_shouldbe_VisibleForExternalUser()
        {
            // Arrange
            int expectedModulesCount = 5;
            string[] expectedModules = {
                "Knowledge Base", "Status Page", "Support Cases", "Upload Files",
                "View Service Requests", "Create Service Requests", "Cloud Consumption", "Cloud Availability"};
            string[] notAvailableModules = {
                "Upload Files", "Cloud Consumption", "Cloud Availability"};

            //Act
            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow(". Ng", "ng.meiling@fujixerox.com", true);

            Thread.Sleep(3000);
            Pages.SSPHomePage ssphomepage = new();

            //Assert
            Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            Assert.That(notAvailableModules, Is.Not.AnyOf(ssphomepage.Modules, "Unauthorized modules are visible"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
