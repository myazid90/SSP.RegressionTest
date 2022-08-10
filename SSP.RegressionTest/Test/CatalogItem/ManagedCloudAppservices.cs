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
            string catalog = "Sitecore Managed Cloud AppServices";
            string category = "Sales";
            string catItem = "Get Detailed Consumption Report";

            LoginHelper loginhelper = new();
            loginhelper.LoginToSNow("Kirk Wolfe", "kirk.wolfe@westernsouthernlife.com", true);
            Thread.Sleep(3000);

            //Act
            Pages.SSPHomePage ssphomepage = new();
            ssphomepage.SelectModule("Create Service Requests");
            Thread.Sleep(2000);
            
            Pages.CreateServiceRequest ritmpage = new();
            ritmpage.SelectCatalog(catalog);
            Thread.Sleep(3000);

            ritmpage.SelectCategory(category);
            Thread.Sleep(3000);

            ritmpage.SelectCatalogItem(catItem);
            Thread.Sleep(3000);

            ////Catalog item page
            Pages.CatalogItem catalogitempage = new();
            Thread.Sleep(2000);
            ////Populate form
            catalogitempage.PopulateFieldNew("Timezone", "Asia Pacific");
            
            ////Click Submit
            catalogitempage.Submit();
            Thread.Sleep(2000);

            ////Detail service request page
            Pages.DetailServiceRequest currentsr = new();


            //Assert
            Assert.That("CSM Service Catalog - Customer Support", Is.EqualTo(currentsr.Title), "Wrong page");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
