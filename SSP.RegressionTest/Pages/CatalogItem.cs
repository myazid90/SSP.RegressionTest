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
        public string CatalogItemName => driver.FindElement(By.CssSelector(".sc-sticky-item-header > h1")).Text;
        public string CatalogItemShortDescription => driver.FindElement(By.CssSelector(".sc-sticky-item-header > div")).Text;
        public string CatalogItemDescription {get;set;}
        public int FormFields => driver.FindElements(By.CssSelector("#sc_cat_item\\.do > div")).Count();

        //Private properties
        private IList<IWebElement> catalogItemDescription => driver.FindElements(By.CssSelector(".hidden-sm > div > p"));
        private IWebElement submitButton => driver.FindElement(By.CssSelector("div.text-right.ng-scope > button")); 
        private IList<IWebElement> formCanvas => driver.FindElements(By.CssSelector("#sc_cat_item\\.do > div"));
        private Dictionary<string, SingleFieldClass> formFieldDict { get; set; }

        internal CatalogItem()
        {
            ConcatCatalogItemDescription();

            //Instantiating formFieldDictionary
            formFieldDict = new();

            //Iterate through fields
            for(int i = 0; i < formCanvas.Count; i++)
            {
                IWebElement label = null, value = null;
                string fieldType = string.Empty;

                IList<IWebElement> tempFieldGroup = formCanvas[i].FindElements(By.CssSelector("#sc_cat_item\\.do > div > div > div > div > div > div , #sc_cat_item\\.do > div > div > div > div > div > span"));

                for(int j = 0; j < 3; j++)
                {
                    label = tempFieldGroup[0];
                    value = tempFieldGroup[1];
                    fieldType = fieldTypeDict[driver.FindElement(By.CssSelector($"#sc_cat_item\\.do > div:nth-child({i+1}) > div > div > div > div > span > span , #sc_cat_item\\.do > div > div > div > div > div > span > textarea")).GetAttribute("ng-switch-when")];
                }
                PopulateFieldDictionary(formFieldDict, label.Text, value, fieldType);
            }
        }

        //Public method
        public void PopulateFieldNew(string fieldname, string fieldvalue)
        {
            foreach(var fieldGroup in formFieldDict)
            {
                if (fieldGroup.Key.Contains(fieldname))
                {
                    Console.WriteLine(fieldGroup.Key); 
                    Console.WriteLine(fieldGroup.Value.Label);
                    Console.WriteLine(fieldGroup.Value.FieldType);
                    Console.WriteLine(fieldGroup.Value.Value.Text);
                    fieldGroup.Value.Value.Click();
                    Thread.Sleep(2000);

                    var dropdownboxvalue = driver.FindElements(By.CssSelector("#select2-drop > ul > li"));
                    foreach(var item in dropdownboxvalue)
                    {
                        if(item.Text == fieldvalue)
                        {
                            item.Click();    //can we change the index to be dictionary?
                            break;
                        }
                    }

                    Console.WriteLine($"New value: {fieldGroup.Value.Value.Text}");
                }
            }
        }

        public void Submit()
        {
            //click submit
            submitButton.Click();
        }

        //Private methods
        private void ConcatCatalogItemDescription()
        {
            foreach(var item in catalogItemDescription)
            {
                CatalogItemDescription = string.Concat(CatalogItemDescription, item.Text);
            }
        }

        Dictionary<string, string> fieldTypeDict = new Dictionary<string, string>
        {
            {"reference", "thisisadropdown" },
            {"textarea", "textareayoo" }
        };
        
        private void PopulateFieldDictionary(Dictionary<string, SingleFieldClass> formFieldDict, string label, IWebElement value, string fieldtype)
        {
            SingleFieldClass fieldClass = new SingleFieldClass();

            fieldClass.Label = label;
            fieldClass.FieldType = fieldtype;
            fieldClass.Value = value;

            formFieldDict.Add(key: fieldClass.Label, value: fieldClass);
        }

        internal class SingleFieldClass
        {
            public string Label { get; set; }
            public string FieldType { get; set; }
            public IWebElement Value { get; set; }
        }
    }
}
