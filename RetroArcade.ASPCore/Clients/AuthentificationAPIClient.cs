using System.Text.Json;
using System.Text;

namespace RetroArcade.ASPCore.Clients
{
    public class AuthentificationAPIClient
    {
        private readonly HttpClient _http;

        public AuthentificationAPIClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:7184/");
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

            var response = await _http.PostAsync("/api/accounts", content);

            return response.IsSuccessStatusCode;
        }
    }
}
