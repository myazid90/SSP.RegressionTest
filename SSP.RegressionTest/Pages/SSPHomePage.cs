using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System;
using System.Collections.Generic;

namespace SSP.RegressionTest.Pages
{
    class SSPHomePage : DriverHelper
    {
        //Public properties
        public List<string> Modules { get; set; }
        public string SSPTitle { get; set; }

        //Private properties
        private IList<IWebElement> widgetsContainer => driver.FindElements(By.CssSelector("body > div > section > main > div.ng-scope"));
        private IList<IWebElement> widgets { get; set; }

        internal SSPHomePage()
        {
            //List variable instantiate
            Modules = new List<string>();
            widgets = new List<IWebElement>();


            //Getting widget from only 2&3 contatiner rows
            for (int i = 2; i < widgetsContainer.Count; i++)
            {
                foreach(var widget in widgetsContainer[i].FindElements(By.CssSelector("div > sp-page-row > div > div")))
                {
                    widgets.Add(widget);
                }
            }

            GetSSPHomePageTitle();
            GetWidgetName();
            
        }

        public void SelectModule(string moduleName)
        {
            var elementfound = false;
            foreach(var widget in widgets)
            {
                if (moduleName == widget.Text)
                {
                    widget.Click();
                    elementfound = true;
                    break;
                }
            }
            if (!elementfound)
            {
                throw new Exception("Widget not found");
            }
        }

        private void GetSSPHomePageTitle()
        {
            for (int i = 0; i < widgetsContainer.Count; i++)
            {
                if (i == 0)
                {
                    SSPTitle = widgetsContainer[i].Text;
                }
            }
        }

        private void GetWidgetName()
        {
            foreach (var widget in widgets)
            {
                if (widget.Displayed == true)
                {
                    Modules.Add(widget.Text);
                }
            }
        }
    }
}
