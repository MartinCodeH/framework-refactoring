@cookie-banner
Feature: CookieBannerCustomize

A user can customize the cookie preferences

@regression @prod
Scenario: After accepting the cookies the banner disappears
	Given a user is on the home page
	When a user accepts the cookies
	Then the cookie banner disappears