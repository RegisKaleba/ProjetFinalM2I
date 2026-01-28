using System;
using System.Collections.Generic;
using System.Text;

namespace BlogConsole.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; }

        // Constructeur
        public Article(int id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            Comments = new List<Comment>();
        }

        // Ajouter un commentaire
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        // ToString() pour affichage
        public override string ToString()
        {
            return $"Article #{Id}: {Title}\n" +
                   $"Content: {Content}\n" +
                   $"Created: {CreatedAt}" +
                   (UpdatedAt.HasValue ? $", Updated: {UpdatedAt.Value}" : "") +
                   $"\nComments: {Comments.Count}";
        }
    }
}
