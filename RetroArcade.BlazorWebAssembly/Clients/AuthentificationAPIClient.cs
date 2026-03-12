using Blazored.LocalStorage;
using RetroArcade.BlazorWebAssembly.Models.Authentification;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace RetroArcade.BlazorWebAssembly.Clients
{
    public class AuthentificationAPIClient
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private const string SessionKey = "SessionAccount";

        public AuthentificationAPIClient(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Utilisation de PostAsJsonAsync (plus simple que StringContent)
            var response = await _http.PostAsJsonAsync("api/accounts/login", new { email, password });

            // En Blazor WASM, on ne touche PAS aux headers Set-Cookie. 
            // Le navigateur s'en occupe tout seul si l'API est configurée avec CORS "AllowCredentials".
            return response.IsSuccessStatusCode;
        }

        public async Task<AccountInfo?> GetMeAsync()
        {
            try
            {
                // Pas besoin de chercher le JWT manuellement, le navigateur l'envoie via le cookie.
                var response = await _http.GetAsync("api/accounts/me");

                if (!response.IsSuccessStatusCode) return null;

                return await response.Content.ReadFromJsonAsync<AccountInfo>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AccountCreateAsync(string firstname, string lastname, string username,
            string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/accounts", new
            {
                firstname,
                lastname,
                username,
                email,
                password,
                role = 0
            });

            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            // 1. Prévenir l'API pour supprimer le cookie côté serveur
            await _http.PostAsync("api/accounts/logout", null);

            // 2. Nettoyer le LocalStorage côté client
            await _localStorage.RemoveItemAsync(SessionKey);
        }
    }
}
