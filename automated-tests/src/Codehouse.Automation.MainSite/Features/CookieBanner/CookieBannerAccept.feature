@cookie-banner
Feature: CookieBannerAccept

After user accepts the cookies the banner disappears
and not going to be prompted again after refreshing the page
And receives the correct cookie

@regression @prod
Scenario: After accepting the cookies the banner disappears
	Given a user is on the home page
	When a user accepts the cookies
	Then the cookie banner disappears

@regression @prod
Scenario: After accepting the cookies and reloading the page the banner does not appear
	Given a user is on the home page
	When a user accepts the cookies
	And they refresh the page
	Then the cookie banner does not appear

@regression @prod
Scenario: After accepting the cookies the correct cookies are recieved
	Given a user is on the home page
	When a user accepts the cookies
	Then the correct cookie banner cookies are received
