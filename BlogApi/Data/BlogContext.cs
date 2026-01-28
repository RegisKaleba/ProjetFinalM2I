using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}