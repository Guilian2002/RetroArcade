using RetroArcade.ASPCore.Models.RetroArcade;

namespace RetroArcade.ASPCore.Clients
{
    public class RetroArcadeAPIClient
    {
        private readonly HttpClient _http;

        public RetroArcadeAPIClient(HttpClient http)
        {
            _http = http;
        }

        #region Building CRUD
        public async Task<ICollection<BuildingViewModel>> GetAllBuildingsAsync()
        {
            var response = await _http.GetAsync("/api/buildings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ICollection<BuildingViewModel>>();
                return content!.ToList() ?? throw new ArgumentNullException("Pas de batiments");
            }

            throw new ArgumentNullException("Pas de batiments");
        }
        #endregion
    }
}
