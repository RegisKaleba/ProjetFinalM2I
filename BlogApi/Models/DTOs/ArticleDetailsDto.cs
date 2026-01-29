using BlogApi.Models.DTOs;
using System.Text.Json.Serialization;

public class ArticleDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedAt { get; set; }

    public List<CommentDto> Comments { get; set; } = new();
}
