using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomArcadeMachine;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.RoomArcadeMachine
{
    [Authorize(Roles = "Manager")]
    public partial class AddRoomArcadeMachine : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter, SupplyParameterFromQuery] public Guid RoomId { get; set; }

        protected AddRoomArcadeMachineForm _form = new() { InstallationDate = DateTime.Now };
        protected List<ArcadeMachineViewModel> _catalogMachines = new();
        protected string? _errorMessage;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (RoomId == Guid.Empty) { Nav.NavigateTo("/buildings"); return; }

                _form.RoomId = RoomId;
                _form.State = "Active"; // État par défaut

                var result = await ApiClient.GetAllArcadeMachinesAsync();
                _catalogMachines = result?.ToList() ?? new List<ArcadeMachineViewModel>();
            }
            catch { _errorMessage = "ERREUR SYSTÈME : Impossible de charger le catalogue des bornes."; }
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;
            try
            {
                await ApiClient.AddRoomArcadeMachineAsync(_form);
                Nav.NavigateTo($"/rooms/details/{RoomId}");
            }
            catch (Exception ex)
            {
                _errorMessage = "ÉCHEC DE L'INSTALLATION : " + ex.Message;
            }
            finally { _isSubmitting = false; }
        }
    }
}
