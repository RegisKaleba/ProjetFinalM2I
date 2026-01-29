using BlogConsole.Services;
using BlogConsole.Models;

namespace BlogConsole.UI;

public class ConsoleMenu
{
    private readonly ArticleService articleService;
    private readonly CommentService commentService;

    public ConsoleMenu()
    {
        articleService = new ArticleService();
        commentService = new CommentService();
    }

    public void Run()
    {
        bool exit = false;

        while (!exit)
        {
            ConsoleHelper.Clear();
            DrawHeader("BLOG CONSOLE");

            Console.WriteLine("1. Lister les articles");
            Console.WriteLine("2. Créer un article");
            Console.WriteLine("3. Voir un article");
            Console.WriteLine("4. Modifier un article");
            Console.WriteLine("5. Supprimer un article");
            Console.WriteLine("6. Ajouter un commentaire");
            Console.WriteLine("7. Supprimer un commentaire");
            Console.WriteLine("0. Quitter");

            Console.Write("\nVotre choix : ");
            string choice = Console.ReadLine()?.Trim() ?? "";

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
                    ConsoleHelper.WriteError("[ERR] Choix invalide !");
                    break;
            }

            if (!exit) ConsoleHelper.Pause();
        }
    }

    // =====================
    // Affichage stylé
    // =====================
    private void DrawHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===================================");
        Console.WriteLine($"        {title.ToUpper()}");
        Console.WriteLine("===================================\n");
        Console.ResetColor();
    }

    private void DrawSeparator()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 40));
        Console.ResetColor();
    }

    // =====================
    // Menu actions
    // =====================
    private void ListArticles()
    {
        var articles = articleService.GetAll();
        if (articles.Count == 0)
        {
            ConsoleHelper.WriteError("[INFO] Aucun article trouvé.");
            return;
        }

        DrawHeader("LISTE DES ARTICLES");

        foreach (var article in articles)
        {
            ConsoleHelper.WriteTitle(article.Title);
            Console.WriteLine($"ID : {article.Id}");
            Console.WriteLine($"Créé le : {article.CreatedAt}");
            if (article.UpdatedAt.HasValue)
                Console.WriteLine($"Dernière mise à jour : {article.UpdatedAt}");
            Console.WriteLine($"Contenu : {article.Content}");
            DrawSeparator();
        }
    }

    private void CreateArticle()
    {
        DrawHeader("CRÉER UN ARTICLE");

        string title = ConsoleHelper.ReadRequiredString("Titre : ");
        string content = ConsoleHelper.ReadRequiredString("Contenu : ");

        var article = articleService.Create(title, content);
        ConsoleHelper.WriteSuccess($"[OK] Article créé avec ID {article.Id}");
    }

    private void ViewArticle()
    {
        DrawHeader("VOIR UN ARTICLE");

        int id = ConsoleHelper.ReadInt("ID de l'article : ");
        var article = articleService.GetById(id);

        if (article == null)
        {
            ConsoleHelper.WriteError("[ERR] Article non trouvé.");
            return;
        }

        ConsoleHelper.WriteTitle(article.Title);
        Console.WriteLine($"ID : {article.Id}");
        Console.WriteLine($"Créé le : {article.CreatedAt}");
        if (article.UpdatedAt.HasValue)
            Console.WriteLine($"Dernière mise à jour : {article.UpdatedAt}");
        Console.WriteLine($"Contenu : {article.Content}");
        DrawSeparator();

        var comments = commentService.GetByArticleId(id);
        if (comments.Count == 0)
        {
            Console.WriteLine("[INFO] Aucun commentaire pour cet article.");
            return;
        }

        ConsoleHelper.WriteTitle("Commentaires");
        foreach (var c in comments)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{c.Id}] {c.Author} - {c.CreatedAt}");
            Console.ResetColor();
            Console.WriteLine(c.Content);
            DrawSeparator();
        }
    }

    private void UpdateArticle()
    {
        DrawHeader("MODIFIER UN ARTICLE");

        int id = ConsoleHelper.ReadInt("ID de l'article à modifier : ");
        var article = articleService.GetById(id);

        if (article == null)
        {
            ConsoleHelper.WriteError("[ERR] Article non trouvé.");
            return;
        }

        string title = ConsoleHelper.ReadRequiredString("Nouveau titre : ");
        string content = ConsoleHelper.ReadRequiredString("Nouveau contenu : ");

        articleService.Update(id, title, content);
        ConsoleHelper.WriteSuccess("[OK] Article modifié !");
    }

    private void DeleteArticle()
    {
        DrawHeader("SUPPRIMER UN ARTICLE");

        int id = ConsoleHelper.ReadInt("ID de l'article à supprimer : ");
        if (!articleService.Delete(id))
        {
            ConsoleHelper.WriteError("[ERR] Article non trouvé !");
            return;
        }

        foreach (var c in commentService.GetByArticleId(id))
            commentService.Delete(c.Id);

        ConsoleHelper.WriteSuccess("[OK] Article et ses commentaires supprimés !");
    }

    private void AddComment()
    {
        DrawHeader("AJOUTER UN COMMENTAIRE");

        int articleId = ConsoleHelper.ReadInt("ID de l'article : ");
        var article = articleService.GetById(articleId);

        if (article == null)
        {
            ConsoleHelper.WriteError("[ERR] Article non trouvé.");
            return;
        }

        string author = ConsoleHelper.ReadRequiredString("Auteur : ");
        string content = ConsoleHelper.ReadRequiredString("Contenu : ");

        var comment = commentService.Create(articleId, author, content);
        article.AddComment(comment);

        ConsoleHelper.WriteSuccess($"[OK] Commentaire ajouté avec ID {comment.Id}");
    }

    private void DeleteComment()
    {
        DrawHeader("SUPPRIMER UN COMMENTAIRE");

        int commentId = ConsoleHelper.ReadInt("ID du commentaire : ");
        var comment = commentService.GetById(commentId);

        if (comment == null)
        {
            ConsoleHelper.WriteError("[ERR] Commentaire non trouvé.");
            return;
        }

        commentService.Delete(commentId);
        var article = articleService.GetById(comment.ArticleId);
        article?.Comments.RemoveAll(c => c.Id == commentId);

        ConsoleHelper.WriteSuccess("[OK] Commentaire supprimé !");
    }
}
