using BlogApi.Data;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les controllers
builder.Services.AddControllers();

// Ajouter Swagger / OpenAPI
builder.Services.AddOpenApi();

//  Connexion MySQL
var connectionString = "Server=localhost;Database=BlogDB;Uid=root;Pwd=root;port=3306";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 44));

NewMethod(builder, connectionString, serverVersion);

//  ENREGISTRER LES SERVICES pour l'injection de dépendances
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<CommentService>();

var app = builder.Build();

// Swagger en développement
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Méthode pour configurer DbContext
static void NewMethod(WebApplicationBuilder builder, string connectionString, MySqlServerVersion serverVersion)
{
    builder.Services.AddDbContext<BlogContext>(options =>
        options.UseMySql(connectionString, serverVersion));
}
