using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System.Collections.Generic;

namespace SSP.RegressionTest.Pages
{
    class SSPHomePage : DriverHelper
    {
        //Property
        public List<string> Modules { get; set; }
        private IList<IWebElement> widgetContainer { get; set; }

        internal SSPHomePage()
        {
            //List variable instantiate
            Modules = new List<string>();

            //Getting widgets from the first row
            widgetContainer = driver.FindElements(By.CssSelector("div.ng-scope.m-t-n > div > sp-page-row > div > div"));
            //Getting widgets from the second row here
            //

            GetWidgetName();
        }

        private void GetWidgetName()
        {
            foreach (var widget in widgetContainer)
            {
                if (widget.Displayed == true)
                {
                    Modules.Add(widget.Text);
                }
            }
        }
    }
}
