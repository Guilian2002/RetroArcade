using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomFeedback;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<BuildingViewModel> GetBuildingDetailsAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<BuildingViewModel>($"api/buildings/{id}");
                return result ?? throw new Exception("Bâtiment introuvable.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement du bâtiment : " + ex.Message);
            }
        }

        public async Task AddBuildingAsync(AddBuildingForm form)
        {
            var response = await _http.PostAsJsonAsync("api/buildings", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error ?? "Erreur lors de la création.");
            }
        }

        public async Task UpdateBuildingAsync(Guid id, UpdateBuildingForm form)
        {
            var response = await _http.PutAsJsonAsync($"api/buildings/{id}", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error ?? "Erreur lors de la mise à jour.");
            }
        }

        public async Task DeleteBuildingAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/buildings/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error ?? "Erreur lors de la suppression du bâtiment.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Room CRUD
        public async Task<ICollection<RoomViewModel>> GetRoomsByBuildingAsync(Guid buildingId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<RoomViewModel>>($"api/rooms/{buildingId}");
                return result ?? new List<RoomViewModel>();
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la récupération des salles.");
            }
        }

        public async Task<RoomViewModel> GetRoomDetailsAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<RoomViewModel>($"api/rooms/details/{id}");
                return result ?? throw new Exception("Détails de la salle introuvables.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des détails : " + ex.Message);
            }
        }

        public async Task AddRoomAsync(AddRoomForm form)
        {
            var response = await _http.PostAsJsonAsync("api/rooms", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        public async Task UpdateRoomAsync(Guid id, UpdateRoomForm form)
        {
            var response = await _http.PutAsJsonAsync($"api/rooms/{id}", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task DeleteRoomAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/rooms/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Impossible de supprimer la salle.");
            }
        }

        #endregion
        #region Booking CRUD
        public async Task UpdateBookingAsync(Guid id, UpdateBookingForm form)
        {
            var response = await _http.PutAsJsonAsync($"api/bookings/{id}", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error ?? "Erreur lors de la modification de la réservation.");
            }
        }

        public async Task<BookingViewModel> GetBookingByIdAsync(Guid id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<BookingViewModel>($"api/bookings/{id}");
                return result ?? throw new Exception("Réservation introuvable.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération : " + ex.Message);
            }
        }

        public async Task<ICollection<BookingViewModel>> GetBookingsByManagerAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<BookingViewModel>>("api/bookings/bymanager");
                return result ?? new List<BookingViewModel>();
            }
            catch (Exception)
            {
                throw new Exception("Impossible de charger les réservations du complexe.");
            }
        }

        public async Task<ICollection<BookingViewModel>> GetAllBookingsByUserAsync()
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var result = await _http.GetFromJsonAsync<ICollection<BookingViewModel>>("api/bookings", options);
                return result ?? new List<BookingViewModel>();
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"Erreur de format de données : {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible de récupérer vos réservations.");
            }
        }

        public async Task DeleteBookingAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/bookings/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task AddBookingAsync(AddBookingForm form)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/bookings", form);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erreur lors de la réservation : {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Categorie CRUD
        public async Task AddCategorieAsync(AddCategorieForm form)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/categories", form);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<CategorieViewModel>> GetAllCategoriesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<CategorieViewModel>>("api/categories");
                return result ?? new List<CategorieViewModel>();
            }
            catch (Exception)
            {
                throw new Exception("Impossible de charger les catégories.");
            }
        }

        public async Task DeleteCategorieAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/categories/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        #endregion
        #region ArcadeMachine CRUD
        public async Task<ICollection<ArcadeMachineViewModel>> GetAllArcadeMachinesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ICollection<ArcadeMachineViewModel>>("api/arcademachines");
                return result ?? new List<ArcadeMachineViewModel>();
            }
            catch (Exception)
            {
                throw new Exception("Impossible de charger le catalogue des machines.");
            }
        }

        public async Task<ArcadeMachineViewModel> GetArcadeMachineByIdAsync(Guid id)
        {
            var result = await _http.GetFromJsonAsync<ArcadeMachineViewModel>($"api/arcademachines/{id}");
            return result ?? throw new Exception("Machine introuvable.");
        }

        public async Task AddArcadeMachineAsync(AddArcadeMachineForm form)
        {
            var response = await _http.PostAsJsonAsync("api/arcademachines", form);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task UpdateArcadeMachineAsync(Guid id, UpdateArcadeMachineForm form)
        {
            var response = await _http.PutAsJsonAsync($"api/arcademachines/{id}", form);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task DeleteArcadeMachineAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/arcademachines/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        #endregion

        #region RoomArcadeMachine CRUD
        public async Task AddRoomArcadeMachineAsync(AddRoomArcadeMachineForm form)
        {
            var response = await _http.PostAsJsonAsync("api/roomarcademachines", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task DeleteRoomArcadeMachineAsync(Guid roomId, Guid machineId)
        {
            var response = await _http.DeleteAsync($"api/roomarcademachines/{roomId}/{machineId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Erreur lors du retrait de la machine.");
        }
        #endregion

        #region Room Feedback CRUD
        public async Task AddRoomFeedbackAsync(AddRoomFeedbackForm form)
        {
            var response = await _http.PostAsJsonAsync("api/roomfeedbacks", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task DeleteRoomFeedbackAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/roomfeedbacks/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Erreur lors du retrait de la machine.");
        }
        #endregion
    }
}
