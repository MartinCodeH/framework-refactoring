using Codehouse.Automation.MainSite.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Codehouse.Automation.MainSite.Steps;

[Binding]
internal class HeaderSteps
{
    private readonly Header _header;

    public HeaderSteps(Header header)
    {
        _header = header;
    }

    [Then(@"the logo should be displayed in the header")]
    public void ThenTheLogoShouldBeDisplayedInTheHeader()
    {
        _header.ValidateLogo().Should().BeTrue();
    }

    [Then(@"the (.*) link is displayed on the header")]
    public void ThenTheServicesLinkIsDisplayedOnTheHeader(string linkName)
    {
        _header.GetLinkNames().Should().ContainEquivalentOf(linkName, options => options.Using<string>(StringComparer.InvariantCultureIgnoreCase));
    }

}