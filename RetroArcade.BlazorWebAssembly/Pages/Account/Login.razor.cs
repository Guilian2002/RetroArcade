using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    public partial class Login : ComponentBase
    {
        [Inject] public AuthentificationAPIClient AuthClient { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthProvider { get; set; } = default!;

        protected LoginViewModel loginModel = new();
        protected string? error;
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

        protected async Task HandleLogin()
        {
            if (isSubmitting) return;

            error = null;
            isSubmitting = true;

            try
            {
                bool success = await AuthClient.LoginAsync(loginModel.Email, loginModel.Password);

                if (success)
                {
                    if (AuthProvider is CustomAuthenticationStateProvider customProvider)
                    {
                        customProvider.NotifyUserChanged();
                    }

                    Navigation.NavigateTo("/");
                }
                else
                {
                    error = "ACCÈS REFUSÉ : Identifiants ou accès invalides.";
                }
            }
            catch (Exception)
            {
                error = "ERREUR SYSTÈME : Serveur d'authentification injoignable.";
            }
            finally
            {
                isSubmitting = false;
            }
        }
    }
}
