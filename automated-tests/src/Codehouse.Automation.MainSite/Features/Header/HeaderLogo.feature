@header
Feature: HeaderLogo

	The logo should be displayed on the header

@prod
Scenario: a user is on the homepage and can see the header logo displayed
	Given a user is on the home page
	Then the logo should be displayed in the header
