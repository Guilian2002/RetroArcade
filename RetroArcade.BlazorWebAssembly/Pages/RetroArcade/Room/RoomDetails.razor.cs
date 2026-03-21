using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Room
{
    public partial class RoomDetails
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Parameter] public Guid Id { get; set; }

        protected RoomViewModel? _room;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        // Logique de la modale
        private bool showDeleteModal = false;
        private Guid targetId;
        private string targetName = "";
        private bool isDeletingFeedback = false;

        protected override async Task OnInitializedAsync() => await LoadData();

        private async Task LoadData()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                _room = await ApiClient.GetRoomDetailsAsync(Id);
            }
            catch (Exception) { _errorMessage = "Erreur de synchronisation système."; }
            finally { _isLoading = false; }
        }

        private void OpenDeleteModal(Guid id, string name, bool isFeedback)
        {
            targetId = id;
            targetName = name;
            isDeletingFeedback = isFeedback;
            showDeleteModal = true;
        }

        private void CloseDeleteModal() => showDeleteModal = false;

        private async Task ConfirmDelete()
        {
            try
            {
                if (isDeletingFeedback) await ApiClient.DeleteRoomFeedbackAsync(targetId);
                else await ApiClient.DeleteRoomArcadeMachineAsync(Id, targetId);

                showDeleteModal = false;
                await LoadData();
            }
            catch { _errorMessage = "PROTOCOLE ÉCHOUÉ : Impossible d'effacer les données."; }
        }

        protected void NavigateToAddMachine() => Nav.NavigateTo($"/rooms/machines/add?roomId={Id}");
    }
}
