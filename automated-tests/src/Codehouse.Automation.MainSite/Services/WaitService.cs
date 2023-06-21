using Codehouse.Automation.MainSite.Support;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;

namespace Codehouse.Automation.MainSite.Services;

internal class WaitService
{
    private readonly IConfiguration _configuration;

    public WaitService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DefaultWait<T> GetWait<T>(T source)
    {
        return new ConfiguredDefaultWait<T>(source, _configuration);
    }

    public DefaultWait<T> GetWait<T>(T source, WaitSettings settings)
    {
        return new ConfiguredDefaultWait<T>(source, _configuration, settings);
    }
}