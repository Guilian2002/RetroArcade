using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Room
{
    [Authorize(Roles = "Manager")]
    public partial class UpdateRoom
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected UpdateRoomForm _form = new();
        protected string? _errorMessage;
        protected bool _isLoading = true;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                var room = await ApiClient.GetRoomDetailsAsync(Id);

                _form = new UpdateRoomForm
                {
                    Id = room.Id,
                    Name = room.Name,
                    Number = room.Number,
                    MachineCapacity = room.MachineCapacity,
                    Price = room.Price,
                    BuildingId = room.Building.Id
                };
            }
            catch (Exception ex)
            {
                _errorMessage = "ERREUR SYSTÈME : Impossible d'extraire les registres du secteur : " + ex.Message;
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;
            try
            {
                await ApiClient.UpdateRoomAsync(Id, _form);
                Nav.NavigateTo($"/rooms/{_form.BuildingId}");
            }
            catch (Exception ex)
            {
                _errorMessage = "PROTOCOLE ÉCHOUÉ : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
