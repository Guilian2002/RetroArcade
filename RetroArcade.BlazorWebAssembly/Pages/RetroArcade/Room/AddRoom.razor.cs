using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Room
{
    [Authorize(Roles = "Manager")]
    public partial class AddRoom
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter, SupplyParameterFromQuery]
        public Guid BuildingId { get; set; }

        protected AddRoomForm _form = new();
        protected string? _errorMessage;
        protected bool _isSubmitting = false;

        protected override void OnInitialized()
        {
            if (BuildingId == Guid.Empty)
            {
                Nav.NavigateTo("/buildings");
                return;
            }
            _form.BuildingId = BuildingId;
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                await ApiClient.AddRoomAsync(_form);
                Nav.NavigateTo($"/rooms/{BuildingId}");
            }
            catch (Exception ex)
            {
                _errorMessage = "ÉCHEC DE CRÉATION : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
