namespace TeeToys;

internal class CleanupFilter : ConsoleAppFilter
{
    public override async ValueTask Invoke(ConsoleAppContext context, Func<ConsoleAppContext, ValueTask> next)
    {
        // Cleanup the specified directory
        Cleanup(Constants.RootDirPath);

        // Recreate the root directory if it doesn't exist
        Directory.CreateDirectory(Constants.RootDirPath);

        await next(context);
    }
    
    private static void Cleanup(string dirPath)
    {
        string[] directryEntries = Directory.GetDirectories(dirPath);

        foreach (string subDirPath in directryEntries)
        {
            Cleanup(subDirPath);
        }

        // Delete empty files within the directry
        string[] fileEntries = Directory.GetFiles(dirPath);
        foreach(string filePath in fileEntries)
        {
            if(IsDeleteTarget(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Deleted empty file: {filePath}");
            }
        }

        // Delete empty directries
        if (Directory.GetFileSystemEntries(dirPath).Length == 0)
        {
            Directory.Delete(dirPath);
            Console.WriteLine($"Deleted empty directry: {dirPath}");
        }
    }

    private static bool IsDeleteTarget(string filePath)
    {
        var fileInfo = new FileInfo(filePath);

        if (Constants.TextExtensions.Contains(fileInfo.Extension)
            || Constants.MarkdownExtensions.Contains(fileInfo.Extension))
        {
            if (fileInfo.Length == 0)
            {
                return true;
            }
        }

        return false;
    }
}
