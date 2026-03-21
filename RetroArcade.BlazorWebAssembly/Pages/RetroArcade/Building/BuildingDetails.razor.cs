using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Building
{
    public partial class BuildingDetails
    {
        [Inject] public RetroArcadeAPIClient Agency { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        private BuildingViewModel? _building;
        private bool _isLoading = true;
        private string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                _building = await Agency.GetBuildingDetailsAsync(Id);
            }
            catch (Exception ex)
            {
                _errorMessage = "ERREUR SYSTÈME : Liaison avec le complexe interrompue.";
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
