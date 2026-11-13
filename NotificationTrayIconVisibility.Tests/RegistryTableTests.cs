using System.Runtime.CompilerServices;
using EmptyFiles;

namespace NotificationTrayIconVisibility.Tests;

public class RegistryTableTests
{
    // The .include.md ending lets mdsnippets include the verified file in the README.
    [ModuleInitializer]
    internal static void Initialize() => FileExtensions.AddTextExtension("include.md");

    [Fact]
    public Task RegistryTable()
    {
        string[][] rows =
        [
            ["Setting", "Key", "Value"],
            ["The icon is shown on the taskbar", $@"`HKCU\{TrayIconReachability.NotifyIconSettingsPath}\*`", $"`{TrayIconReachability.ExecutablePathValue}`, `{TrayIconReachability.IsPromotedValue}`"],
            ["The menu of hidden icons is shown", $@"`HKCU\{TrayIconReachability.TrayNotifyPath}`", $"`{TrayIconReachability.SystemTrayChevronVisibilityValue}`"],
        ];

        var widths = Enumerable.Range(0, rows[0].Length).Select(column => rows.Max(row => row[column].Length)).ToArray();
        static string Line(IEnumerable<string> cells) => "|" + string.Join("|", cells) + "|";
        var lines = rows.Select(row => Line(row.Select((cell, column) => $" {cell.PadRight(widths[column])} "))).ToList();
        lines.Insert(1, Line(widths.Select(width => new string('-', width + 2))));

        return Verify(string.Join("\n", lines), extension: "include.md").UseFileName("registry-table");
    }
}
