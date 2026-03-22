using Blazored.LocalStorage;
using RetroArcade.BlazorWebAssembly.Models.Authentification;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var response = await _http.PostAsJsonAsync("api/accounts/login", new { email, password });

            return response.IsSuccessStatusCode;
        }

        public async Task<AccountInfo?> GetMeAsync()
        {
            try
            {
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
            await _http.PostAsync("api/accounts/logout", null);

            await _localStorage.RemoveItemAsync(SessionKey);
        }

        #region Admin Account CRUD
        public async Task<ICollection<AccountDetailsViewModel>> GetAllAccountsAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());

                var result = await _http.GetFromJsonAsync<ICollection<AccountDetailsViewModel>>("api/accounts", options);
                return result ?? new List<AccountDetailsViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR DÉTAILLÉE : {ex}");
                throw new Exception($"Erreur : {ex.Message}");
            }
        }


        public async Task<AccountDetailsViewModel> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());

                var result = await _http.GetFromJsonAsync<AccountDetailsViewModel>($"api/accounts/{id}", options);

                if (result == null)
                    throw new Exception("Le compte récupéré est vide.");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur désérialisation : {ex.Message}");
                throw new Exception($"Impossible de charger le compte : {ex.Message}");
            }
        }

        public async Task UpdateAccountAsync(Guid id, UpdateAccountForm form)
        {
            var response = await _http.PutAsJsonAsync($"api/accounts/{id}", form);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/accounts/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Erreur lors de la suppression.");
        }
        #endregion
    }
}
