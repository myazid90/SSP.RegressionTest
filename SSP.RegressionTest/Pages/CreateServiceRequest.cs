using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSP.RegressionTest.Pages
{
    class CreateServiceRequest : DriverHelper
    {
        //Public properties
        public List<string> Category { get; set; }

        //Private properties
        private IWebElement catalogsWidget => driver.FindElement(By.CssSelector("#catalog_title"));
        private IList<IWebElement> catalogs => driver.FindElements(By.CssSelector("#select2-results-2 > li"));
        private IList<IWebElement> categories => driver.FindElements(By.CssSelector("section > div:nth-child(2) > ul > li"));
        private IList<IWebElement> catalogItemsList => driver.FindElements(By.CssSelector("table > tbody > tr"));

        internal CreateServiceRequest()
        {
            //Category = new List<string>()
            //{
            //    "Account Management",
            //    "Engineering",
            //    "IP Geolocation",
            //    "Sales",
            //    "Sitecore Content Hub",
            //    "Sitecore Email Cloud",
            //    "Sitecore Managed Cloud Appservices",
            //    "Sitecore Managed Cloud Containers"
            //};

            //GetAllCategories()
        }
        
        public void SelectCatalog(string catalogName)
        {
            catalogsWidget.Click();
            FindAndClickThisElement(catalogName, catalogs);
        }

        public void SelectCategory(string categoryName)
        {
            FindAndClickThisElement(categoryName, categories);
        }

        public void SelectCatalogItem(string catalogItemName)
        {
            var elementFound = false;
            foreach (var item in catalogItemsList)
            {
                if (item.Text.Contains(catalogItemName))
                {
                    elementFound = true;
                    FindAndClickThisElement(catalogItemName, item.FindElements(By.CssSelector("td")));
                    break;
                }
            }
            if (!elementFound)
            {
                throw new NotFoundException($"The catalog item: \"{catalogItemName}\", is not found.");
            }
        }

        private void FindAndClickThisElement(string element, IList<IWebElement> elementCollection)
        {
            var elementFound = false;
            foreach (var singleElement in elementCollection)
            {
                if (element == singleElement.Text)
                {
                    singleElement.Click();
                    elementFound = true;
                    break;
                }
            }
            if (!elementFound)
            {
                throw new NotFoundException($"The element: \"{element}\", is not found.");
            }
        }

    }
}
