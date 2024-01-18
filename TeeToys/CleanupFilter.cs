namespace TeeToys;

internal class CleanupFilter : ConsoleAppFilter
{
    public override async ValueTask Invoke(ConsoleAppContext context, Func<ConsoleAppContext, ValueTask> next)
    {
        // フォルダが存在しない場合は作成    
        Directory.CreateDirectory(Constants.RootDirPath);
        
        // 対象フォルダをクリーンアップ
        Cleanup(Constants.RootDirPath);
        Console.WriteLine($"Cleanup completed");

        await next(context);
    }
    
    private static void Cleanup(string dirPath)
    {
        string[] directryEntries = Directory.GetDirectories(dirPath);

        // サブディレクトリを再帰的に処理
        foreach (string subDirPath in directryEntries)
        {
            Cleanup(subDirPath);
        }        

        // 空のファイルを削除
        string[] fileEntries = Directory.GetFiles(dirPath);
        foreach(string filePath in fileEntries)
        {
            if(IsDeleteTarget(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Deleted empty file: {filePath}");
            }
        }

        // 空のディレクトリを削除
        if (Directory.GetFileSystemEntries(dirPath).Length == 0)
        {
            Directory.Delete(dirPath);
            Console.WriteLine($"Deleted empty directry: {dirPath}");
        }
    }

    // 削除対象のファイルかどうかを判定
    private static bool IsDeleteTarget(string filePath)
    {
        var fileInfo = new FileInfo(filePath);

        // .txtか.mdで0byte
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
