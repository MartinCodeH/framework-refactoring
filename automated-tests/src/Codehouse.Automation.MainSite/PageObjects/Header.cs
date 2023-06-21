using Codehouse.Automation.MainSite.Support;
using FluentAssertions;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.PageObjects;

internal class Header
{
    private static readonly By LogoLocator = By.CssSelector("a[aria-label='Codehouse logo']");
    private static readonly By LinkLocator = By.CssSelector(".framer-7czh4c > div a");

    private readonly WebDriverProxy _webDriver;

    public Header(WebDriverProxy webDriver)
    {
        _webDriver = webDriver;
    }

    public void ValidateLogo()
    {
        _webDriver.WaitForAndFindElement(LogoLocator, new WebElementQuery
        {
            IsDisplayed = true
        });
    }

    public void ClickLogo()
    {
        _webDriver.WaitForAndFindElement(LogoLocator, new WebElementQuery
        {
            IsDisplayed = true,
            IsEnabled = true
        }).Click();
    }

    public string[] GetLinkNames()
    {
        var linkCollection = _webDriver.WaitForAndFindElements(LinkLocator, new WebElementQuery
        {
            IsDisplayed = true
        });
        return linkCollection.Select(link => link.GetText()).ToArray();
    }
}