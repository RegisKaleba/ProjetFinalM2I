using BlogApi.Models;
using BlogApi.Models.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // POST /api/articles/{articleId}/comments
        [HttpPost("articles/{articleId}/comments")]
        public async Task<ActionResult<CommentDto>> AddComment(
            int articleId,
            CreateCommentDto dto)
        {
            var comment = new Comment
            {
                ArticleId = articleId,
                Author = dto.Author,
                Content = dto.Content,
                CreatedAt = DateTime.Now
            };

            var created = await _commentService.CreateAsync(comment);

            var result = new CommentDto
            {
                Id = created.Id,
                Author = created.Author,
                Content = created.Content,
                CreatedAt = created.CreatedAt
            };

            return CreatedAtAction(nameof(GetCommentById),
                new { id = created.Id },
                result);
        }

        // GET /api/comments/{id}
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null) return NotFound();

            var dto = new CommentDto
            {
                Id = comment.Id,
                Author = comment.Author,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return Ok(dto);
        }

        // DELETE /api/comments/{id}
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleted = await _commentService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
