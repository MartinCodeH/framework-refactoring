using Codehouse.Automation.MainSite.Services;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Support;

internal class WebElementProxy : SearchContextProxy
{
    private readonly WaitService _waitService;
    private readonly IWebElement _webElement;
    private readonly WebElementProxyFactory _webElementProxyFactory;

    public WebElementProxy(
        IWebElement webElement,
        WaitService waitService,
        WebElementProxyFactory webElementProxyFactory)
        : base(webElement, waitService, webElementProxyFactory)
    {
        _webElement = webElement;
        _waitService = waitService;
        _webElementProxyFactory = webElementProxyFactory;
    }

    public string GetText()
    {
        return _webElement.Text;
    }

    public WebElementProxy Click()
    {
        _webElement.Click();
        return this;
    }
}