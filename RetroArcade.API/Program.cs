using Microsoft.Data.SqlClient;
using RetroArcade.Domain.Domain.Repositories;
using RetroArcade.Domain.Domain.Services;
using System.Data.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RetroArcade.API.JWT;
using RetroArcade.API.JWT.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICES DE BASE ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- BASE DE DONNÉES ---
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Missing DefaultConnection");

builder.Services.AddScoped<DbConnection, SqlConnection>(_ => new SqlConnection(cs));

// --- REPOSITORIES / SERVICES ---
builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<IBuildingRepository, BuildingService>();
builder.Services.AddScoped<IRoomRepository, RoomService>();
builder.Services.AddScoped<ITokenManager, TokenManager>();

// ==========================================
// CONFIGURATION AUTHENTIFICATION JWT
// ==========================================
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        }
    };
});

// ==============================
// CORS
// ==============================
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MvcCors", p =>
    {
        var origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        p.WithOrigins(origins)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials();
    });
});

var app = builder.Build();

// --- MIDDLEWARE PIPELINE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MvcCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();