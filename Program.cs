using System.Diagnostics;
using TeeToys;

var app = ConsoleApp.Create(args, config =>
{
    config.GlobalFilters = new[] { new CleanupFilter() };
});

app.AddRootCommand("Open today directory", () =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        Process.Start("explorer.exe", todayDirPath);
    });

app.AddCommand("txt", "Open temporary text file", () =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        string filePath = CreateTemporaryTextFile(todayDirPath);
        OpenFile(filePath);
    });

app.AddCommand("md", "Open temporary markdown file",() =>
    {
        string todayDirPath = GetOrCreateTodayDirectory();
        string filePath = CreateTemporaryMarkdownFile(todayDirPath);
        OpenFile(filePath);
    });

app.Run();


string CreateTemporaryTextFile(string todayDir)
{
    string filePath = Path.Combine(todayDir, $"{DateTime.Now:yyyy-MM-dd_HHmmss}.txt");
    File.Create(filePath).Dispose();
    return filePath;
}

string CreateTemporaryMarkdownFile(string todayDir)
{
    string filePath = Path.Combine(todayDir, $"{DateTime.Now:yyyy-MM-dd_HHmmss}.md");
    File.Create(filePath).Dispose();
    return filePath;
}

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