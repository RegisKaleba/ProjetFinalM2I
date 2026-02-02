(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

Application de blog en C# avec interface console et API REST.
Gère les articles et commentaires avec persistance via Entity Framework Core.

(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

🔹 Fonctionnalités
Console

Lister, créer, modifier et supprimer des articles

Ajouter et supprimer des commentaires

Menu interactif coloré

API REST

(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

----> Articles

GET /api/v1/articles → liste

GET /api/v1/articles/{id} → détails avec commentaires

POST /api/v1/articles → création

PUT /api/v1/articles/{id} → mise à jour

DELETE /api/v1/articles/{id} → suppression

----> Commentaires

POST /api/v1/articles/{articleId}/comments → ajout

GET /api/v1/comments/{id} → récupération

DELETE /api/v1/comments/{id} → suppression

(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

Exemple post :

POST /api/v1/articles
{
  "title": "Mon premier article",
  "content": "Contenu de l'article"
}

POST /api/v1/articles/1/comments
{
  "author": "Alice",
  "content": "Super article !"
}

(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

🔹 Technologies

C# / .NET 7

Entity Framework Core

SQLite (ou autre DB configurée)

ASP.NET Core Web API

(>'-')>  <('-'<)  ^('-')^  v('-')v  (>'-')>  <('-'<)  ^('-')^  v('-')v

🔹 Auteur

Nom : Régis Kaléba

Email : regis.kaleba@orange.fr

