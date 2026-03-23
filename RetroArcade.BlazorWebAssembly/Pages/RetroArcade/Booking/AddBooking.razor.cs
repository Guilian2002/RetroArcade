using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Booking
{
    [Authorize(Roles = "User")]
    public partial class AddBooking
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        [Parameter, SupplyParameterFromQuery] public Guid RoomId { get; set; }

        private AddBookingForm _form = new() { BeginDate = DateTime.Now.AddHours(1).Date.AddHours(DateTime.Now.Hour + 1) };
        private RoomViewModel? _room;
        private string? _errorMessage;
        private bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            if (RoomId == Guid.Empty)
            {
                Navigation.NavigateTo("/buildings");
                return;
            }

            try
            {
                _room = await ApiClient.GetRoomDetailsAsync(RoomId);

                if (_room != null && _room.Building != null)
                {
                    _form.RoomId = RoomId;
                    _form.Price = _room.Price;

                    _form.OpeningHour = new TimeSpan(_room.Building.OpeningHour.Hours, _room.Building.OpeningHour.Minutes, 0);
                    _form.ClosingHour = new TimeSpan(_room.Building.ClosingHour.Hours, _room.Building.ClosingHour.Minutes, 0);
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"ERREUR SYSTÈME : {ex.Message}";
            }
        }

        private async Task HandleSubmit()
        {
            if (_room == null) return;

            _isSubmitting = true;
            _errorMessage = null;

            _form.BeginDate = new DateTime(
                _form.BeginDate.Year,
                _form.BeginDate.Month,
                _form.BeginDate.Day,
                _form.BeginDate.Hour,
                0, 0);

            _form.EndDate = _form.BeginDate.AddHours(1);

            try
            {
                await ApiClient.AddBookingAsync(_form);
                Navigation.NavigateTo("/buildings");
            }
            catch (Exception ex)
            {
                _errorMessage = "ÉCHEC DE LA TRANSMISSION : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
