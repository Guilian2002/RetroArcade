using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public partial class UpdateAccount : ComponentBase
    {
        [Inject] public AuthentificationAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected UpdateAccountForm _form = new();
        protected string? _errorMessage;
        protected bool _isLoading = true;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var account = await ApiClient.GetAccountByIdAsync(Id);
                _form = new UpdateAccountForm
                {
                    AccountId = account.Id,
                    Firstname = account.Firstname,
                    Lastname = account.Lastname,
                    Username = account.Username,
                    Role = account.Role.ToString()
                };
            }
            catch (Exception) { _errorMessage = "Impossible de charger le compte."; }
            finally { _isLoading = false; }
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            try
            {
                await ApiClient.UpdateAccountAsync(Id, _form);
                Nav.NavigateTo("/admin/accounts");
            }
            catch (Exception ex) { _errorMessage = ex.Message; }
            finally { _isSubmitting = false; }
        }
    }
}
