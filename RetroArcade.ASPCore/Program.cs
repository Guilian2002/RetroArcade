using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddHttpContextAccessor();
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? throw new Exception("API URL manquante");

builder.Services.AddHttpClient<AuthentificationAPIClient>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<RetroArcadeAPIClient>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped<SessionCheckFilter>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<SessionCheckFilter>();
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();

// Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
