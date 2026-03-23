using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Booking
{
    [Authorize(Roles = "User")]
    public partial class UpdateBooking : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected UpdateBookingForm _form = new();
        protected string? _errorMessage;
        protected bool _isLoading = true;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var booking = await ApiClient.GetBookingByIdAsync(Id);
                _form.BookingId = booking.Id;
                _form.GroupSize = booking.GroupSize;
            }
            catch (Exception)
            {
                _errorMessage = "Impossible de charger la réservation.";
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
                await ApiClient.UpdateBookingAsync(Id, _form);
                Nav.NavigateTo("/");
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
