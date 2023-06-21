using Codehouse.Automation.MainSite.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Codehouse.Automation.MainSite.Services;

internal class WebDriverService
{
    private readonly IConfiguration _configuration;

    public WebDriverService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IWebDriver NewWebDriver()
    {
        var options = _configuration.GetSection(WebDriverOptions.SectionName).Get<WebDriverOptions>() ??
                      new WebDriverOptions();
        IWebDriver webDriver;
        switch (options.Type.ToLowerInvariant())
        {
            case "chrome":
                var chromeOptions = new ChromeOptions();
                if (options.Headless)
                {
                    chromeOptions.AddArguments("headless=new");
                }

                webDriver = new ChromeDriver(chromeOptions);
                break;
            case "edge":
                var edgeOptions = new EdgeOptions();
                if (options.Headless)
                {
                    edgeOptions.AddArguments("headless=new");
                }

                webDriver = new EdgeDriver(edgeOptions);
                break;
            case "firefox":
                var firefoxOptions = new FirefoxOptions();
                if (options.Headless)
                {
                    firefoxOptions.AddArguments("--headless");
                }

                webDriver = new FirefoxDriver(firefoxOptions);
                break;
            default:
                throw new InvalidOperationException();
        }

        webDriver.Manage().Window.Maximize();
        return webDriver;
    }
}