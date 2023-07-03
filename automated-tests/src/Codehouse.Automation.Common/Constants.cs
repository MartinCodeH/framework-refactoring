namespace Codehouse.Automation.Common
{
    public record Constants
    {
        public static class WaitTimeLimit
        {
            public static int OneSecond => 1;
            public static int TwoSeconds => 2;
            public static int ThreeSeconds => 3;
            public static int FiveSeconds => 5;
            public static int TenSeconds => 10;
            public static int TwentySeconds => 20;
            public static int ThirtySeconds => 30;
            public static int OneMinute => 60;
            public static int TwoMinutes => 120;
            public static int ThreeMinutes => 180;
        }
        public static IEnumerable<string> ChromeHeadlessArgs => new List<string>
        {
            "--headless=new",
            "--window-size=1680,1050",
        };
    }
}