Feature: CookieBannerDecline

After a user closes the cookie banner, the cookie banner disappears
And the user does not receive the cookie
If the user refreshes the page the cookie banner should reappear

@regression @prod @cookie-banner
Scenario: After closing the cookies the banner disappears
	Given a user is on the home page
	When a user closes the cookies
	Then the cookie banner disappears

@regression @prod @cookie-banner
Scenario: After closing the cookies and reloading the page the banner does not appear
	Given a user is on the home page
	When a user closes the cookies
	And they refresh the page
	Then the cookie banner does not appear

@regression @prod @cookie-banner
Scenario: After closing the cookies the correct cookies are not recieved
	Given a user is on the home page
	When a user closes the cookies
	Then the correct cookie banner cookies are not received
