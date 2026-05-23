namespace BlogApi.Dtos;

public sealed class PostItemDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Preview { get; init; } = string.Empty;
    public DateTime PublishedAt { get; init; }
    public int CommentCount { get; init; }
}
