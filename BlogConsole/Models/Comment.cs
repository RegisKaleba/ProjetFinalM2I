using System;
using System.Collections.Generic;
using System.Text;

namespace BlogConsole.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Constructeur
        public Comment(int id, int articleId, string author, string content)
        {
            Id = id;
            ArticleId = articleId;
            Author = author;
            Content = content;
            CreatedAt = DateTime.Now;
        }

        // ToString() pour affichage
        public override string ToString()
        {
            return $"Comment #{Id} by {Author} on Article #{ArticleId}: {Content} (Created: {CreatedAt})";
        }
    }
}