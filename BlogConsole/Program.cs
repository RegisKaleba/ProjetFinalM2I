using System;
using BlogConsole.Models;
using BlogConsole.Services;

class Program
{
    static ArticleService articleService = new ArticleService();
    static CommentService commentService = new CommentService();

    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== BLOG CONSOLE ===\n");
            Console.WriteLine("1. Lister les articles");
            Console.WriteLine("2. Créer un article");
            Console.WriteLine("3. Voir un article");
            Console.WriteLine("4. Modifier un article");
            Console.WriteLine("5. Supprimer un article");
            Console.WriteLine("6. Ajouter un commentaire");
            Console.WriteLine("7. Supprimer un commentaire");
            Console.WriteLine("0. Quitter");
            Console.Write("\nVotre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": ListArticles(); break;
                case "2": CreateArticle(); break;
                case "3": ViewArticle(); break;
                case "4": UpdateArticle(); break;
                case "5": DeleteArticle(); break;
                case "6": AddComment(); break;
                case "7": DeleteComment(); break;
                case "0": exit = true; break;
                default:
                    Console.WriteLine("Choix invalide !"); break;
            }

            if (!exit)
            {
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }

    // =========================
    // Fonctions du menu
    // =========================

    static void ListArticles()
    {
        var articles = articleService.GetAll();
        if (articles.Count == 0)
        {
            Console.WriteLine("Aucun article trouvé.");
            return;
        }

        foreach (var article in articles)
        {
            Console.WriteLine(article);
            Console.WriteLine("---------------------------");
        }
    }

    static void CreateArticle()
    {
        Console.Write("Titre : ");
        string title = Console.ReadLine();
        Console.Write("Contenu : ");
        string content = Console.ReadLine();

        var article = articleService.Create(title, content);
        Console.WriteLine($"Article créé avec ID {article.Id}");
    }

    static void ViewArticle()
    {
        Console.Write("ID de l'article : ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var article = articleService.GetById(id);
            if (article == null) { Console.WriteLine("Article non trouvé."); return; }

            Console.WriteLine(article);
            Console.WriteLine("\nCommentaires :");
            foreach (var c in commentService.GetByArticleId(id))
                Console.WriteLine(c);
        }
        else Console.WriteLine("ID invalide !");
    }

    static void UpdateArticle()
    {
        Console.Write("ID de l'article à modifier : ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var article = articleService.GetById(id);
            if (article == null) { Console.WriteLine("Article non trouvé."); return; }

            Console.Write("Nouveau titre : ");
            string title = Console.ReadLine();
            Console.Write("Nouveau contenu : ");
            string content = Console.ReadLine();

            articleService.Update(id, title, content);
            Console.WriteLine("Article modifié !");
        }
        else Console.WriteLine("ID invalide !");
    }

    static void DeleteArticle()
    {
        Console.Write("ID de l'article à supprimer : ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (articleService.Delete(id))
            {
                // Supprimer aussi les commentaires liés
                foreach (var c in commentService.GetByArticleId(id))
                    commentService.Delete(c.Id);

                Console.WriteLine("Article et ses commentaires supprimés !");
            }
            else Console.WriteLine("Article non trouvé !");
        }
        else Console.WriteLine("ID invalide !");
    }

    static void AddComment()
    {
        Console.Write("ID de l'article : ");
        if (int.TryParse(Console.ReadLine(), out int articleId))
        {
            var article = articleService.GetById(articleId);
            if (article == null) { Console.WriteLine("Article non trouvé."); return; }

            Console.Write("Auteur : ");
            string author = Console.ReadLine();
            Console.Write("Contenu : ");
            string content = Console.ReadLine();

            var comment = commentService.Create(articleId, author, content);
            article.AddComment(comment);

            Console.WriteLine($"Commentaire ajouté avec ID {comment.Id}");
        }
        else Console.WriteLine("ID invalide !");
    }

    static void DeleteComment()
    {
        Console.Write("ID du commentaire : ");
        if (int.TryParse(Console.ReadLine(), out int commentId))
        {
            var comment = commentService.GetById(commentId);
            if (comment == null) { Console.WriteLine("Commentaire non trouvé."); return; }

            if (commentService.Delete(commentId))
            {
                // Supprimer aussi de l'article
                var article = articleService.GetById(comment.ArticleId);
                article?.Comments.RemoveAll(c => c.Id == commentId);

                Console.WriteLine("Commentaire supprimé !");
            }
        }
        else Console.WriteLine("ID invalide !");
    }
}
