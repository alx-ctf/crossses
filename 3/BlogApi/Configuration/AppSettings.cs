namespace BlogApi.Configuration;

/// <summary>
/// Пользовательские настройки из секции AppSettings в appsettings.json.
/// </summary>
public class AppSettings
{
    public string AppName { get; set; } = "Blog API";
    public string Version { get; set; } = "1.0";
    public int MaxItems { get; set; } = 10;
}
