namespace Codehouse.Automation.MainSite.Services;

internal class SleepService
{
    public void ThreadSleep(TimeSpan timeout)
    {
        Thread.Sleep(timeout);
    }
}