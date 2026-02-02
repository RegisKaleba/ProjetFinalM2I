using BlogApi.Data;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les controllers
builder.Services.AddControllers();

// Connexion MySQL depuis appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 44));

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Injection de dépendances
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<CommentService>();

var app = builder.Build();

app.UseHttpsRedirection();

// Ajouter un préfixe global "api/v1" pour tous les controllers
app.MapControllerRoute(
    name: "v1",
    pattern: "api/v1/{controller}/{action=Index}/{id?}");

app.Run();
