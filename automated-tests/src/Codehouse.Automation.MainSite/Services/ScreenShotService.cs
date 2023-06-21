using Codehouse.Automation.MainSite.Configuration;
using Codehouse.Automation.MainSite.Support;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Services;

internal class ScreenShotService
{
    private readonly IConfiguration _configuration;
    private readonly WebDriverProxy _webDriver;

    //take a screenshot and save to a file log the path of the file 
    public ScreenShotService(WebDriverProxy webDriver, IConfiguration configuration)
    {
        _webDriver = webDriver;
        _configuration = configuration;
    }

    public void Screenshot()
    {
        var screenShotOptions = _configuration.GetSection(ScreenshotOptions.SectionName).Get<ScreenshotOptions>() ??
                                new ScreenshotOptions();
        var screenShot = _webDriver.TakeScreenShot();
        var assemblyDirectory = Directory.GetCurrentDirectory();
        var screenshotDirectory = Path.Combine(assemblyDirectory, screenShotOptions.Directory);
        if (!Path.Exists(screenshotDirectory))
        {
            Directory.CreateDirectory(screenshotDirectory);
        }
        var screenshotFile = Path.Combine(screenshotDirectory, $"{Guid.NewGuid():D}.png");
        screenShot.SaveAsFile(screenshotFile, ScreenshotImageFormat.Png);
    }
}