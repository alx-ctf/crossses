using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEfCore.Models;

[Table("comments")]
public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("author_name")]
    public string AuthorName { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    [Column("text")]
    public string Text { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("post_id")]
    public int PostId { get; set; }

    public BlogPost Post { get; set; } = null!;
}
