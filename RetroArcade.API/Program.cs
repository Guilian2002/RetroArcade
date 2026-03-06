using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RetroArcade.API.JWT;
using RetroArcade.API.JWT.Interfaces;
using RetroArcade.Domain.Domain.Commands.AccountCommands.Validators;
using RetroArcade.Domain.Domain.Commands.BookingCommands.Validators;
using RetroArcade.Domain.Domain.Repositories;
using RetroArcade.Domain.Domain.Services;
using System.Data.Common;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICES DE BASE ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetroArcade API", Version = "v1" });

    c.AddSecurityDefinition("CookieAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie,
        Name = "jwt",
        Description = "Authentifiez-vous via l'endpoint Login. Le cookie 'jwt' sera utilisé automatiquement."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "CookieAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// --- BASE DE DONNÉES ---
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Missing DefaultConnection");

builder.Services.AddScoped<DbConnection, SqlConnection>(_ => new SqlConnection(cs));

// --- REPOSITORIES / SERVICES ---
builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<IBuildingRepository, BuildingService>();
builder.Services.AddScoped<IRoomRepository, RoomService>();
builder.Services.AddScoped<IBookingRepository, BookingService>();
builder.Services.AddScoped<IArcadeMachineRepository, ArcadeMachineService>();
builder.Services.AddScoped<ITokenManager, TokenManager>();

// --- VALIDATORS ---
builder.Services.AddValidatorsFromAssemblyContaining<AddAccountCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddBookingCommandValidator>();

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