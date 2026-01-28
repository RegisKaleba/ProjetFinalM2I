using System;
using System.Collections.Generic;

namespace BlogApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}