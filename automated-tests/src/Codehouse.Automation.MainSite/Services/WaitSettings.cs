namespace Codehouse.Automation.MainSite.Services;

internal class WaitSettings
{
    public TimeSpan? TimeoutOverride { get; init; } = null;

    public TimeSpan? PollingIntervalOverride { get; init; } = null;
}