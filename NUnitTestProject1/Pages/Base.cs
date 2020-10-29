using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTAutomationTest.Pages
{
   public class Base
    {      

        public void GoToUrl(string url)
        {
            Driver.GoTo(url);
        }

        protected void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        protected void WaitForElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

    }
}
