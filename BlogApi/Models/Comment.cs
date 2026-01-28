using System;

namespace BlogApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}