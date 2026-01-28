namespace BlogApi.Models.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Author { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
