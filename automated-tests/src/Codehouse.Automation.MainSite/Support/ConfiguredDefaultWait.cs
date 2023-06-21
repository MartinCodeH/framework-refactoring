using Codehouse.Automation.MainSite.Configuration;
using Codehouse.Automation.MainSite.Services;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Codehouse.Automation.MainSite.Support;

internal class ConfiguredDefaultWait<T> : DefaultWait<T>
{
    public ConfiguredDefaultWait(T source, IConfiguration configuration)
        : this(new SystemClock(), source, configuration, new WaitSettings())
    {
    }

    public ConfiguredDefaultWait(T source, IConfiguration configuration, WaitSettings settings)
        : this(new SystemClock(), source, configuration, settings)
    {
    }

    public ConfiguredDefaultWait(
        IClock clock,
        T source,
        IConfiguration configuration,
        WaitSettings settings)
        : base(source, clock)
    {
        var waitOptions = configuration.GetSection(WaitOptions.SectionName).Get<WaitOptions>() ?? new WaitOptions();
        Timeout = settings.TimeoutOverride ?? TimeSpan.FromSeconds(waitOptions.DefaultWaitTimeoutInSeconds);
        PollingInterval = settings.PollingIntervalOverride ??
                          TimeSpan.FromSeconds(waitOptions.DefaultPollingIntervalInSeconds);
        IgnoreExceptionTypes(typeof(NotFoundException));
    }
}