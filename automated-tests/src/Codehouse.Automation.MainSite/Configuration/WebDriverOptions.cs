namespace Codehouse.Automation.MainSite.Configuration;

internal record WebDriverOptions
{
    public const string SectionName = "webDriver";

    public string Type { get; init; } = string.Empty;

    public bool Headless { get; init; } = false;
}