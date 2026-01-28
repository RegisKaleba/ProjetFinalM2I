using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.DTOs
{
    public class CreateCommentDto
    {
        public string Author { get; set; }
        public string Content { get; set; }
    }
}