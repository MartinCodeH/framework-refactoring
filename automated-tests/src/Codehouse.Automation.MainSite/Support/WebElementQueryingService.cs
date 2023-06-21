using OpenQA.Selenium;

namespace Codehouse.Automation.MainSite.Support;

public class WebElementQueryingService
{
    public static bool Perform(IWebElement element, WebElementQuery query)
    {
        if (query.IsDisplayed is not null && !query.IsDisplayed.Equals(element.Displayed))
        {
            return false;
        }

        if (query.IsEnabled is not null && !query.IsEnabled.Equals(element.Enabled))
        {
            return false;
        }

        return true;
    }
}