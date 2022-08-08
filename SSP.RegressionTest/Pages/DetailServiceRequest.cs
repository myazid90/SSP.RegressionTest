using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSP.RegressionTest.Pages
{
    class DetailServiceRequest : DriverHelper
    {
        //Public properties
        public string Title { get; set; }

        //Private properties
        private IWebElement srTitle => driver.FindElement(By.CssSelector("div > div.panel-body > dl > dd:nth-child(4) > di"));

        internal DetailServiceRequest()
        {
            //Status = srStatus.Text;
            Title = driver.Title;
        }
    }
}
