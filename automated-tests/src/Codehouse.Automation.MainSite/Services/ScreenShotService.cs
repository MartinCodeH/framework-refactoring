using Codehouse.Automation.MainSite.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Services;

internal class ScreenShotService
{
    private readonly IConfiguration _configuration;
    private readonly IWebDriver _webDriver;
    
    public ScreenShotService(IWebDriver webDriver, IConfiguration configuration)
    {
        _webDriver = webDriver;
        _configuration = configuration;
    }

    public void Screenshot()
    {
        var screenShotOptions = _configuration.GetSection(ScreenshotOptions.SectionName).Get<ScreenshotOptions>() ??
                                new ScreenshotOptions();
        var screenShot = ((ITakesScreenshot)_webDriver).GetScreenshot();
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