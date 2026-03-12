using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.Authentification;

namespace RetroArcade.BlazorWebAssembly.Pages.Account
{
    public partial class Register
    {
        [Inject] public AuthenticationStateProvider AuthProvider { get; set; } = default!;
        [Inject] public AuthentificationAPIClient AuthClient { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        protected CreateAccountViewModel model = new();
        protected string? errorMessage;

        // Gestion de la visibilité du mot de passe
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

        protected async Task HandleRegister()
        {
            errorMessage = null;
            try
            {
                bool response = await AuthClient.AccountCreateAsync(
                    model.Firstname, model.Lastname, model.Username, model.Email, model.Password);

                if (response)
                {
                    Navigation.NavigateTo("/Account/Login");
                }
                else
                {
                    errorMessage = "ERREUR : L'API a refusé la création (Email déjà utilisé ?).";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"CRASH : {ex.Message}";
            }
        }
    }
}