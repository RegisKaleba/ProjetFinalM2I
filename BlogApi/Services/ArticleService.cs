using BlogApi.Data;     
using BlogApi.Models;  
using Microsoft.EntityFrameworkCore; 

namespace BlogApi.Services
{
    // Logique métier pour gérer les articles
    public class ArticleService
    {
        private readonly BlogContext _context; // Contexte EF Core injecté

        // Constructeur avec injection de dépendances
        public ArticleService(BlogContext context)
        {
            _context = context;
        }

        // =====================
        // Récupère tous les articles avec leurs comms
        // =====================
        public async Task<List<Article>> GetAllAsync() =>
            await _context.Articles
                .Include(a => a.Comments) // Charge les commentaires liés (Eager Loading)
                .ToListAsync();           // Retourne la liste de manière asynchrone

        // =====================
        // Récupère un article spécifique par ID avec ses commentaires
        // =====================
        public async Task<Article?> GetByIdAsync(int id) =>
            await _context.Articles
                .Include(a => a.Comments)               // Charge les commentaires
                .FirstOrDefaultAsync(a => a.Id == id); // Renvoie null si inexistant

        // =====================
        // Cré un nouvel article
        // =====================
        public async Task<Article> CreateAsync(Article article)
        {
            _context.Articles.Add(article);  // Ajoute l'article à la table
            await _context.SaveChangesAsync(); // Sauvegarde dans la base
            return article;                   // Retourne l'article créé (avec ID)
        }

        // =====================
        // Met à jour un article existant
        // =====================
        public async Task<bool> UpdateAsync(Article article)
        {
            _context.Articles.Update(article);               // Marque l'article comme modifié
            return await _context.SaveChangesAsync() > 0;    // Retourne true si au moins 1 ligne modifiée
        }

        // =====================
        // Supprime un article par ID
        // =====================
        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id); // Recherche l'article par clé primaire
            if (article == null) return false;                   // Retourne false si inexistant

            _context.Articles.Remove(article);                  // Supprime l'article
            return await _context.SaveChangesAsync() > 0;       // Retourne true si succès
        }
    }
}
