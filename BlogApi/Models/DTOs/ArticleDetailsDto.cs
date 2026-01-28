namespace BlogApi.Models.DTOs
{
    public class ArticleDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
    }
}
