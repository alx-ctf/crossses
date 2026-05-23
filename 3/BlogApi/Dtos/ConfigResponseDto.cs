namespace BlogApi.Dtos;

public sealed class ConfigResponseDto
{
    public string AppName { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public int MaxItems { get; init; }
    public bool ConnectionStringConfigured { get; init; }
}
