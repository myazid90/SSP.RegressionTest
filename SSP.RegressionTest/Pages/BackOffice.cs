using OpenQA.Selenium;
using SSP.RegressionTest.Helper;

namespace SSP.RegressionTest.Pages
{
    class BackOffice : DriverHelper
    {
        IWebElement userInfo => driver.FindElement(By.XPath("//*[@id=\"user_info_dropdown\"]"));

        IWebElement impersonateOption => driver.FindElement(By.XPath("//*[@id=\"glide_ui_impersonator\"]"));

        public void ClickUserInfo() => userInfo.Click();

        public void ClickImpersonate() => impersonateOption.Click();

        public string GetUserInfoAttribute(string attribute)
        {
            return userInfo.GetAttribute(attribute);
        }
    }
}