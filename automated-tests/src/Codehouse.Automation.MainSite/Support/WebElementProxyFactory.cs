using Codehouse.Automation.MainSite.Services;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Support;

internal class WebElementProxyFactory
{
    private readonly WaitService _waitService;

    public WebElementProxyFactory(WaitService waitService)
    {
        _waitService = waitService;
    }

    public WebElementProxy FromWebElement(IWebElement webElement)
    {
        return new WebElementProxy(webElement, _waitService, this);
    }
}