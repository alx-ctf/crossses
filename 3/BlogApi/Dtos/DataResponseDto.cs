namespace BlogApi.Dtos;

public sealed class DataResponseDto
{
    public string AppName { get; init; } = string.Empty;
    public string AppVersion { get; init; } = string.Empty;
    public int TotalInDatabase { get; init; }
    public int ReturnedCount { get; init; }
    public string ProcessedBy { get; init; } = string.Empty;
    public IReadOnlyList<PostItemDto> Items { get; init; } = Array.Empty<PostItemDto>();
}
