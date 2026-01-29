using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        // =====================
        // Les DbSet
        // =====================
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // =====================
        // Configurer les relations
        // =====================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)      // un Comment appartient à un Article
                .WithMany(a => a.Comments)   // un Article peut avoir plusieurs Comments
                .HasForeignKey(c => c.ArticleId); // clé étrangère dans Comment
        }
    }
}
