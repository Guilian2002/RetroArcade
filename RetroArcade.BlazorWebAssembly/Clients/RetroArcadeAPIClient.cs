using RetroArcade.BlazorWebAssembly.Models.RetroArcade;
using System.Net.Http.Json;

namespace RetroArcade.BlazorWebAssembly.Clients
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
            // Utilisation de GetFromJsonAsync : plus court et gère la désérialisation
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<BuildingViewModel>>("api/buildings");
                return result ?? new List<BuildingViewModel>();
            }
            catch
            {
                throw new Exception("Impossible de récupérer les bâtiments.");
            }
        }

        public async Task<BuildingViewModel> GetBuildingByIdAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<BuildingViewModel>($"api/buildings/{id}");
                return result ?? throw new Exception("Bâtiment introuvable.");
            }
            catch
            {
                throw new Exception("Erreur lors de la récupération du bâtiment.");
            }
        }
        #endregion

        #region Room CRUD
        public async Task<ICollection<RoomViewModel>> GetAllRoomsByBuildingAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<RoomViewModel>>($"api/rooms/{id}");
                return result ?? new List<RoomViewModel>();
            }
            catch
            {
                throw new Exception("Impossible de récupérer les salles.");
            }
        }
        #endregion
    }
}
