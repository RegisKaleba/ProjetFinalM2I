using System;
using System.Collections.Generic;

namespace BlogConsole.Models
{
    // Représente un article du blog
    public class Article
    {
        // Identifiant unique de l'article
        public int Id { get; set; }

        // Titre de l'article
        public string Title { get; set; }

        // Contenu de l'article
        public string Content { get; set; }

        // Date de création de l'article
        public DateTime CreatedAt { get; set; }

        // Date de dernière mise à jour (nullable car pas toujours modifié)
        public DateTime? UpdatedAt { get; set; }

        // Liste des commentaires associés à l'article
        public List<Comment> Comments { get; set; }

        // Constructeur : initialise un nouvel article
        public Article(int id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;

            // La date de création est définie automatiquement
            CreatedAt = DateTime.Now;

            // L'article n'a pas encore été modifié
            UpdatedAt = null;

            // Initialisation de la liste des commentaires
            // (évite les NullReferenceException)
            Comments = new List<Comment>();
        }

        // Ajoute un commentaire à l'article
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        // Méthode utilisée pour afficher l'article dans la console
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

