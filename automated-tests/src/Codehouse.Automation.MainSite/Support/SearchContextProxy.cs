using Codehouse.Automation.MainSite.Services;
using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Support;

internal class SearchContextProxy
{
    private readonly ISearchContext _searchContext;
    private readonly WaitService _waitService;
    private readonly WebElementProxyFactory _webElementProxyFactory;

    public SearchContextProxy(
        ISearchContext searchContext,
        WaitService waitService,
        WebElementProxyFactory webElementProxyFactory)
    {
        _searchContext = searchContext;
        _waitService = waitService;
        _webElementProxyFactory = webElementProxyFactory;
    }

    public WebElementProxy WaitForAndFindElement(By locator)
    {
        var element = _waitService.GetWait(_searchContext).Until(sc => sc.FindElement(locator));
        return _webElementProxyFactory.FromWebElement(element);
    }

    public WebElementProxy WaitForAndFindElement(By locator, WebElementQuery query)
    {
        var element = _waitService.GetWait(_searchContext).Until(sc =>
        {
            var foundElement = sc.FindElement(locator);
            return WebElementQueryingService.Perform(foundElement, query)
                ? foundElement
                : null;
        });
        return _webElementProxyFactory.FromWebElement(element);
    }

    public List<WebElementProxy> WaitForAndFindElements(By locator)
    {
        var elements = _waitService.GetWait(_searchContext).Until(sc =>
        {
            var collection = sc.FindElements(locator);
            return collection.Any() ? collection : null;
        });
        return elements.Select(_webElementProxyFactory.FromWebElement).ToList();
    }

    public List<WebElementProxy> WaitForAndFindElements(By locator, WebElementQuery query)
    {
        var elements = _waitService.GetWait(_searchContext).Until(sc =>
        {
            var collection = sc
                .FindElements(locator)
                .Where(e => WebElementQueryingService.Perform(e, query));
            return collection.Any() ? collection : null;
        });
        return elements.Select(_webElementProxyFactory.FromWebElement).ToList();
    }

    public void WaitForAll(By locator, WebElementQuery query)
    {
        _waitService.GetWait(_searchContext).Until(sc =>
        {
            var collection = sc
                .FindElements(locator);
            return collection.All(e => WebElementQueryingService.Perform(e, query));
        });
    }
}