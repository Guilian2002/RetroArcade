using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using RetroArcade.BlazorWebAssembly;
using RetroArcade.BlazorWebAssembly.Clients;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Services de base
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// 2. Handler pour envoyer les Cookies (JWT) à l'API
builder.Services.AddTransient<CookieHandler>();

// 3. Configuration des Clients avec le CookieHandler
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7184/";

builder.Services.AddHttpClient<AuthentificationAPIClient>(client => {
client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddHttpClient<RetroArcadeAPIClient>(client => {
client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler<CookieHandler>();

// 4. HttpClient par défaut
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

// --- CLASSE INTERNE POUR LES COOKIES ---
public class CookieHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}