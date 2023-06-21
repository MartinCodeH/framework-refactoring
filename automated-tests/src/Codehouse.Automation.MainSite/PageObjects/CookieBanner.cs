using Codehouse.Automation.MainSite.Services;
using Codehouse.Automation.MainSite.Support;
using FluentAssertions;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.PageObjects;

internal class CookieBanner
{
    private readonly WebDriverProxy _webDriver;

    public CookieBanner(WebDriverProxy webDriver)
    {
        _webDriver = webDriver;
    }

    private static readonly By CookieContainerLocator = By.CssSelector("div.cky-consent-container");
    private static readonly By CookieAcceptButtonLocator = By.CssSelector("div.cky-consent-container button.cky-btn-accept");
    private static readonly By CookieCloseButtonLocator = By.CssSelector("div.cky-consent-container button.cky-banner-btn-close");


    public void ValidateIsDisplayed()
    {
        _webDriver.WaitForAndFindElement(CookieContainerLocator, new WebElementQuery
        {
            IsDisplayed = true
        });
    }

    public void ValidateIsNotDisplayed()
    {
        _webDriver.WaitForAll(CookieContainerLocator, new WebElementQuery
        {
            IsDisplayed = false
        });
    }

    public void Accept()
    {
        _webDriver.WaitForAndFindElement(CookieAcceptButtonLocator, new WebElementQuery
        {
            IsDisplayed = true,
            IsEnabled = true
        }).Click();
    }

    public void ValidateCorrectCookies()
    {
        _webDriver.ValidateCookieNamed("_gid");
    }

    private void ValidateCorrectCookies(WaitSettings settings)
    {
        _webDriver.ValidateCookieNamed("_gid", settings);
    }

    public void ValidateNotCorrectCookies()
    {
        new Action(() => ValidateCorrectCookies(new WaitSettings { TimeoutOverride = TimeSpan.FromSeconds(5) })).Should().Throw<WebDriverTimeoutException>();
    }

    public void Close()
    {
        _webDriver.WaitForAndFindElement(CookieCloseButtonLocator, new WebElementQuery
        {
            IsDisplayed = true,
            IsEnabled = true
        }).Click();
    }
}