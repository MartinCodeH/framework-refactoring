using FluentAssertions;
using Isos.Automation.Common.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Codehouse.Automation.MainSite.PageObjects;

internal class CookieBanner
{
    private readonly IWebDriver _webDriver;

    public CookieBanner(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    private static readonly By _cookieContainerLocator = By.CssSelector("div.cky-consent-container");
    private static readonly By _cookieAcceptButtonLocator = By.CssSelector("div.cky-consent-container button.cky-btn-accept");
    private static readonly By _cookieCloseButtonLocator = By.CssSelector("div.cky-consent-container button.cky-banner-btn-close");

    public void ValidateIsDisplayed()
    {
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3))
                .Until(driver => driver.FindElements(_cookieContainerLocator).Any(e => e.Displayed));
    }

    public void ValidateIsNotDisplayed()
    {
        new Action(ValidateIsDisplayed).Should().Throw<WebDriverTimeoutException>();
    }

    public void Accept()
    {
        _webDriver.Click(_cookieAcceptButtonLocator);
    }

    public void ValidateCorrectCookies()
    {
        var cookieName = "_gid";
        _webDriver.Manage().Cookies.GetCookieNamed(cookieName).Name.Should().Be(cookieName);
    }

    public void ValidateNotCorrectCookies()
    {
        new Action(ValidateCorrectCookies).Should().Throw<WebDriverTimeoutException>();
    }

    public void Close()
    {
        _webDriver.Click(_cookieCloseButtonLocator);
    }
}