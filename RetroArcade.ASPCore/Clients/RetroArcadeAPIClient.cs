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

        public async Task<BuildingViewModel> GetBuildingByIdAsync(Guid id)
        {
            var response = await _http.GetAsync($"/api/buildings/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<BuildingViewModel>();
                return content ?? throw new ArgumentNullException("Pas de bâtiment");
            }

            throw new ArgumentNullException("Pas de bâtiment");
        }
        #endregion
        #region Room CRUD
        public async Task<ICollection<RoomViewModel>> GetAllRoomsByBuildingAsync(Guid id)
        {
            var response = await _http.GetAsync($"/api/rooms/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ICollection<RoomViewModel>>();
                return content!.ToList() ?? throw new ArgumentNullException("Pas de salles d'arcades");
            }

            throw new ArgumentNullException("Pas de salles d'arcades");
        }
        #endregion
    }
}
