using OpenQA.Selenium;
using SSP.RegressionTest.Helper;
using System.Threading;

namespace SSP.RegressionTest.Modal
{
    class ImpersonateDialogBox : DriverHelper
    {
        IWebElement nameField => driver.FindElement(By.Id("s2id_autogen2"));
        IWebElement userFound => driver.FindElement(By.XPath("/html/body/div[9]/ul/li"));


        public void EnterID(string value)
        {
            nameField.SendKeys(value);
        }

        public void ClickUser()
        {
            Thread.Sleep(1000);
            userFound.Click();
        }
    }
}
