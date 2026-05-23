using BlogApi.Configuration;
using BlogApi.Data;
using BlogApi.Dtos;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var connectionString = builder.Configuration.GetConnectionString("BlogDb")
    ?? throw new InvalidOperationException(
        "Строка подключения 'BlogDb' не найдена в секции ConnectionStrings (appsettings.json).");

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IDataService, BlogDataService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    await db.Database.MigrateAsync();
    await DbInitializer.SeedAsync(db);
}

app.MapGet("/api/data", async (IDataService dataService, CancellationToken cancellationToken) =>
    Results.Ok(await dataService.GetDataAsync(cancellationToken)));

app.MapGet("/api/config", (IConfiguration configuration) =>
{
    var section = configuration.GetSection("AppSettings");
    var response = new ConfigResponseDto
    {
        AppName = section["AppName"] ?? string.Empty,
        Version = section["Version"] ?? string.Empty,
        MaxItems = section.GetValue<int>("MaxItems"),
        ConnectionStringConfigured = !string.IsNullOrWhiteSpace(
            configuration.GetConnectionString("BlogDb"))
    };
    return Results.Ok(response);
});

app.MapGet("/", () => Results.Redirect("/api/data"));

app.Run();
