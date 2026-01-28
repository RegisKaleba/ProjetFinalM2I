using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.DTOs
{
    public class UpdateArticleDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
