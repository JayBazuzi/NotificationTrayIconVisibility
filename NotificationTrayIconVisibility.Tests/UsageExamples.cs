using System.Windows;

namespace NotificationTrayIconVisibility.Tests;

// Compiled but not run, so the README example stays in sync with the API.
static class UsageExamples
{
    public static void HideToTrayOnlyWhenReachable(Window window)
    {
        // begin-snippet: hide-to-tray
        if (TrayIconReachability.CanUserReachTrayIcon(Environment.ProcessPath!))
        {
            window.Hide();
        }
        else
        {
            window.WindowState = WindowState.Minimized;
        }
        // end-snippet
    }

    public static void WarnWhenHiddenToUnreachableTray(Window window)
    {
        // begin-snippet: warn-when-hidden
        if (!TrayIconReachability.CanUserReachTrayIcon())
        {
            MessageBox.Show(
                "The app is still running, but its tray icon is hidden. " +
                "To show it, open Settings > Personalization > Taskbar > Other system tray icons.",
                "App hidden");
        }
        // end-snippet
    }
}
