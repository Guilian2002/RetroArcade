using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Building
{
    public partial class SeeAllBuildings
    {
        [Inject] public RetroArcadeAPIClient Agency { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        private List<BuildingViewModel>? buildings;
        private bool isLoading = true;
        private string? errorMessage;

        private bool showDeleteModal = false;
        private BuildingViewModel? buildingToDelete;

        protected override async Task OnInitializedAsync()
        {
            await LoadBuildings();
        }

        private async Task LoadBuildings()
        {
            try
            {
                isLoading = true;
                errorMessage = null;
                var result = await Agency.GetAllBuildingsAsync();
                buildings = result?.ToList() ?? new List<BuildingViewModel>();
            }
            catch (Exception ex)
            {
                errorMessage = "ERREUR SYSTÈME : Connexion au serveur impossible.";
                Console.WriteLine(ex.Message);
            }
            finally
            {
                isLoading = false;
            }
        }

        protected void NavigateToRooms(Guid buildingId)
        {
            Nav.NavigateTo($"/rooms/{buildingId}");
        }

        protected void NavigateToDetails(Guid buildingId)
        {
            Nav.NavigateTo($"/buildings/details/{buildingId}");
        }

        protected void NavigateToUpdate(Guid buildingId)
        {
            Nav.NavigateTo($"/buildings/update/{buildingId}");
        }

        private void OpenDeleteModal(BuildingViewModel building)
        {
            buildingToDelete = building;
            showDeleteModal = true;
            StateHasChanged();
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
            buildingToDelete = null;
            StateHasChanged();
        }

        private async Task ConfirmDelete()
        {
            if (buildingToDelete != null)
            {
                try
                {
                    await Agency.DeleteBuildingAsync(buildingToDelete.Id);
                    showDeleteModal = false;
                    await LoadBuildings();
                }
                catch (Exception)
                {
                    errorMessage = "CRITICAL ERROR : Suppression interrompue.";
                    showDeleteModal = false;
                }
            }
        }
    }
}
