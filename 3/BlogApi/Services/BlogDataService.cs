using BlogApi.Configuration;
using BlogApi.Data;
using BlogApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogApi.Services;

public sealed class BlogDataService : IDataService
{
    private readonly BlogDbContext _db;
    private readonly AppSettings _settings;

    public BlogDataService(BlogDbContext db, IOptions<AppSettings> options)
    {
        _db = db;
        _settings = options.Value;
    }

    public async Task<DataResponseDto> GetDataAsync(CancellationToken cancellationToken = default)
    {
        var total = await _db.Posts.CountAsync(cancellationToken);

        var posts = await _db.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .OrderByDescending(p => p.PublishedAt)
            .Take(_settings.MaxItems)
            .ToListAsync(cancellationToken);

        var items = posts
            .Select(p => new PostItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Preview = p.Preview,
                PublishedAt = p.PublishedAt,
                CommentCount = p.Comments.Count
            })
            .ToList();

        return new DataResponseDto
        {
            AppName = _settings.AppName,
            AppVersion = _settings.Version,
            TotalInDatabase = total,
            ReturnedCount = items.Count,
            ProcessedBy = nameof(BlogDataService),
            Items = items
        };
    }
}
