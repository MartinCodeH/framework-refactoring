Feature: CookieBannerPopup

Cookie banner should appear when user accesses the site

@regression @prod @cookie-banner
Scenario: The user accesses the homepage
	When a user is on the home page
	Then the cookie banner appears
