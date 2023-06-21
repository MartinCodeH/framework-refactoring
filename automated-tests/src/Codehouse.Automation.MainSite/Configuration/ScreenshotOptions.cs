namespace Codehouse.Automation.MainSite.Configuration;

internal record ScreenshotOptions
{
    public const string SectionName = "screenshot";

    public string Directory { get; init; } = string.Empty;
    public string ArchiveDirectory { get; init; } = string.Empty;
}