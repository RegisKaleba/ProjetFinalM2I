using BlogApi.Data;     
using BlogApi.Models;   
using Microsoft.EntityFrameworkCore; 

namespace BlogApi.Services
{
    // Service métier pour gérer les coms
    public class CommentService
    {
        private readonly BlogContext _context; // Contexte EF Core injecté

        // Constructeur avec injection de dépendances
        public CommentService(BlogContext context)
        {
            _context = context;
        }

        // =====================
        // Crée un com pour un article spécifique
        // =====================
        public async Task<Comment?> CreateAsync(int articleId, Comment comment)
        {
            //  Vérifier que l'article existe avant d'ajouter le com
            var articleExists = await _context.Articles
                .AnyAsync(a => a.Id == articleId); // Retourne true si l'article existe

            if (!articleExists)
                return null; // Retourne null si l'article n'existe pas

            _context.Comments.Add(comment);     // Ajoute le com à la table
            await _context.SaveChangesAsync();  // Sauvegarde en base

            return comment; // Retourne le com créé 
        }

        // =====================
        // Récupère un com par son ID
        // =====================
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id); // Recherche par Primary Key
        }

        // =====================
        // Supprime un com par son ID
        // =====================
        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id); // Recherche le com
            if (comment == null) return false;                  // Retourne false si inexistant

            _context.Comments.Remove(comment);                  // Supprime le com
            await _context.SaveChangesAsync();                  // Sauvegarde en base
            return true;                                        // Retourne true si succès
        }
    }
}
