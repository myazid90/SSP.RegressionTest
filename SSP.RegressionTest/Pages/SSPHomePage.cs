using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System.Collections.Generic;

namespace SSP.RegressionTest.Pages
{
    class SSPHomePage : DriverHelper
    {
        //Property
        public List<string> Modules { get; set; }
        public string SSPTitle { get; set; }
        private IList<IWebElement> widgetsContainer { get; set; }
        private IList<IWebElement> widgets { get; set; }

        internal SSPHomePage()
        {
            //List variable instantiate
            Modules = new List<string>();
            widgets = new List<IWebElement>();

            //Getting all containers
            widgetsContainer = driver.FindElements(By.CssSelector("body > div > section > main > div.ng-scope"));

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
