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
                var result = await _http.GetFromJsonAsync<ICollection<AccountDetailsViewModel>>("api/accounts");
                return result ?? new List<AccountDetailsViewModel>();
            }
            catch { throw new Exception("Erreur lors de la récupération des comptes."); }
        }


        public async Task<AccountDetailsViewModel> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<AccountDetailsViewModel>($"api/accounts/{id}");

                if (result == null)
                {
                    throw new Exception("Le compte récupéré est vide.");
                }

                return result;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Impossible de trouver ce compte ou l'identifiant est invalide.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur est survenue : {ex.Message}");
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
