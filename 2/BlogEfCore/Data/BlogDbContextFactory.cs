using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogEfCore.Data;

/// <summary>
/// Тот же путь к БД, что при запуске (для dotnet ef).
/// </summary>
public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args) => new();
}
