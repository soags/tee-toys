using System.Diagnostics;
using System.Text.RegularExpressions;
using TeeToys;
using System.Globalization;

var app = ConsoleApp.Create(args, config =>
{
    config.GlobalFilters = new[] { new CleanupFilter() };
});

app.AddRootCommand("Open today directory", () =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        Console.WriteLine($"Created empty directry: {todayDirPath}");
        Process.Start("explorer.exe", todayDirPath);
    });

app.AddCommand("txt", "Open temporary text file", 
    () =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        string filePath = Path.Combine(todayDirPath, $"{DateTime.Now:yyyy-MM-dd_HHmmss}.txt");
        File.Create(filePath).Dispose();
        Console.WriteLine($"Created empty file: {filePath}");
        OpenFile(filePath);
    });

app.AddCommand("md", "Open temporary markdown file",() =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        string filePath = Path.Combine(todayDirPath, $"{DateTime.Now:yyyy-MM-dd_HHmmss}.md");
        File.Create(filePath).Dispose();
        Console.WriteLine($"Created empty file: {filePath}");
        OpenFile(filePath);
    });

app.AddCommand("archive", "Archive old temporary folders",
    ([Option(null, "Number of days to exclude from archiving")] int days = 30) =>
    {
        var archiveFromDate = DateTime.Today.AddDays(Math.Max(days, 1) * -1);

        string[] directryEntries = Directory.GetDirectories(Constants.RootDirPath);

        Directory.CreateDirectory(Constants.ArchiveDir);

        foreach (string dir in directryEntries)
        {
            // Check if the folder name contains a date.
            var match = TemporaryFolderDateRegex().Match(dir);
            if (match.Success)
            {
                // Check if the folder date within the archiving period.
                var folderDate = DateTime.ParseExact(match.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (folderDate <= archiveFromDate)
                {
                    string dirName = Path.GetFileName(dir)!;
                    string dest = Path.Combine(Constants.ArchiveDir, dirName);

                    // Skip If folder with the same name already exists in destination.
                    if (Directory.Exists(dest))
                    {
                        Console.WriteLine($"Archive destination already exists: {dest}");
                        continue;
                    }

                    // Move to archive destination.
                    Directory.Move(dir, dest);
                    Console.WriteLine($"Archived: {dest}");
                }
            }
        }
    });

app.Run();


string GetOrCreateTodayDirectory()
{
    string dirPath = Path.Combine(Constants.RootDirPath, $"{DateTime.Today:yyyy-MM-dd}");
    Directory.CreateDirectory(dirPath);
    return dirPath;
}

void OpenFile(string filePath)
{
    // Open the specified file using the default associated application
    var p = new Process
    {
        StartInfo = new ProcessStartInfo(filePath)
        {
            UseShellExecute = true
        }
    };
    p.Start();
}

partial class Program
{
    [GeneratedRegex("\\b\\d{4}-\\d{2}-\\d{2}\\b")]
    public static partial Regex TemporaryFolderDateRegex();
}