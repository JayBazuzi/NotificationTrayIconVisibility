namespace NotificationTrayIconVisibility.Tests;

public class TrayIconReachabilityTests
{
    [Fact]
    public void ReadsTheRealSettingsWithoutThrowing()
    {
        new TrayIconReachability().CanUserReachTrayIcon(@"C:\Users\someone\AppData\Local\SomeApp\SomeApp.exe");
    }
}
