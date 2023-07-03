using Isos.Automation.Common.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Codehouse.Automation.MainSite.PageObjects;

internal class Header
{
    private static readonly By LogoLocator = By.CssSelector("a[aria-label='Codehouse logo']");
    private static readonly By LinkLocator = By.CssSelector(".framer-7czh4c > div a");

    private readonly IWebDriver _webDriver;

    public Header(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public bool ValidateLogo()
    {
        try
        {
            new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3))
                .Until(driver => driver.FindElements(LogoLocator).Any(e => e.Displayed));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void ClickLogo()
    {
        _webDriver.Click(LogoLocator);
    }

    public string[] GetLinkNames()
    {
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3))
            .Until(driver => driver.FindElements(LinkLocator).Any(e => e.Displayed));

        _webDriver.FindElements(LinkLocator);
        //var linkCollection = _webDriver.WaitForAndFindElements(LinkLocator, new WebElementQuery
        //{
        //    IsDisplayed = true
        //});
        //return linkCollection.Select(link => link.GetText()).ToArray();
        return new string[3];
    }
}