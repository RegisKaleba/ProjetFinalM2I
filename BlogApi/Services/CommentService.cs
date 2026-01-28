using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class CommentService
    {
        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        // Récupérer tous les commentaires d'un article
        public async Task<List<Comment>> GetCommentsByArticleIdAsync(int articleId)
        {
            return await _context.Comments
                .Where(c => c.ArticleId == articleId)
                .ToListAsync();
        }

        // Récupérer un commentaire par son Id
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.Article)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Créer un nouveau commentaire
        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        // Supprimer un commentaire
        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Optionnel : mettre à jour un commentaire
        public async Task<bool> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
