Feature: Customer Details
	In order to provide service to customers
	As a service provider
	I want to be verify customer details

Scenario: Verify customer details for customer (var_0)
    Given I navigate to customer list page
	When I view details for customer "Jeff Bridges"
	Then I expect customer details to be displayed
	| Name         | Email         | Phone         | City       | State              | Country | Organisation | JobProfile         | AdditionalInfo       |
	| Jeff Bridges | abcd@test.com | 0161 225 7632 | Manchester | Greater Manchester | England | Company 2    | Software Developer | Buys Products Rarely |

Scenario: Verify customer details for customer (var_1)
    Given I navigate to customer list page
	When I view details for customer "John Smith"
	Then I expect customer details to be displayed
	| Name       | Email           | Phone      | City   | State          | Country | Organisation | JobProfile         | AdditionalInfo                                                |
	| John Smith | jsmith@test.com | 0208092029 | London | Greater London | England | Company 1    | Software Developer | Has Bought a lot of products before and a high Value Customer |

Scenario: Verify details for customer matches the email and phone from side panel (var_0)
    Given I navigate to customer list page
	When I view details for customer "Jeff Bridges"
	Then I expect the customer email and phone to match between side panel and details panel

Scenario: Verify details for customer matches the email and phone from side panel (var_1)
    Given I navigate to customer list page
	When I view details for customer "John Smith"
	Then I expect the customer email and phone to match between side panel and details panel

Scenario Outline: Verify email and phone number for customers on the side panel
  Given I navigate to customer list page
  Then I verify <Email> and <Phone> for <Customer> name
  Examples:
  | Email                 | Phone         | Customer     |
  | jsmith@test.com       | 0208092029    | John Smith   |
  | abcd@test.com         | 0161 225 7632 | Jeff Bridges |
  | steven.jones@test.com | 01403 215100  | Steve Jones  |

Scenario: Verify no of customers on the customer list page
 Given I navigate to customer list page
 Then I expect "3" customers to be displayed
 | customerName |
 | John Smith   |
 | Jeff Bridges |
 | Steve Jones  |
