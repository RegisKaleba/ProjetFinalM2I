using System;
using System.Collections.Generic;
using System.Linq;
using BlogConsole.Models;

namespace BlogConsole.Services
{
    // Service chargé de gérer la logique métier des articles
    // (création, lecture, modification et suppression)
    public class ArticleService
    {
        // Liste en mémoire qui stocke les articles
        private List<Article> _articles = new List<Article>();

        // Compteur utilisé pour générer des identifiants uniques
        private int _nextId = 1;

        // =====================
        // Create
        // =====================

        // Crée un nouvel article et l'ajoute à la liste
        public Article Create(string title, string content)
        {
            // Création de l'article avec un ID unique
            var article = new Article(_nextId++, title, content);

            // Ajout de l'article dans la collection
            _articles.Add(article);

            return article;
        }


        // =====================
        // Read
        // =====================

        // Retourne la liste de tous les articles
        public List<Article> GetAll()
        {
            return _articles;
        }

        // Retourne un article à partir de son identifiant
        public Article GetById(int id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        // =====================
        // Update
        // =====================

        // Met à jour le titre et le contenu d'un article existant
        public bool Update(int id, string newTitle, string newContent)
        {
            // Recherche de l'article
            var article = GetById(id);

            // Si l'article n'existe pas, la mise à jour échoue
            if (article == null) return false;

            // Mise à jour des données
            article.Title = newTitle;
            article.Content = newContent;

            // Mise à jour de la date de modification
            article.UpdatedAt = DateTime.Now;

            return true;
        }

        // =====================
        // Delete
        // =====================

        // Supprime un article à partir de son identifiant
        public bool Delete(int id)
        {
            // Recherche de l'article
            var article = GetById(id);

            // Si l'article n'existe pas, la suppression échoue
            if (article == null) return false;

            // Suppression de l'article de la liste
            _articles.Remove(article);

            return true;
        }
    }
}
