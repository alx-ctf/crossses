namespace BlogEfCore;

/// <summary>
/// Файл БД в каталоге проекта (рядом с Program.cs).
/// </summary>
public static class DbPath
{
    public const string FileName = "blog_variant2.db";

    public static string DatabaseFile =>
        Path.Combine(ProjectDirectory, FileName);

    public static string ProjectDirectory
    {
        get
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (File.Exists(Path.Combine(dir.FullName, "BlogEfCore.csproj")))
                    return dir.FullName;

                dir = dir.Parent;
            }

            return Directory.GetCurrentDirectory();
        }
    }
}
