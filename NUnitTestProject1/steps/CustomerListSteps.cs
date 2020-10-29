using CTAutomationTest.Models;
using CTAutomationTest.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CTAutomationTest.steps
{
    [Binding]
    public sealed class CustomerListSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public readonly ScenarioContext _scenarioContext;

        public CustomerListSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to customer list page")]
        public void GivenINavigateToCustomerListPage()
        {
            new CustomerListPage().GoToUrl("https://9a2cdb3e.azurewebsites.net/customerlist");
        }

        [When(@"I view details for customer ""(.*)""")]
        public void GivenIViewDetailsForCustomer(string customerName)
        {
            _scenarioContext["Name"]= customerName;
            new CustomerListPage().ClickToViewDetails(customerName);
        }

        [Then(@"I expect customer details to be displayed")]
        public void ThenIExpectCustomerDetailsToBeDisplayed(Table table)
        {
            var actualDetails = new CustomerListPage().GetCustomerDetails();
            table.CompareToInstance<CustomerDetails>(actualDetails);
        }

        [Then(@"I expect ""(.*)"" customers to be displayed")]
        public void ThenIExpectCustomersToBeDisplayed(int count, Table table)
        {
            var expectedResults = table.Rows.Select(r => r[0]).ToArray();
            var actualResults = new CustomerListPage().GetCustomerNames();
            Assert.That(actualResults.Count==count);
            Assert.That(expectedResults.SequenceEqual(actualResults));
        }


        [Then(@"I verify (.*) and (.*) for (.*) name")]
        public void ThenIVerifyAndForName(string email, string phone, string customerName)
        {
            var sidePanelDetails = new CustomerListPage().GetCustomerDetailsFromSidePanel(customerName);
            Assert.AreEqual(email, sidePanelDetails["Email"]);
            Assert.AreEqual(phone, sidePanelDetails["Phone"]);
        }

        [Then(@"I expect the customer email and phone to match between side panel and details panel")]
        public void ThenIExpectTheCustomerEmailAndPhoneToMatchBetweenSidePanelAndDetailsPanel()
        {
            var sidePanelDetails = new CustomerListPage().GetCustomerDetailsFromSidePanel(_scenarioContext["Name"].ToString());
            var customerDetails = new CustomerListPage().GetCustomerDetails();
            Assert.AreEqual(sidePanelDetails["Email"],customerDetails.Email);
            Assert.AreEqual(sidePanelDetails["Phone"],customerDetails.Phone);

        }


    }
}
