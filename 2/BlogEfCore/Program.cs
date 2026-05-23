using BlogEfCore;
using BlogEfCore.Data;
using BlogEfCore.Models;
using Microsoft.EntityFrameworkCore;

await using var db = new BlogDbContext();
await db.Database.MigrateAsync();

if (!await db.Posts.AnyAsync())
{
    var post = new BlogPost
    {
        Title = "Первый пост",
        Slug = "pervyj-post",
        Body = "Текст демонстрационной записи для практической работы №2.",
        PublishedAt = DateTime.UtcNow,
        Comments =
        {
            new Comment
            {
                AuthorName = "Кобец Кирилл",
                Text = "Комментарий к записи блога.",
                CreatedAt = DateTime.UtcNow
            }
        }
    };
    db.Posts.Add(post);
    await db.SaveChangesAsync();
}

var count = await db.Posts.Include(p => p.Comments).CountAsync();
Console.WriteLine($"База данных готова. Записей в блоге: {count}");
Console.WriteLine($"Файл БД: {DbPath.DatabaseFile}");
