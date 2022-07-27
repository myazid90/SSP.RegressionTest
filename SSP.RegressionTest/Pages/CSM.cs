using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SSP.RegressionTest.Pages
{
    class CSM : DriverHelper
    {
        public string GetPageSource()
        {
            return driver.PageSource;
        }

        //public string GetWidgets(string attribute)
        //{
        //    //return widget.GetAttribute(attribute);
        //}
    }
}
