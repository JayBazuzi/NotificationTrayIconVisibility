# Notification Tray Icon Visibility

[![Build and test](https://github.com/JayBazuzi/NotificationTrayIconVisibility/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/JayBazuzi/NotificationTrayIconVisibility/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/NotificationTrayIconVisibility.svg)](https://www.nuget.org/packages/NotificationTrayIconVisibility)

Tells a Windows app whether the user can reach its notification area (system tray) icon.

## Why

Windows 11 can hide a tray icon, and can also hide the menu of hidden icons.
When both are hidden, a tray app leaves the user no way to see that the app is running, interact with it, and exit it.
it's just hidden and lost.

## Example

Ask before hiding the window, and keep it on the taskbar when the icon can't be reached:

<!-- snippet: hide-to-tray -->
<a id='snippet-hide-to-tray'></a>
```cs
if (TrayIconReachability.CanUserReachTrayIcon(Environment.ProcessPath!))
{
    window.Hide();
}
else
{
    window.WindowState = WindowState.Minimized;
}
```
<sup><a href='/NotificationTrayIconVisibility.Tests/UsageExamples.cs#L10-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-hide-to-tray' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Or tell the user how to get it back:

<!-- snippet: warn-when-hidden -->
<a id='snippet-warn-when-hidden'></a>
```cs
if (!TrayIconReachability.CanUserReachTrayIcon())
{
    MessageBox.Show(
        "The app is still running, but its tray icon is hidden. " +
        "To show it, open Settings > Personalization > Taskbar > Other system tray icons.",
        "App hidden");
}
```
<sup><a href='/NotificationTrayIconVisibility.Tests/UsageExamples.cs#L24-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-warn-when-hidden' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## How it works

The icon counts as reachable when either of these is true, both read from the current user's registry:

<!-- include: registry-table.verified. path: /NotificationTrayIconVisibility.Tests/registry-table.verified.include.md -->
| Setting                           | Key                                                                                         | Value                          |
|-----------------------------------|---------------------------------------------------------------------------------------------|--------------------------------|
| The icon is shown on the taskbar  | `HKCU\Control Panel\NotifyIconSettings\*`                                                   | `ExecutablePath`, `IsPromoted` |
| The menu of hidden icons is shown | `HKCU\Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\TrayNotify` | `SystemTrayChevronVisibility`  |
<!-- endInclude -->

