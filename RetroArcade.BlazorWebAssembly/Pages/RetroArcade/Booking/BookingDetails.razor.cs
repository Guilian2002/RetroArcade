using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Booking
{
    [Authorize]
    public partial class BookingDetails : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected BookingViewModel? _booking;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _booking = await ApiClient.GetBookingByIdAsync(Id);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
