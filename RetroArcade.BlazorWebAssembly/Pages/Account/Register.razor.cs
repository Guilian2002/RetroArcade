using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject] public AuthenticationStateProvider AuthProvider { get; set; } = default!;
        [Inject] public AuthentificationAPIClient AuthClient { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        protected CreateAccountViewModel model = new();
        protected string? errorMessage;
        protected bool isSubmitting = false;

        protected bool showPassword = false;
        protected string passwordType => showPassword ? "text" : "password";

        protected void TogglePassword() => showPassword = !showPassword;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated ?? false)
            {
                Navigation.NavigateTo("/");
            }
        }

        protected async Task HandleRegister()
        {
            if (isSubmitting) return;

            errorMessage = null;
            isSubmitting = true;

            try
            {
                bool success = await AuthClient.AccountCreateAsync(
                    model.Firstname, model.Lastname, model.Username, model.Email, model.Password);

                if (success)
                {
                    Navigation.NavigateTo("/Account/Login");
                }
                else
                {
                    errorMessage = "ACCÈS REFUSÉ : Les données sont invalides ou l'email est déjà utilisé.";
                }
            }
            catch (Exception)
            {
                errorMessage = "ERREUR SYSTÈME : Connexion au serveur impossible.";
            }
            finally
            {
                isSubmitting = false;
            }
        }
    }
}
