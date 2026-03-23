using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Booking
{
    [Authorize(Roles = "Manager")]
    public partial class SeeAllBookingsManager : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected List<BookingViewModel>? _bookings;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                var result = await ApiClient.GetBookingsByManagerAsync();
                _bookings = result.OrderBy(b => b.BeginDate).ToList();
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
