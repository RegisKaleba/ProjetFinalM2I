using BlogApi.Models;        
using BlogApi.Models.DTOs;   
using BlogApi.Services;      
using Microsoft.AspNetCore.Mvc;  

namespace BlogApi.Controllers
{
    [ApiController]  // Indique que c’est un contrôleur d’API (validation automatique des modèles, etc.)
    [Route("api/v1")]  // Préfixe général des routes pour ce contrôleur
    public class CommentsController : ControllerBase  // ControllerBase = API sans vues MVC
    {
        private readonly CommentService _commentService;  // Service métier injecté

        // Constructeur avec injection de dépendances
        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // =====================
        // POST /api/v1/articles/{articleId}/comments
        // Ajoute un commentaire à un article spécifique
        // =====================
        [HttpPost("articles/{articleId}/comments")]
        public async Task<ActionResult<CommentDto>> AddComment(
            int articleId,             // ID de l'article auquel on ajoute le commentaire
            CreateCommentDto dto)       // DTO contenant l'auteur et le contenu
        {
            var comment = new Comment
            {
                ArticleId = articleId,   // Associe le commentaire à l'article
                Author = dto.Author,     // Auteur du commentaire
                Content = dto.Content,   // Contenu
                CreatedAt = DateTime.Now // Date de création
            };

            // Appel du service pour créer le commentaire
            var created = await _commentService.CreateAsync(articleId, comment);

            if (created == null)  // Si l'article n'existe pas
                return NotFound("Article inexistant");

            // Transformation en DTO pour la réponse
            var result = new CommentDto
            {
                Id = created.Id,
                Author = created.Author,
                Content = created.Content,
                CreatedAt = created.CreatedAt
            };

            // Retourne un 201 Created avec lien vers le commentaire créé
            return CreatedAtAction(
                nameof(GetCommentById), // Nom de l'action GET pour ce commentaire
                new { id = created.Id }, // Route values pour créer l'URL
                result);
        }

        // =====================
        // GET /api/v1/comments/{id}
        // Récupère un commentaire par son ID
        // =====================
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);  // Recherche par ID
            if (comment == null) return NotFound();                // 404 si inexistant

            // Transformation en DTO
            var dto = new CommentDto
            {
                Id = comment.Id,
                Author = comment.Author,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return Ok(dto);  // Retourne 200 OK
        }

        // =====================
        // DELETE /api/v1/comments/{id}
        // Supprime un commentaire par son ID
        // =====================
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleted = await _commentService.DeleteAsync(id);  // Appel du service pour supprimer
            if (!deleted) return NotFound();                      // 404 si le commentaire n'existe pas
            return NoContent();                                   // 204 No Content si supprimé
        }
    }
}
