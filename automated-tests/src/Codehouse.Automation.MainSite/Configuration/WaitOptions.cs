namespace Codehouse.Automation.MainSite.Configuration;

internal record WaitOptions
{
    public const string SectionName = "wait";

    public double DefaultWaitTimeoutInSeconds { get; init; }

    public double DefaultPollingIntervalInSeconds { get; init; }
}