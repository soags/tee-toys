namespace TeeToys;

internal static class Constants
{
    /// <summary>
    /// ルートディレクトリ
    /// </summary>
    public const string RootDirPath = @"C:\tmp";

    /// <summary>
    /// アーカイブディレクトリ
    /// </summary>
    public static readonly string ArchiveDir = Path.Combine(RootDirPath, ".archives");

    /// <summary>
    /// テキストファイルの拡張子
    /// </summary>
    public static readonly string[] TextExtensions = { ".txt" };

    /// <summary>
    /// Markdownファイルの拡張子
    /// </summary>
    public static readonly string[] MarkdownExtensions = { ".md", ".mkdn", "mdown", "markdown" };    
}
