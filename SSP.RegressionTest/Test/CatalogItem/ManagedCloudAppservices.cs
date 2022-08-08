using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SSP.RegressionTest.Helper;

namespace SSP.RegressionTest.CatalogItem
{
    class ManagedCloudAppservices : DriverHelper
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
        [Description("Scenario: An authorized Cloud Customer Contact(CTC) user successfully submit a request. " +
            "Get Detailed Consumption Report catalog item.")]
        public void MCA601_Sales_CTCUser_Submits_GetDetailedConsumptionReport_shouldbe_successfull()
        {
            // Arrange
            string catItem = "Get Detailed Consumption Report";

            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow("Kirk Wolfe", "kirk.wolfe@westernsouthernlife.com", true);
            Thread.Sleep(3000);

            //Act
            Pages.SSPHomePage ssphomepage = new();
            ssphomepage.SelectModule("Create Service Requests");
            Thread.Sleep(2000);
            
            Pages.CreateServiceRequest ritmpage = new();
            ritmpage.SelectCatalog("Sitecore Managed Cloud AppServices");
            Thread.Sleep(3000);

            ritmpage.SelectCategory("Sales");
            Thread.Sleep(3000);

            ritmpage.SelectCatalogItem(catItem);
            Thread.Sleep(3000);

            //goto catalog item page
            Pages.CatalogItem catalogitempage = new();

            //Populate form
            catalogitempage.PopulateField("Timezone", "Asia Pacific");

            //Click Submit
            catalogitempage.Submit();
            Thread.Sleep(2000);

            //goto detail service request page
            Pages.DetailServiceRequest currentsr = new();

            var q = currentsr.Title;

            //Assert
            Assert.That("CSM Service Catalog - Customer Support", Is.EqualTo(currentsr.Title), "Wrong page");
            //Assert.That(expectedModulesCount, Is.EqualTo(ssphomepage.Modules.Count), "Incorrect modules count");
            //Assert.That(expectedModules, Is.AnyOf(ssphomepage.Modules), "Incorrect modules name");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
