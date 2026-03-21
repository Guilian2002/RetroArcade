using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Booking
{
    public partial class SeeAllBookings : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected List<BookingViewModel>? _bookings;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        // Gestion de la modale
        protected bool _showDeleteModal = false;
        private Guid _bookingIdToDelete;

        protected override async Task OnInitializedAsync()
        {
            await LoadBookings();
        }

        private async Task LoadBookings()
        {
            try
            {
                _isLoading = true;
                var result = await ApiClient.GetAllBookingsByUserAsync();
                _bookings = result.OrderByDescending(b => b.BeginDate).ToList();
            }
            catch (Exception ex)
            {
                _errorMessage = "Erreur de synchronisation : " + ex.Message;
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected void ShowDeleteModal(Guid id)
        {
            _bookingIdToDelete = id;
            _showDeleteModal = true;
        }

        protected void CloseDeleteModal()
        {
            _showDeleteModal = false;
        }

        protected async Task ConfirmCancel()
        {
            try
            {
                _showDeleteModal = false;
                await ApiClient.DeleteBookingAsync(_bookingIdToDelete);
                await LoadBookings();
            }
            catch (Exception ex)
            {
                _errorMessage = "Échec de l'annulation : " + ex.Message;
            }
        }
    }
}
