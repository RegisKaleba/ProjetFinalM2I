using BlogApi.Models;        
using BlogApi.Models.DTOs;   
using BlogApi.Services;      
using Microsoft.AspNetCore.Mvc;  

namespace BlogApi.Controllers
{
    [ApiController]  // Indique que c’est un contrôleur d’API (automatiquement valide les modèles, etc.)
    [Route("api/v1/articles")]  // Préfixe des routes pour ce contrôleur
    public class ArticlesController : ControllerBase  // ControllerBase = API sans vues MVC
    {
        private readonly ArticleService _service;  // Service métier injecté

        // Constructeur avec injection de dépendances
        public ArticlesController(ArticleService service)
        {
            _service = service;
        }

        // =====================
        // GET /api/v1/articles
        // Récupère la liste de tous les articles
        // =====================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleListDto>>> GetAll()
        {
            var articles = await _service.GetAllAsync();  // Appel service pour récupérer les articles
            var dtos = articles.Select(a => new ArticleListDto  // Transformation en DTO (ne garde que les champs utiles)
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Ok(dtos); // Retourne un 200 OK avec la liste
        }

        // =====================
        // GET /api/v1/articles/{id}
        // Récupère un article spécifique + ses commentaires
        // =====================
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Get(int id)
        {
            var article = await _service.GetByIdAsync(id);  // Recherche par ID
            if (article == null) return NotFound();        // 404 si inexistant

            var dto = new ArticleDetailsDto  // Création du DTO détaillé
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                Comments = article.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return Ok(dto); // Retourne 200 OK
        }

        // =====================
        // POST /api/v1/articles
        // Crée un nouvel article
        // =====================
        [HttpPost]
        public async Task<ActionResult<ArticleDetailsDto>> Create(CreateArticleDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.Now  // Date de création
            };

            var created = await _service.CreateAsync(article);  // Création via le service

            var result = new ArticleDetailsDto  // Transformation en DTO pour réponse
            {
                Id = created.Id,
                Title = created.Title,
                Content = created.Content,
                CreatedAt = created.CreatedAt,
                Comments = new() // Liste vide pour un nouvel article
            };

            // CreatedAtAction renvoie un 201 Created + URL de l’article créé
            return CreatedAtAction(nameof(Get), new { id = created.Id }, result);
        }

        // =====================
        // PUT /api/v1/articles/{id}
        // Mets à jour un article existant
        // =====================
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Update(int id, UpdateArticleDto dto)
        {
            var article = await _service.GetByIdAsync(id);  // Recherche article
            if (article == null) return NotFound();        // 404 si inexistant

            // Mise à jour des champs
            article.Title = dto.Title;
            article.Content = dto.Content;
            article.UpdatedAt = DateTime.Now;              // Date de mise à jour

            await _service.UpdateAsync(article);          // Sauvegarde via service

            // DTO pour réponse
            var result = new ArticleDetailsDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                Comments = article.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return Ok(result);  // Retourne 200 OK avec les détails mis à jour
        }

        // =====================
        // DELETE /api/v1/articles/{id}
        // Supprime un article
        // =====================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id); // Appel service pour supprimer
            if (!deleted) return NotFound();             // 404 si l’article n’existe pas
            return NoContent();                           // 204 No Content si supprimé
        }
    }
}
