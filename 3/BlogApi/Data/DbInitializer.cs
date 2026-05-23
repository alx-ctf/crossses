using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(BlogDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.Posts.AnyAsync(cancellationToken))
            return;

        var now = DateTime.UtcNow;
        var posts = new[]
        {
            new BlogPost
            {
                Title = "Первый пост",
                Slug = "pervyj-post",
                Body = "Текст демонстрационной записи для практической работы №3.",
                PublishedAt = now.AddDays(-2),
                Comments =
                {
                    new Comment
                    {
                        AuthorName = "Кобец Кирилл",
                        Text = "Комментарий к первой записи.",
                        CreatedAt = now.AddDays(-1)
                    }
                }
            },
            new BlogPost
            {
                Title = "ASP.NET Core Minimal API",
                Slug = "aspnet-minimal-api",
                Body = "Обзор подхода Minimal API и внедрения зависимостей.",
                PublishedAt = now.AddDays(-1)
            },
            new BlogPost
            {
                Title = "Entity Framework и SQLite",
                Slug = "ef-core-sqlite",
                Body = "Продолжение модели данных из практической работы №2.",
                PublishedAt = now,
                Comments =
                {
                    new Comment
                    {
                        AuthorName = "Кобец Кирилл",
                        Text = "База данных переиспользована из ПЗ2.",
                        CreatedAt = now
                    },
                    new Comment
                    {
                        AuthorName = "Преподаватель",
                        Text = "Проверка endpoint /api/data.",
                        CreatedAt = now
                    }
                }
            }
        };

        db.Posts.AddRange(posts);
        await db.SaveChangesAsync(cancellationToken);
    }
}
