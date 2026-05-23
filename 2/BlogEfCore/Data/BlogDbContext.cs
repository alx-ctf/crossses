using BlogEfCore;
using BlogEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogEfCore.Data;

public class BlogDbContext : DbContext
{
    public DbSet<BlogPost> Posts => Set<BlogPost>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite($"Data Source={DbPath.DatabaseFile}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.ToTable("posts");
            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.Slug)
                .IsUnique()
                .HasDatabaseName("IX_posts_slug_unique");

            entity.Property(p => p.Title).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Slug).HasMaxLength(120).IsRequired();

            entity.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comments");
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.PostId).HasDatabaseName("IX_comments_post_id");
            entity.Property(c => c.AuthorName).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Text).HasMaxLength(1000).IsRequired();
        });
    }
}
