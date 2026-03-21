using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Room
{
    public partial class SeeAllRooms
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid BuildingId { get; set; }

        private List<RoomViewModel>? _rooms;
        private bool _isLoading = true;
        private string? _errorMessage;

        private bool showDeleteModal = false;
        private RoomViewModel? roomToDelete;

        protected override async Task OnInitializedAsync()
        {
            await LoadRooms();
        }

        private async Task LoadRooms()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                var result = await ApiClient.GetRoomsByBuildingAsync(BuildingId);
                _rooms = result?.ToList() ?? new List<RoomViewModel>();
            }
            catch (Exception)
            {
                _errorMessage = "ERREUR SYSTÈME : Accès aux secteurs impossible.";
            }
            finally { _isLoading = false; }
        }

        private void OpenDeleteModal(RoomViewModel room)
        {
            roomToDelete = room;
            showDeleteModal = true;
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
            roomToDelete = null;
        }

        private async Task ConfirmDelete()
        {
            if (roomToDelete != null)
            {
                try
                {
                    await ApiClient.DeleteRoomAsync(roomToDelete.Id);
                    showDeleteModal = false;
                    await LoadRooms();
                }
                catch (Exception)
                {
                    _errorMessage = "CRITICAL ERROR : Impossible d'effacer le secteur.";
                    showDeleteModal = false;
                }
            }
        }
    }
}
