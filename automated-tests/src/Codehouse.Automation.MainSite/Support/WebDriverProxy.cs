using Codehouse.Automation.MainSite.Services;
using FluentAssertions;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Support;

internal class WebDriverProxy : SearchContextProxy
{
    private readonly WaitService _waitService;
    private readonly IWebDriver _webDriver;
    private readonly WebElementProxyFactory _webElementProxyFactory;

    public WebDriverProxy(
        IWebDriver webDriver,
        WaitService waitService,
        WebElementProxyFactory webElementProxyFactory)
        : base(webDriver, waitService, webElementProxyFactory)
    {
        _webDriver = webDriver;
        _waitService = waitService;
        _webElementProxyFactory = webElementProxyFactory;
    }

    public void ValidateCookieNamed(string name)
    {
        var cookie = _waitService.GetWait(_webDriver).Until(driver => driver.Manage().Cookies.GetCookieNamed(name));
        cookie.Name.Should().Be(name);
    }

    public void ValidateCookieNamed(string name, WaitSettings settings)
    {
        var cookie = _waitService.GetWait(_webDriver, settings).Until(driver => driver.Manage().Cookies.GetCookieNamed(name));
        cookie.Name.Should().Be(name);
    }

    public Screenshot TakeScreenShot()
    {
        return ((ITakesScreenshot)_webDriver).GetScreenshot();
    }
   
}