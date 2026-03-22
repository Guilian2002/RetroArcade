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

        // Gestion de la modale
        protected bool _showDeleteModal = false;
        private Guid _accountIdToDelete;
        protected string? _targetUsername;

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
            catch (Exception ex) { _errorMessage = "ERREUR DE SYNC : " + ex.Message; }
            finally { _isLoading = false; }
        }

        protected void OpenDeleteModal(Guid id, string username)
        {
            _accountIdToDelete = id;
            _targetUsername = username;
            _showDeleteModal = true;
        }

        protected void CloseDeleteModal()
        {
            _showDeleteModal = false;
        }

        protected async Task ConfirmDelete()
        {
            try
            {
                _showDeleteModal = false;
                await ApiClient.DeleteAccountAsync(_accountIdToDelete);
                await LoadAccounts();
            }
            catch (Exception ex) { _errorMessage = "ECHEC SUPPRESSION : " + ex.Message; }
        }
    }
}
