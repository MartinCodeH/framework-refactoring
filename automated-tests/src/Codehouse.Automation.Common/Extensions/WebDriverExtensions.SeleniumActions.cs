using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using Codehouse.Automation.Common;
using SeleniumExtras.WaitHelpers;

namespace Isos.Automation.Common.Extensions;

public static partial class WebDriverExtensions
{
    //private static readonly int DefaultTimeout = AppSettings.Root.GetValue<int>("DefaultTimeout");

    /// <summary>
    ///     Explicit Wait - Wait X Seconds
    ///     Used only when the explicit wait methods are not applicable to specific elements
    /// </summary>
    /// <param name="seconds">The duration of the explicit wait in seconds</param>
    public static void BrowserSleep(this IWebDriver webDriver, int seconds)
    {
        Thread.Sleep(seconds * 1000);
    }

    /// <summary>
    ///     For the selected input field clear the text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void ClearKeys(this IWebDriver webDriver, By locator)
    {
        ClearKeys(webDriver, locator, null);
    }

    /// <summary>
    ///     For the selected input field clear the text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void ClearKeys(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        WaitForElementToBeClickable(webDriver, locator, GetTimeout(timeoutOverride));
        var inputFieldId = webDriver.FindElement(locator);
        inputFieldId.Clear();
    }

    /// <summary>
    ///     Wait for the element state to be clickable before triggering click action
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void Click(this IWebDriver webDriver, By locator)
    {
        Click(webDriver, locator, null);
    }

    /// <summary>
    ///     Wait for the element state to be clickable before triggering click action
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void Click(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        WaitForElementToBeClickable(webDriver, locator, GetTimeout(timeoutOverride));
        var clickButton = webDriver.FindElement(locator);
        clickButton.Click();
    }

    /// <summary>
    ///     Wait for the IWeb element to be clickable before triggering click action
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    public static void ClickByWebElement(this IWebDriver webDriver, IWebElement element)
    {
        ClickByWebElement(webDriver, element, null);
    }

    /// <summary>
    ///     Wait for the IWeb element to be clickable before triggering click action
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void ClickByWebElement(this IWebDriver webDriver, IWebElement element, int? timeoutOverride)
    {
        WaitForElementToBeClickableByWebElement(webDriver, element, GetTimeout(timeoutOverride));
        element.Click();
    }

    /// <summary>
    ///     This method selects element and performs drag and drop
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="sourceLocator">The source element to drag</param>
    /// <param name="destinationLocator">The destination element to be dropped</param>
    public static void DragAndDrop(this IWebDriver webDriver, By sourceLocator, By destinationLocator)
    {
        var builder = new Actions(webDriver);
        var dragAndDrop = builder.ClickAndHold(webDriver.FindElement(sourceLocator)).MoveToElement(webDriver.FindElement(destinationLocator)).Release(webDriver.FindElement(sourceLocator)).Build();
        dragAndDrop.Perform();
    }

    /// <summary>
    ///     For the current window opened, redirect to a new url.
    /// </summary>
    /// <param name="url">Redirect the web driver to the specified url</param>
    /// <param name="webDriver">Current webdriver in use</param>
    public static void GoToPage(this IWebDriver webDriver, string url)
    {
        webDriver.Navigate().GoToUrl(url);
    }

    /// <summary>
    ///     This method performs mouse hover over
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">Element to hover over/param>
    public static void MouseHoverElement(this IWebDriver webDriver, By locator)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).Build().Perform();
    }

    /// <summary>
    ///     This method performs mouse hover over and gets specified attribute value
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator"> Element to hover over</param>
    /// <param name="attributeValue">Attribute value of the element/param>
    public static string MouseHoverElementGetAttribute(this IWebDriver webDriver, By locator, string attributeValue)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).Build().Perform();
        return webDriver.FindElement(locator).GetAttribute(attributeValue);
    }

    /// <summary>
    ///     This method performs mouse hover over
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">Element to hover over</param>
    public static void MouseHoverWebElement(this IWebDriver webDriver, IWebElement element)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(element).Build().Perform();
    }

    public static void PseudoElementClick(this IWebDriver webDriver, By locator)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).Click().Build().Perform();
    }

    /// <summary>
    ///     This method helps to simulate tab key press
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pseudo element locator</param>
    public static void PseudoElementEnterTab(this IWebDriver webDriver, By locator)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).SendKeys(Keys.Tab).Build().Perform();
    }

    /// <summary>
    ///     This method helps extract text of a pseudo element
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pseudo element locator</param>
    public static string PseudoElementGetPropertyValue(this IWebDriver webDriver, By locator, string propertyValue)
    {
        return webDriver.FindElement(locator).GetCssValue(propertyValue);
    }

    /// <summary>
    ///     This method helps extract text of a pesudo element
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pesudo element locator</param>
    public static string PseudoElementGetText(this IWebDriver webDriver, By locator)
    {
        return webDriver.FindElement(locator).GetAttribute("value");
    }

    /// <summary>
    ///     This method helps extract css property value of the element with after pseudo tag
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="querySelector">The css value of the pseudo element</param>
    /// <param name="propertyValue">The style sheet property value</param>
    public static string PseudoElementProteryValueAfterTag(this IWebDriver webDriver, string querySelector, string propertyValue)
    {
        var scriptColor = "return window.getComputedStyle(document.querySelector('" + querySelector + "'),':after').getPropertyValue('" + propertyValue + "')";
        var jsColor = (IJavaScriptExecutor)webDriver;
        return (string)jsColor.ExecuteScript(scriptColor);
    }

    /// <summary>
    ///     This method helps extract css property value of the pseudo element before pesudo tag
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="querySelector">The css value of the pseudo element</param>
    /// <param name="propertyValue">The style sheet property value</param>
    public static string PseudoElementProteryValueBeforeTag(this IWebDriver webDriver, string querySelector, string propertyValue)
    {
        var scriptColor = "return window.getComputedStyle(document.querySelector('" + querySelector + "'),':before').getPropertyValue('" + propertyValue + "')";
        var jsColor = (IJavaScriptExecutor)webDriver;
        return (string)jsColor.ExecuteScript(scriptColor);
    }

    /// <summary>
    ///     This method types specified text on a Pseudo Element with before and after tags
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pseudo element locator</param>
    /// <param name="textToType">The pseudo element locator</param>
    public static void PseudoElementSendKeys(this IWebDriver webDriver, By locator, string textToType)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).SendKeys(textToType).Build().Perform();
    }

    /// <summary>
    ///     This method helps to simulate enter key presson an pseudo element
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pseudo element locator</param>
    public static void PseudoElementSimulateEnterKey(this IWebDriver webDriver, By locator)
    {
        var action = new Actions(webDriver);
        action.MoveToElement(webDriver.FindElement(locator)).SendKeys(Keys.Return).Build().Perform();
    }

    /// <summary>
    ///     This method helps to simulate multiple tab key press
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The pseudo element locator</param>
    public static void PseudoElementSimulateTabKey(this IWebDriver webDriver, By locator, int tabCount)
    {
        var action = new Actions(webDriver);
        for (var i = 1; i < tabCount; i++)
        {
            action.MoveToElement(webDriver.FindElement(locator)).SendKeys(Keys.Tab).Build().Perform();
        }
    }

    /// <summary>
    ///     Overloaded method for SafeClickByLocator
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="locator"></param>
    public static void SafeClickByLocator(this IWebDriver webDriver, By locator)
    {
        SafeClickByLocator(webDriver, locator, null);
    }

    /// <summary>
    ///     For the selected input field clear the text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SafeClickByLocator(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GetTimeout(timeoutOverride)));
        try
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }
        catch
        (
            ElementNotInteractableException ex)
        {
            Console.Write(ex.InnerException);
        }
    }

    /// <summary>
    ///     Overloaded method for SafeClickByWebElement
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="element"></param>
    public static void SafeClickByWebElement(this IWebDriver webDriver, IWebElement element)
    {
        SafeClickByWebElement(webDriver, element, null);
    }

    /// <summary>
    ///     For the selected input field clear the text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SafeClickByWebElement(this IWebDriver webDriver, IWebElement element, int? timeoutOverride)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GetTimeout(timeoutOverride)));
        try
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
        }
        catch (ElementNotInteractableException ex)
        {
            Console.Write(ex.InnerException);
        }
    }

    /// <summary>
    ///     This method selects the specified string value from a drop down
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The drop down element locator</param>
    /// <param name="indexValue">The index value of the item to select</param>
    public static void SelectDropDownIndex(this IWebDriver webDriver, By locator, int indexValue)
    {
        //var selectElement = new SelectElement(webDriver.FindElement(locator));
        //.SelectByIndex(indexValue);
    }

    /// <summary>
    ///     This method selects the specified string value from a drop down
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The drop down element locator</param>
    /// <param name="valueToSelect">The string value to select</param>
    public static void SelectDropDownValue(this IWebDriver webDriver, By locator, string valueToSelect)
    {
        //var selectElement = new SelectElement(webDriver.FindElement(locator));
        //selectElement.SelectByText(valueToSelect);
    }

    /// <summary>
    ///     Wait for the dropdown element ot be available.
    ///     Once available click the selected dropdown item.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="dropDown">Waiting for the section to be available</param>
    /// <param name="dropDownList">Listed items in the dropwdown</param>
    /// <param name="selectedInput">The selected item from the list you want to choose</param>
    public static void SelectItemFromDropdown(this IWebDriver webDriver, By dropDown, By dropDownList, string selectedInput)
    {
        SelectItemFromDropdown(webDriver, dropDown, dropDownList, selectedInput, null);
    }

    /// <summary>
    ///     Wait for the dropdown element ot be available.
    ///     Once available click the selected dropdown item.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="dropDown">Waiting for the section to be available</param>
    /// <param name="dropDownList">Listed items in the dropwdown</param>
    /// <param name="selectedInput">The selected item from the list you want to choose</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SelectItemFromDropdown(this IWebDriver webDriver, By dropDown, By dropDownList, string selectedInput, int? timeoutOverride)
    {
        WaitForElementToBeClickable(webDriver, dropDown);
        var listedItem = webDriver.FindElements(dropDownList);

        foreach (var selectedItem in listedItem)
        {
            if (selectedItem.Text == selectedInput)
            {
                WaitForElementToBeClickableByWebElement(webDriver, selectedItem, GetTimeout(timeoutOverride));
                selectedItem.Click();
            }
        }
    }

    /// <summary>
    ///     For the selected input field populate input field with selected text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="sendValues">Specified text that will be parsed into the input field</param>
    public static void SendKeys(this IWebDriver webDriver, By locator, string sendValues)
    {
        SendKeys(webDriver, locator, sendValues, null);
    }

    /// <summary>
    ///     For the selected input field populate input field with selected text
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="sendValues">Specified text that will be parsed into the input field</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SendKeys(this IWebDriver webDriver, By locator, string sendValues, int? timeoutOverride)
    {
        WaitForElementToBeClickable(webDriver, locator, GetTimeout(timeoutOverride));
        var inputFieldId = webDriver.FindElement(locator);
        inputFieldId.SendKeys(sendValues);
    }

    /// <summary>
    ///     Similar to the "WaitForElementToBeClickable" method.
    ///     This is used for Sending value to an element
    ///     from a collection to be in a "clickable" state
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    /// <param name="sendValue">Specified text that will be parsed into the input field</param>
    public static void SendKeysByWebElement(this IWebDriver webDriver, IWebElement element, string sendValue)
    {
        SendKeysByWebElement(webDriver, element, sendValue, null);
    }

    /// <summary>
    ///     Similar to the "WaitForElementToBeClickable" method.
    ///     This is used for Sending value to an element
    ///     from a collection to be in a "clickable" state
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    /// <param name="sendValue">Specified text that will be parsed into the input field</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SendKeysByWebElement(this IWebDriver webDriver, IWebElement element, string sendValue, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.ElementToBeClickable(element));
        var inputFieldId = element;
        inputFieldId.SendKeys(sendValue);
    }

    /// <summary>
    ///     Wait for specified iFrame to be available in the DOM and then switch to it
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="selectedIFrame">The selected IFrame within a web page</param>
    public static void SwitchIFrame(this IWebDriver webDriver, string selectedIFrame)
    {
        SwitchIFrame(webDriver, selectedIFrame, null);
    }

    /// <summary>
    ///     Wait for specified iFrame to be available in the DOM and then switch to it
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="selectedIFrame">The selected IFrame within a web page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void SwitchIFrame(this IWebDriver webDriver, string selectedIFrame, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(selectedIFrame));
    }

    /// <summary>
    ///     Validate the specified metadata string of the current web page
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="selectedMetaData">Specified metadata tag to search</param>
    /// <param name="metaDataValue">Validation string of the selected meta data</param>
    /// <returns></returns>
    public static bool ValidateMetaData(this IWebDriver webDriver, string selectedMetaData, string metaDataValue)
    {
        return ValidateMetaData(webDriver, selectedMetaData, metaDataValue, null);
    }

    /// <summary>
    ///     Validate the specified metadata string of the current web page
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="selectedMetaData">Specified metadata tag to search</param>
    /// <param name="metaDataValue">Validation string of the selected meta data</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    /// <returns></returns>
    public static bool ValidateMetaData(this IWebDriver webDriver, string selectedMetaData, string metaDataValue, int? timeoutOverride)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GetTimeout(timeoutOverride)));
        Func<IWebDriver, bool> waitForElement = Web =>
        {
            if (webDriver.PageSource != "")
            {
                return true;
            }

            return false;
        };
        wait.Until(waitForElement);

        return selectedMetaData switch
        {
            "description" => webDriver.FindElement(By.XPath("//meta[@name='description']")).GetAttribute("content").ToLowerInvariant().Contains(metaDataValue.ToLowerInvariant()),
            "keywords" => webDriver.FindElement(By.XPath("//meta[@name='keywords']")).GetAttribute("content").ToLowerInvariant().Contains(metaDataValue.ToLowerInvariant()),
            _ => false
        };
    }

    /// <summary>
    ///     Wait for the selected element to contain the following string value
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="selectedText">The specified text within a web element</param>
    public static void WaitForElementTextToBePresent(this IWebDriver webDriver, By locator, string selectedText)
    {
        WaitForElementTextToBePresent(webDriver, locator, selectedText, null);
    }

    /// <summary>
    ///     Wait for the selected element to contain the following string value
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="selectedText">The specified text within a web element</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementTextToBePresent(this IWebDriver webDriver, By locator, string selectedText, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.TextToBePresentInElement(webDriver.FindElement(locator), selectedText));
    }

    /// <summary>
    ///     Wait for the selected element to be in a "clickable"
    ///     state to stop race condition with the web driver.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void WaitForElementToBeClickable(this IWebDriver webDriver, By locator)
    {
        WaitForElementToBeClickable(webDriver, locator, null);
    }

    /// <summary>
    ///     Wait for the selected element to be in a "clickable"
    ///     state to stop race condition with the web driver.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementToBeClickable(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }

    /// <summary>
    ///     Similar to the "WaitForElementToBeClickable" method.
    ///     This is used for waiting for a element
    ///     from a collection to be in a "clickable" state
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    public static void WaitForElementToBeClickableByWebElement(this IWebDriver webDriver, IWebElement element)
    {
        WaitForElementToBeClickableByWebElement(webDriver, element, null);
    }

    /// <summary>
    ///     Similar to the "WaitForElementToBeClickable" method.
    ///     This is used for waiting for a element
    ///     from a collection to be in a "clickable" state
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementToBeClickableByWebElement(this IWebDriver webDriver, IWebElement element, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.ElementToBeClickable(element));
    }

    /// <summary>
    ///     Wait for the selected element to not be displayed within the DOM
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void WaitForElementToBeInvisible(this IWebDriver webDriver, By locator)
    {
        WaitForElementToBeInvisible(webDriver, locator, null);
    }

    /// <summary>
    ///     Wait for the selected element to not be displayed within the DOM
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementToBeInvisible(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
    }

    /// <summary>
    ///     Wait for the selected element to be displayed in the DOM.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void WaitForElementToBePresent(this IWebDriver webDriver, By locator)
    {
        WaitForElementToBePresent(webDriver, locator, null);
    }

    /// <summary>
    ///     Wait for the selected element to be displayed in the DOM.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementToBePresent(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.ElementExists(locator));
    }

    /// <summary>
    ///     Wait for the selected element "display" state to be true
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    public static void WaitForElementToBeVisible(this IWebDriver webDriver, By locator)
    {
        WaitForElementToBeVisible(webDriver, locator, null);
    }

    /// <summary>
    ///     Wait for the selected element "display" state to be true
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitForElementToBeVisible(this IWebDriver webDriver, By locator, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    /// <summary>
    ///     Wait for the tab title to change to a specific string
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="title">The specified title on the page</param>
    public static bool WaitForTabTitleToChange(this IWebDriver webDriver, string title)
    {
        return WaitForTabTitleToChange(webDriver, title, null);
    }

    /// <summary>
    ///     Wait for the tab title to change to a specific string
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="title">The specified title on the page</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static bool WaitForTabTitleToChange(this IWebDriver webDriver, string title, int? timeoutOverride)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GetTimeout(timeoutOverride)));
        Func<IWebDriver, bool> waitForElement = Web =>
        {
            if (webDriver.Title.ToLowerInvariant().Contains(title.ToLowerInvariant()))
            {
                return true;
            }

            return false;
        };
        wait.Until(waitForElement);
        return webDriver.Title.ToLowerInvariant().Contains(title.ToLowerInvariant());
    }

    /// <summary>
    ///     Check through all the listed items and check the text is valid for the specified web element position
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">Listed elements to verify through</param>
    public static void WaitUntilElementInListIsDisplayed(this IWebDriver webDriver, IWebElement element)
    {
        WaitUntilElementInListIsDisplayed(webDriver, element, null);
    }

    /// <summary>
    ///     Check through all the listed items and check the text is valid for the specified web element position
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">Listed elements to verify through</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilElementInListIsDisplayed(this IWebDriver webDriver, IWebElement element, int? timeoutOverride)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GetTimeout(timeoutOverride)));
        Func<IWebDriver, bool> waitForElement = Web =>
        {
            if (element.Displayed)
            {
                return true;
            }

            return false;
        };
        wait.Until(waitForElement);
    }

    /// <summary>
    ///     Wait for an Iframe to be inside the DOM and then switches to the selected frame
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="frameName">Selected iframe to switch to</param>
    public static void WaitUntilFrameIsAvailableThenSwitchToIt(this IWebDriver webDriver, string frameName)
    {
        WaitUntilFrameIsAvailableThenSwitchToIt(webDriver, frameName, null);
    }

    /// <summary>
    ///     Wait for an Iframe to be inside the DOM and then switches to the selected frame
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="frameName">Selected iframe to switch to</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilFrameIsAvailableThenSwitchToIt(this IWebDriver webDriver, string frameName, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameName));
        webDriver.BrowserSleep(Constants.WaitTimeLimit.ThreeSeconds);
    }

    /// <summary>
    ///     Check through all the listed items and check the text is valid for the specified web element position
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">Listed elements to verify through</param>
    /// <param name="matchedMessage">Selected string to find in a particular web element</param>
    public static void WaitUntilTextInElementIsPresent(this IWebDriver webDriver, IWebElement element, string matchedMessage)
    {
        WaitUntilTextInElementIsPresent(webDriver, element, matchedMessage, null);
    }

    /// <summary>
    ///     Check through all the listed items and check the text is valid for the specified web element position
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="element">Listed elements to verify through</param>
    /// <param name="matchedMessage">Selected string to find in a particular web element</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilTextInElementIsPresent(this IWebDriver webDriver, IWebElement element, string matchedMessage, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.TextToBePresentInElement(element, matchedMessage));
    }

    /// <summary>
    ///     Wait for a certain string to be displayed in a web element
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="matchedMessage">Selected string to find in a particular web element</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilTextIsPresent(this IWebDriver webDriver, By locator, string matchedMessage)
    {
        WaitUntilTextIsPresent(webDriver, locator, matchedMessage, null);
    }

    /// <summary>
    ///     Wait for a certain string to be displayed in a web element
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">The specified web element on the page</param>
    /// <param name="matchedMessage">Selected string to find in a particular web element</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilTextIsPresent(this IWebDriver webDriver, By locator, string matchedMessage, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, matchedMessage));
    }

    /// <summary>
    ///     Wait for the url to change and contain the string "matchedmessage"
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="matchedMessage">Selected string to compare the url the webdriver is currently viewing</param>
    public static void WaitUntilUrlContains(this IWebDriver webDriver, string matchedMessage)
    {
        WaitUntilUrlContains(webDriver, matchedMessage, null);
    }

    /// <summary>
    ///     Wait for the url to change and contain the string "matchedmessage"
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="matchedMessage">Selected string to compare the url the webdriver is currently viewing</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilUrlContains(this IWebDriver webDriver, string matchedMessage, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.UrlContains(matchedMessage));
    }

    /// <summary>
    ///     Wait for the url to contain the following string
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="url">Updated url string</param>
    public static void WaitUntilUrlHasChanged(this IWebDriver webDriver, string url)
    {
        WaitUntilUrlHasChanged(webDriver, url, null);
    }

    /// <summary>
    ///     Wait for the url to contain the following string
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="url">Updated url string</param>
    /// <param name="timeoutOverride">The maximum wait (in seconds) as the timeout override</param>
    public static void WaitUntilUrlHasChanged(this IWebDriver webDriver, string url, int? timeoutOverride)
    {
        var timeOut = TimeSpan.FromSeconds(GetTimeout(timeoutOverride));
        var wait = new WebDriverWait(webDriver, timeOut);
        wait.Until(ExpectedConditions.UrlToBe(url));
    }

    /// <summary>
    ///     Gets the rgba colour of an element and returns as hex format.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">Element to retrieve colour</param>
    public static string GetColorAsHex(this IWebDriver webDriver, By locator)
    {
        var color = webDriver.FindElement(locator).GetCssValue("color");
        var colors = Regex.Matches(color, @"\d+").Select(m => int.Parse(m.Value)).ToList();

        return $"#{colors[0]:X}{colors[1]:X}{colors[2]:X}".ToLowerInvariant();
    }

    /// <summary>
    ///     Gets the rgba colour of an element and returns as rgb format.
    /// </summary>
    /// <param name="webDriver">Current webdriver in use</param>
    /// <param name="locator">Element to retrieve colour</param>
    public static string GetColorAsRgb(this IWebDriver webDriver, By locator)
    {
        var color = webDriver.FindElement(locator).GetCssValue("color");
        var colors = Regex.Matches(color, @"\d+").Select(m => int.Parse(m.Value)).ToList();

        return $"rgb({colors[0]}, {colors[1]}, {colors[2]})";
    }

    private static int GetTimeout(int? timeoutOverride)
    {
        return 0;
        //return timeoutOverride.GetValueOrDefault(DefaultTimeout);
    }
}