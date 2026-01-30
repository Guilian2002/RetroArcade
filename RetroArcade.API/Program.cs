using Microsoft.Data.SqlClient;
using RetroArcade.Domain.Domain.Repositories;
using RetroArcade.Domain.Domain.Services;
using System.Data.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // API documentation
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "RetroArcade API",
        Version = "v1",
        Description = "Interface de contrôle de la grille RetroArcade"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Missing DefaultConnection");

builder.Services.AddScoped<DbConnection, SqlConnection>(_ => new SqlConnection(cs));
builder.Services.AddScoped<IAccountRepository, AccountService>();

// ==============================
// CORS (Cross-Origin Resource Sharing)
// ==============================
//
// Le CORS est un mécanisme de sécurité APPLIQUÉ PAR LE NAVIGATEUR.
// Il empêche une page web (HTML/JS) hébergée sur une origine
// (schéma + domaine + port) d'appeler une API située sur une autre
// origine, sauf si le serveur l'autorise explicitement.
//
// Exemple :
// - MVC (navigateur) : http://localhost:7185
// - Web API          : http://localhost:7184
// → origines différentes ⇒ CORS bloqué par défaut
//
// Ce middleware indique AU NAVIGATEUR quelles origines
// ont le droit d'appeler l'API.
//

builder.Services.AddCors(opt =>
{
    // Déclaration d'une policy CORS nommée "MvcCors"
    opt.AddPolicy("MvcCors", p =>
    {
        // Origines autorisées à appeler l'API
        // Une origine = scheme + host + port
        //
        // Exemple :
        //   http://localhost:5188
        //   https://localhost:5188
        //
        // IMPORTANT :
        // - Ce n'est PAS une URL complète
        // - Pas de /api, pas de path
        // - Le port DOIT correspondre exactement
        //
        var origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        p.WithOrigins(origins)

         // Autorise tous les headers HTTP envoyés par le navigateur
         // Exemples :
         // - Content-Type
         // - Authorization (JWT)
         // - Accept
         //
         // Sans ça :
         // → les requêtes POST/PUT avec JSON sont bloquées
         .AllowAnyHeader()

         // Autorise toutes les méthodes HTTP
         // GET, POST, PUT, DELETE, OPTIONS...
         //
         // Sans ça :
         // → seules les requêtes GET fonctionnent
         //
         // Le navigateur fait aussi une requête "OPTIONS"
         // (préflight) avant certains appels
         .AllowAnyMethod();
        //.WithMethods("GET", "POST", "PUT");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
