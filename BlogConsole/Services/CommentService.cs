using System;
using System.Collections.Generic;
using System.Linq;
using BlogConsole.Models;

namespace BlogConsole.Services
{
    // Service chargé de gérer la logique métier des commentaires
    public class CommentService
    {
        // Liste en mémoire qui stocke les commentaires
        private List<Comment> _comments = new List<Comment>();

        // Compteur utilisé pour générer des identifiants uniques
        private int _nextId = 1;

        // =====================
        // Create
        // =====================

        // Crée un nouveau commentaire lié à un article
        public Comment Create(int articleId, string author, string content)
        {
            // Création du commentaire avec un ID unique
            var comment = new Comment(_nextId++, articleId, author, content);

            // Ajout du commentaire dans la collection
            _comments.Add(comment);

            return comment;
        }

        // =====================
        // Read
        // =====================

        // Retourne tous les commentaires associés à un article
        public List<Comment> GetByArticleId(int articleId)
        {
            return _comments
                .Where(c => c.ArticleId == articleId)
                .ToList();
        }

        // Retourne un commentaire à partir de son identifiant
        public Comment GetById(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        // =====================
        // Update
        // =====================

        // Met à jour le contenu d'un commentaire existant
        public bool Update(int id, string newContent)
        {
            // Recherche du commentaire
            var comment = GetById(id);

            // Si le commentaire n'existe pas, la mise à jour échoue
            if (comment == null) return false;

            // Mise à jour du contenu
            comment.Content = newContent;

            return true;
        }

        // =====================
        // Delete
        // =====================

        // Supprime un commentaire à partir de son identifiant
        public bool Delete(int id)
        {
            // Recherche du commentaire
            var comment = GetById(id);

            // Si le commentaire n'existe pas, la suppression échoue
            if (comment == null) return false;

            // Suppression du commentaire de la liste
            _comments.Remove(comment);

            return true;
        }
    }
}
