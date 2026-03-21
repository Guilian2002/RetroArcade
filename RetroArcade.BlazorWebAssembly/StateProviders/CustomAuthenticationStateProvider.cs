using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthentificationAPIClient _authClient;
    private const string SessionKey = "SessionAccount";

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage, AuthentificationAPIClient authClient)
    {
        _localStorage = localStorage;
        _authClient = authClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // 1. On tente de récupérer les infos user du cache local
        var account = await _localStorage.GetItemAsync<AccountInfo>(SessionKey);

        // 2. Si pas de cache, on demande à l'API (qui vérifiera le cookie JWT)
        if (account == null)
        {
            account = await _authClient.GetMeAsync();
            if (account != null)
                await _localStorage.SetItemAsync(SessionKey, account);
        }

        if (account == null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        // 3. Création de l'identité pour Blazor (Rôles pour Building/Room)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Username),
            new Claim(ClaimTypes.Role, account.Role)
        };

        var identity = new ClaimsIdentity(claims, "ServerAuth");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyUserChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
