using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Building
{
    public partial class UpdateBuilding
    {
        [Inject] public RetroArcadeAPIClient Agency { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected UpdateBuildingForm _form = new();
        protected string? _errorMessage;
        protected bool _isLoading = true;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                var b = await Agency.GetBuildingDetailsAsync(Id);

                _form = new UpdateBuildingForm
                {
                    Id = b.Id,
                    Name = b.Name,
                    OpeningHour = TimeOnly.FromTimeSpan(new TimeSpan(b.OpeningHour.Hours, b.OpeningHour.Minutes, 0)),
                    ClosingHour = TimeOnly.FromTimeSpan(new TimeSpan(b.ClosingHour.Hours, b.ClosingHour.Minutes, 0)),
                    AddressStreet = b.Street,
                    AddressNumber = b.Number,
                    PostalCode = b.PostalCode,
                    City = b.City,
                    Country = b.Country
                };
            }
            catch (Exception ex)
            {
                _errorMessage = "Échec du chargement des données système : " + ex.Message;
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
                await Agency.UpdateBuildingAsync(Id, _form);
                Nav.NavigateTo("/buildings");
            }
            catch (Exception ex)
            {
                _errorMessage = "Erreur lors de la synchronisation : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
