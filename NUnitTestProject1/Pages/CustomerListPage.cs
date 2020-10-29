using CTAutomationTest.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CTAutomationTest.Pages
{
   public class CustomerListPage : Base
    {
        public string ClickToViewDetailsLinkLocator = "//h3[text()='{0}']/../..//button";
        public string SidePanelCustomerNameLocator = "//div[@class='addmargin']/div[1]//h3";
        public string SidePanelCustomerDetailsLocator = "//h3[text()='{0}']/../..//p";

        public CustomerListPage()
        {
            WaitForPageLoad();
        } 
        public void ClickToViewDetails(string name)
        {           
            Driver.Instance.FindElement(By.XPath(string.Format(ClickToViewDetailsLinkLocator, name))).Click();
            WaitForElement(By.XPath(string.Format("//div[@class='customerdetails']//h3[text()='{0}']",name)));
            
        }

        public CustomerDetails GetCustomerDetails()
        {
            CustomerDetails details = new CustomerDetails
            {
                Name = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[1]")).Text.Split(':')[1].Trim(),
                Email = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[2]")).Text.Split(':')[1].Trim(),
                Phone = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[3]")).Text.Split(':')[1].Trim(),
                City = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[4]")).Text.Split(':')[1].Trim(),
                State = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[5]")).Text.Split(':')[1].Trim(),
                Country = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[6]")).Text.Split(':')[1].Trim(),
                Organisation = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[7]")).Text.Split(':')[1].Trim(),
                JobProfile = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[8]")).Text.Split(':')[1].Trim(),
                AdditionalInfo = Driver.Instance.FindElement(By.XPath("//div[@class='customerdetails']//p[9]")).Text.Split(':')[1].Trim()
            };

            return details;
        }

        public List<string> GetCustomerNames()
        {          
            List<string> items = new List<string>();
            IReadOnlyCollection<IWebElement> allElements = Driver.Instance.FindElements(By.XPath(SidePanelCustomerNameLocator));
            foreach (var element in allElements)
            {
                items.Add(element.Text.Trim());
            }
            return items;
        }

        public Dictionary<string,string> GetCustomerDetailsFromSidePanel(string name)
        {
            var items = new Dictionary<string,string>();
            var allElements = Driver.Instance.FindElements(By.XPath(string.Format(SidePanelCustomerDetailsLocator,name)));
            items.Add("Email", allElements[0].Text);
            items.Add("Phone", allElements[1].Text);
            return items;
        }

    }
}
