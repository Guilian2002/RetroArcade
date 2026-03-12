using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    public partial class Login
    {
        [Inject] public AuthentificationAPIClient AuthClient { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthProvider { get; set; } = default!;

        protected LoginViewModel loginModel = new();
        protected string? error;

        // Gestion de la visibilité
        protected bool showPassword = false;
        protected string passwordType => showPassword ? "text" : "password";
        protected void TogglePassword() => showPassword = !showPassword;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo("/");
            }
        }

        protected async Task HandleLogin()
        {
            error = null;
            bool success = await AuthClient.LoginAsync(loginModel.Email, loginModel.Password);

            if (success)
            {
                ((CustomAuthenticationStateProvider)AuthProvider).NotifyUserChanged();
                Navigation.NavigateTo("/");
            }
            else
            {
                error = "ACCÈS REFUSÉ : Identifiants invalides.";
            }
        }
    }
}