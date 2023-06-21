namespace Codehouse.Automation.MainSite.Support;

public record WebElementQuery
{
    public bool? IsDisplayed { get; init; }

    public bool? IsEnabled { get; init; }
}