@header
Feature: HeaderNavigation

	Verify header navigation on homepage

@prod
Scenario Outline: a user is on the homepage and can see the correct Header link
	Given a user is on the home page
    Then the <linkName> link is displayed on the header
	Examples: 
	| linkName |
	| services |
	| work     |
	| Insights |
	| Contact  |


