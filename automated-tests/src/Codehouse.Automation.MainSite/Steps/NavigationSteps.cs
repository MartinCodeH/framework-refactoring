using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Codehouse.Automation.MainSite.Steps;

[Binding]
internal class NavigationSteps
{
    private readonly IWebDriver _webDriver;

    public NavigationSteps(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    [Given(@"a user is on the (.+) page")]
    [When(@"a user is on the (.+) page")]
    public void GoToPageByAlias(string pageAlias)
    {
        _webDriver.Navigate().GoToUrl(ResolvePageAlias(pageAlias));
    }

    [When(@"they refresh the page")]
    public void WhenTheyRefreshThePage()
    {
        _webDriver.Navigate().Refresh();
    }

    private static string ResolvePageAlias(string pageAlias)
    {
        return pageAlias.ToLowerInvariant() switch
        {
            "home" => "https://www.codehousegroup.com",
            "services" => "https://www.codehousegroup.com/services",
            _ => throw new ArgumentOutOfRangeException(nameof(pageAlias), pageAlias, null)
        };
    }
}