using BlogApi.Models;
using BlogApi.Models.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/v1/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _service;

        public ArticlesController(ArticleService service)
        {
            _service = service;
        }

        // GET /api/v1/articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleListDto>>> GetAll()
        {
            var articles = await _service.GetAllAsync();
            var dtos = articles.Select(a => new ArticleListDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Ok(dtos);
        }

        // GET /api/v1/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Get(int id)
        {
            var article = await _service.GetByIdAsync(id);
            if (article == null) return NotFound();

            var dto = new ArticleDetailsDto
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

            return Ok(dto);
        }

        // POST /api/v1/articles
        [HttpPost]
        public async Task<ActionResult<ArticleDetailsDto>> Create(CreateArticleDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.Now
            };

            var created = await _service.CreateAsync(article);

            var result = new ArticleDetailsDto
            {
                Id = created.Id,
                Title = created.Title,
                Content = created.Content,
                CreatedAt = created.CreatedAt,
                Comments = new()
            };

            return CreatedAtAction(nameof(Get), new { id = created.Id }, result);
        }

        // PUT /api/v1/articles/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Update(int id, UpdateArticleDto dto)
        {
            var article = await _service.GetByIdAsync(id);
            if (article == null) return NotFound();

            article.Title = dto.Title;
            article.Content = dto.Content;
            article.UpdatedAt = DateTime.Now;

            await _service.UpdateAsync(article);

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

            return Ok(result);
        }

        // DELETE /api/v1/articles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
