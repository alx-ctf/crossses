using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models;

[Table("posts")]
public class BlogPost
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 3)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    [StringLength(4000)]
    [Column("body")]
    public string? Body { get; set; }

    [Column("published_at")]
    public DateTime PublishedAt { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [NotMapped]
    public string Preview => string.IsNullOrWhiteSpace(Body)
        ? Title
        : (Body.Length > 80 ? Body[..80] + "…" : Body);
}
