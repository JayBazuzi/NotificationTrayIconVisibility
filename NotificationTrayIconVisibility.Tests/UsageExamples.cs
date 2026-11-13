using System.Windows;

namespace NotificationTrayIconVisibility.Tests;

// Compiled but not run, so the README example stays in sync with the API.
static class UsageExamples
{
    public static void HideToTrayOnlyWhenReachable(Window window)
    {
        // begin-snippet: hide-to-tray
        if (new TrayIconReachability().CanUserReachTrayIcon(Environment.ProcessPath!))
        {
            window.Hide();
        }
        else
        {
            window.WindowState = WindowState.Minimized;
        }
        // end-snippet
    }
}
