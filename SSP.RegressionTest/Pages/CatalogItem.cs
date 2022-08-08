using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSP.RegressionTest.Pages
{
    class CatalogItem : DriverHelper
    {
        //Public properties
        public IWebElement CatalogItemName => driver.FindElement(By.CssSelector(".sc-sticky-item-header > h1"));
        public IWebElement CatalogItemShortDescription => driver.FindElement(By.CssSelector(".sc-sticky-item-header > div"));
        public string CatalogItemDescription {get;set;}

        //Private properties
        private IList<IWebElement> catalogItemDescription => driver.FindElements(By.CssSelector(".hidden-sm > div > p"));
        private IList<IWebElement> formfield => driver.FindElements(By.CssSelector("#sc_cat_item\\.do > div"));
        private IWebElement submitButton => driver.FindElement(By.CssSelector("div.text-right.ng-scope > button"));


        internal CatalogItem()
        {
            ConcatCatalogItemDescription();
        }

        public void PopulateField(string fieldName, string fieldValue)
        {
            //foreach(var item in formfield)
            //{
            //    var x = item.FindElements(By.CssSelector("div > div > div > div > div"));
            //    //IList<IWebElement> tempitem = new List<IWebElement>();
            //    //tempitem.Add(x);
            //    //foreach(var value in x)
            //    //{
            //    //    var label = value.FindElement(By.CssSelector("div"));
            //    //    var inputfield = value.FindElement(By.CssSelector("span"));
            //    //}
            //    var label = x[0].FindElement(By.CssSelector("div"));
            //    var inputfield = x[0].FindElement(By.CssSelector("span"));
            //}

            //lets do the bare basic way
            //timezone field
            var timezonevaluefield = driver.FindElement(By.CssSelector("#timezone > div > span"));
            timezonevaluefield.Click();
            Thread.Sleep(2000);
            //cannot use sendkeys because it is not a text field. //y.SendKeys(fieldValue);
            //dropdown with search box
            var dropdownbox = driver.FindElement(By.CssSelector("#select2-drop"));
            var dropdownboxvalue = dropdownbox.FindElements(By.CssSelector("#select2-drop > ul > li"));
            dropdownboxvalue[1].Click();    //can we change the index to be dictionary?
            Thread.Sleep(1000);
        }

        public void Submit()
        {
            //click submit
            submitButton.Click();
        }

        private void ConcatCatalogItemDescription()
        {
            foreach(var item in catalogItemDescription)
            {
                CatalogItemDescription = string.Concat(CatalogItemDescription, item.Text);
            }
        }
    }
}
