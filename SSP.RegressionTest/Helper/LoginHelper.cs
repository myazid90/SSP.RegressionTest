using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SSP.RegressionTest.Modal;
using System;
using System.Threading;

namespace SSP.RegressionTest.Helper
{
    class LoginHelper : DriverHelper
    {
        public void LoginToSNow(bool admin = true)
        {
            if (admin)
            {
                LoginAsAdmin();
            }
            else
            {
                //public user
                driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/csm");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(e => e.PageSource.Contains("Self-Service Portal"));
            }
        }

        public void LoginToSNow(string impersonateeName, string impersonateeUserID, bool redirectToSSPHomePage = false)
        {
            LoginAsAdmin();
            LoginAsImpersonatee(impersonateeUserID);

            if (redirectToSSPHomePage)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(e => e.PageSource.Contains("Self-Service Portal"));
            }
            else
            {
                Pages.BackOffice backOffice = new Pages.BackOffice();
                Thread.Sleep(3000);
                Assert.That(backOffice.GetUserInfoAttribute("aria-label"),
                    Is.SupersetOf(impersonateeName), $"{impersonateeName} impersonation fail");
            }
        }

        private void LoginAsAdmin()
        {
            driver.Navigate().GoToUrl("https://sitecoredev.service-now.com/");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(e => e.Url.Contains("dashboard"));
        }

        private void LoginAsImpersonatee(string impersonateeUserID)
        {
            Pages.BackOffice backOffice = new Pages.BackOffice();
            backOffice.ClickUserInfo();
            backOffice.ClickImpersonate();

            ImpersonateDialogBox impersonateDialogBox = new ImpersonateDialogBox();
            impersonateDialogBox.EnterID(impersonateeUserID);
            Thread.Sleep(1000);
            impersonateDialogBox.ClickUser();
        }
    }
}
