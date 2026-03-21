using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public partial class SeeAllAccounts : ComponentBase
    {
        [Inject] public AuthentificationAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected List<AccountDetailsViewModel>? _accounts;
        protected bool _isLoading = true;
        protected string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
        }

        private async Task LoadAccounts()
        {
            try
            {
                _isLoading = true;
                var result = await ApiClient.GetAllAccountsAsync();
                _accounts = result.ToList();
            }
            catch (Exception ex) { _errorMessage = ex.Message; }
            finally { _isLoading = false; }
        }

        protected async Task HandleDelete(Guid id)
        {
            try
            {
                await ApiClient.DeleteAccountAsync(id);
                await LoadAccounts();
            }
            catch (Exception ex) { _errorMessage = ex.Message; }
        }
    }
}
