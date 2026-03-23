using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Building
{
    [Authorize(Roles = "Manager")]
    public partial class AddBuilding
    {
        [Inject] public RetroArcadeAPIClient Agency { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected AddBuildingForm _form = new();
        protected string? _errorMessage;
        protected bool _isSubmitting = false;

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                await Agency.AddBuildingAsync(_form);
                Nav.NavigateTo("/buildings");
            }
            catch (Exception ex)
            {
                _errorMessage = "ERREUR DE TRANSMISSION : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
