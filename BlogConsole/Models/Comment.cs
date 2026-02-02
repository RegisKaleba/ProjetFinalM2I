using System;

namespace BlogConsole.Models
{
    // Représente un commentaire associé à un article
    public class Comment
    {
        // Identifiant unique du commentaire
        public int Id { get; set; }

        // Identifiant de l'article auquel le commentaire est lié
        public int ArticleId { get; set; }

        // Nom de l'auteur du commentaire
        public string Author { get; set; }

        // Contenu du commentaire
        public string Content { get; set; }

        // Date de création du commentaire
        public DateTime CreatedAt { get; set; }

        // Constructeur : initialise un nouveau commentaire
        public Comment(int id, int articleId, string author, string content)
        {
            Id = id;
            ArticleId = articleId;
            Author = author;
            Content = content;

            // La date de création est définie automatiquement
            CreatedAt = DateTime.Now;
        }

        // Méthode utilisée pour afficher le commentaire dans la console
        public override string ToString()
        {
            return $"Comment #{Id} by {Author} on Article #{ArticleId}: {Content} (Created: {CreatedAt})";
        }
    }
}
