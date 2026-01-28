using System;
using System.Collections.Generic;
using System.Linq;
using BlogConsole.Models;

namespace BlogConsole.Services
{
    public class ArticleService
    {
        private List<Article> _articles = new List<Article>();
        private int _nextId = 1;

        // Create
        public Article Create(string title, string content)
        {
            var article = new Article(_nextId++, title, content);
            _articles.Add(article);
            return article;
        }

        // Read all
        public List<Article> GetAll()
        {
            return _articles;
        }

        // Read by Id
        public Article GetById(int id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        // Update
        public bool Update(int id, string newTitle, string newContent)
        {
            var article = GetById(id);
            if (article == null) return false;

            article.Title = newTitle;
            article.Content = newContent;
            article.UpdatedAt = DateTime.Now;
            return true;
        }

        // Delete
        public bool Delete(int id)
        {
            var article = GetById(id);
            if (article == null) return false;

            _articles.Remove(article);
            return true;
        }
    }
}
