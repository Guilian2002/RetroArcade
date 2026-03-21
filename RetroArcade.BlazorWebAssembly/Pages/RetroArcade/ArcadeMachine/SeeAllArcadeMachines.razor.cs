using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.ArcadeMachine
{
    public partial class SeeAllArcadeMachines
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected List<ArcadeMachineViewModel>? _machines;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        private bool showDeleteModal = false;
        private ArcadeMachineViewModel? machineToDelete;

        protected override async Task OnInitializedAsync()
        {
            await LoadMachines();
        }

        protected async Task LoadMachines()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                var result = await ApiClient.GetAllArcadeMachinesAsync();
                _machines = result?.ToList() ?? new List<ArcadeMachineViewModel>();
            }
            catch (Exception ex)
            {
                _errorMessage = "ERREUR SYSTÈME : Connexion au catalogue impossible.";
            }
            finally { _isLoading = false; }
        }

        private void OpenDeleteModal(ArcadeMachineViewModel machine)
        {
            machineToDelete = machine;
            showDeleteModal = true;
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
            machineToDelete = null;
        }

        private async Task ConfirmDelete()
        {
            if (machineToDelete != null)
            {
                try
                {
                    await ApiClient.DeleteArcadeMachineAsync(machineToDelete.Id);
                    showDeleteModal = false;
                    await LoadMachines();
                }
                catch (Exception ex)
                {
                    _errorMessage = "CRITICAL ERROR : Impossible de supprimer l'unité.";
                    showDeleteModal = false;
                }
            }
        }
    }
}
