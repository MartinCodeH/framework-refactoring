using BoDi;
using Codehouse.Automation.MainSite.Services;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Codehouse.Automation.MainSite.Hooks;

[Binding]
internal class ScenarioHooks
{
    [BeforeScenario(Order = 0)]
    public static void RegisterConfiguration(IObjectContainer container)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        container.RegisterInstanceAs<IConfiguration>(configuration);
    }

    [BeforeScenario(Order = 1000)]
    public static void BeforeScenario(
        IObjectContainer container,
        WebDriverService webDriverService)
    {
        container.RegisterInstanceAs(webDriverService.NewWebDriver());
    }

    [AfterScenario(Order = 0)]

    public static void TakeScreenshot(ScreenShotService service, ScenarioContext scenarioContext)
    {
        if (scenarioContext.TestError is not null)
        {
            service.Screenshot();
        }
    }

    [AfterScenario(Order = 11000)]
    public static void AfterScenario(IObjectContainer container)
    {
        try
        {
            container.Resolve<IWebDriver>().Quit();
        }
        catch
        {
            // ignored
        }
    }
}