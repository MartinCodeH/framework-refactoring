using OpenQA.Selenium;

namespace Isos.Automation.Common.Extensions;

public static partial class WebDriverExtensions
{
    /// <summary>
    ///     Scroll down to the selected element with additional changes
    ///     to the Y Position to make sure the variable is on screen
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="locator"></param>
    /// <param name="yScrollUpBy"></param>
    public static void ScrollDownToElementByLocator(this IWebDriver webDriver, By locator, int yScrollUpBy)
    {
        WaitForElementToBeClickable(webDriver, locator);
        var jsDriver = (IJavaScriptExecutor)webDriver;
        var selectedFieldId = webDriver.FindElement(locator);
        var xPosId = selectedFieldId.Location.X;
        var yPosId = selectedFieldId.Location.Y - yScrollUpBy;
        jsDriver.ExecuteScript($"window.scroll({xPosId}, {yPosId})");
    }

    /// <summary>
    ///     Scroll down to the selected element with additional changes
    ///     to the Y Position to make sure the variable is on screen
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="locator"></param>
    /// <param name="yScrollUpBy"></param>
    public static void ScrollDownToElementWebElement(this IWebDriver webDriver, IWebElement element, int yScrollUpBy)
    {
        WaitForElementToBeClickableByWebElement(webDriver, element);
        var jsDriver = (IJavaScriptExecutor)webDriver;
        var selectedFieldId = element;
        var xPosId = selectedFieldId.Location.X;
        var yPosId = selectedFieldId.Location.Y - yScrollUpBy;
        jsDriver.ExecuteScript($"window.scroll({xPosId}, {yPosId})");
    }

    /// <summary>
    ///     Scroll to the bottom of the page
    /// </summary>
    /// <param name="webDriver"></param>
    public static void ScrollToBottom(this IWebDriver webDriver)
    {
        var jsDriver = (IJavaScriptExecutor)webDriver;
        jsDriver.ExecuteScript("window.scroll(0, document.body.scrollHeight)");
    }

    /// <summary>
    ///     Scrolling to a specified element position
    ///     from a collection of web elements
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="element"></param>
    public static void ScrollToElementWebElement(this IWebDriver webDriver, IWebElement element)
    {
        var jsDriver = (IJavaScriptExecutor)webDriver;
        jsDriver.ExecuteScript("arguments[0].scrollIntoView(false)", element);
    }

    /// <summary>
    ///     Scroll to the middle of the page
    /// </summary>
    /// <param name="webDriver"></param>
    public static void ScrollToMiddle(this IWebDriver webDriver)
    {
        var jsDriver = (IJavaScriptExecutor)webDriver;
        jsDriver.ExecuteScript("window.scroll(0,document.body.scrollHeight/2)");
    }

    /// <summary>
    ///     Scroll to the top of the page
    /// </summary>
    /// <param name="webDriver"></param>
    public static void ScrollToTop(this IWebDriver webDriver)
    {
        var jsDriver = (IJavaScriptExecutor)webDriver;
        jsDriver.ExecuteScript("window.scroll(0, 0)");
    }
}