using Microsoft.Win32;

namespace NotificationTrayIconVisibility;

/// <summary>
/// Windows 11 can hide a tray icon, and can also hide the menu of hidden icons, leaving the user no way to reach the icon.
/// Both settings are in the current user's registry.
/// </summary>
public static class TrayIconReachability
{
    public const string NotifyIconSettingsPath = @"Control Panel\NotifyIconSettings";
    public const string ExecutablePathValue = "ExecutablePath";
    public const string IsPromotedValue = "IsPromoted";
    public const string TrayNotifyPath = @"Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\TrayNotify";
    public const string SystemTrayChevronVisibilityValue = "SystemTrayChevronVisibility";

    /// <summary>
    /// A new icon is hidden until the user chooses to show it, so an icon Windows hasn't recorded yet counts as hidden.
    /// Older Windows has no setting for the hidden icons menu and always shows it.
    /// </summary>
    public static bool CanUserReachTrayIcon(string? exePath = null) => IsTrayIconShownOnTaskbar(exePath) || IsHiddenIconsMenuShown();

    public static bool IsTrayIconShownOnTaskbar(string? exePath = null)
    {
        exePath ??= Environment.ProcessPath;

        using var settings = Registry.CurrentUser.OpenSubKey(NotifyIconSettingsPath);
        if (settings is null)
        {
            return false;
        }

        foreach (var iconName in settings.GetSubKeyNames())
        {
            using var icon = settings.OpenSubKey(iconName);
            if (string.Equals(icon?.GetValue(ExecutablePathValue) as string, exePath, StringComparison.OrdinalIgnoreCase) && icon?.GetValue(IsPromotedValue) is 1)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsHiddenIconsMenuShown()
    {
        using var trayNotify = Registry.CurrentUser.OpenSubKey(TrayNotifyPath);
        return trayNotify?.GetValue(SystemTrayChevronVisibilityValue) is not 0;
    }
}
