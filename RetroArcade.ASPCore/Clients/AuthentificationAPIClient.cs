using System.Text.Json;
using System.Text;
using RetroArcade.ASPCore.Models.Authentification;

namespace RetroArcade.ASPCore.Clients
{
    public class AuthentificationAPIClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthentificationAPIClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var content = new StringContent(JsonSerializer.Serialize(
                new 
                { 
                    email, 
                    password 
                }), 
                Encoding.UTF8, 
                "application/json"
            );

            var response = await _http.PostAsync("api/accounts/login", content);

            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
                {
                    foreach (var cookie in cookies)
                    {
                        _httpContextAccessor.HttpContext?.Response.Headers.Append("Set-Cookie", cookie);
                    }
                }
                return true;
            }

            return false;
        }

        public async Task<AccountInfoDTO?> GetMeAsync()
        {
            var jwt = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];

            var request = new HttpRequestMessage(HttpMethod.Get, "api/accounts/me");
            if (!string.IsNullOrEmpty(jwt))
                request.Headers.Add("Cookie", $"jwt={jwt}");

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<AccountInfoDTO>();
        }

        public async Task<bool> AccountCreateAsync(string firstname, string lastname, string username,
            string email, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    firstname,
                    lastname,
                    username,
                    email,
                    password,
                    role = 0
                }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("api/accounts", content);

            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            var jwt = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];

            var request = new HttpRequestMessage(HttpMethod.Post, "api/accounts/logout");

            if (!string.IsNullOrEmpty(jwt))
            {
                request.Headers.Add("Cookie", $"jwt={jwt}");
            }

            var response = await _http.SendAsync(request);

            if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
            {
                foreach (var cookie in cookies)
                {
                    _httpContextAccessor.HttpContext?.Response.Headers.Append("Set-Cookie", cookie);
                }
            }
        }
    }
}
