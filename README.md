# Notification Tray Icon Visibility

[![Build and test](https://github.com/JayBazuzi/NotificationTrayIconVisibility/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/JayBazuzi/NotificationTrayIconVisibility/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/NotificationTrayIconVisibility.svg)](https://www.nuget.org/packages/NotificationTrayIconVisibility)

Tells a Windows app whether the user can reach its notification area (system tray) icon.

## Why

Windows 11 can hide a tray icon, and can also hide the menu of hidden icons.
When both are hidden, an app that hides its window to the tray leaves the user no way to get it back, short of killing it in Task Manager.

It works with any tray icon library, since it only reads Windows' settings.

## Example

Ask before hiding the window, and keep it on the taskbar when the icon can't be reached:

<!-- snippet: hide-to-tray -->
<a id='snippet-hide-to-tray'></a>
```cs
if (new TrayIconReachability().CanUserReachTrayIcon(Environment.ProcessPath!))
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


## How it works

The icon counts as reachable when either of these is true, both read from the current user's registry:

| Setting                           | Key                                                                                         | Value                         |
|-----------------------------------|---------------------------------------------------------------------------------------------|-------------------------------|
| The icon is shown on the taskbar  | `HKCU\Control Panel\NotifyIconSettings\{ExecutablePath}`                                    | `IsPromoted`                  |
| The menu of hidden icons is shown | `HKCU\Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\TrayNotify` | `SystemTrayChevronVisibility` |

