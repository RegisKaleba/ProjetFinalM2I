using System;
using System.Collections.Generic;
using System.Linq;
using BlogConsole.Models;

namespace BlogConsole.Services
{
    public class CommentService
    {
        private List<Comment> _comments = new List<Comment>();
        private int _nextId = 1;

        // Create
        public Comment Create(int articleId, string author, string content)
        {
            var comment = new Comment(_nextId++, articleId, author, content);
            _comments.Add(comment);
            return comment;
        }

        // Read all comments for an article
        public List<Comment> GetByArticleId(int articleId)
        {
            return _comments.Where(c => c.ArticleId == articleId).ToList();
        }

        // Read by Id
        public Comment GetById(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        // Update
        public bool Update(int id, string newContent)
        {
            var comment = GetById(id);
            if (comment == null) return false;

            comment.Content = newContent;
            return true;
        }

        // Delete
        public bool Delete(int id)
        {
            var comment = GetById(id);
            if (comment == null) return false;

            _comments.Remove(comment);
            return true;
        }
    }
}
