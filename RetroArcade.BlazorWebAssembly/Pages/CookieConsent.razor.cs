using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

namespace RetroArcade.BlazorWebAssembly.Components
{
    public partial class CookieConsent
    {
        [Inject] public ILocalStorageService LocalStorage { get; set; } = default!;

        private bool _showBanner = false; // Par défaut invisible
        private const string ConsentKey = "cookie_consent_accepted";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    var hasAccepted = await LocalStorage.GetItemAsync<bool>(ConsentKey);

                    if (!hasAccepted)
                    {
                        _showBanner = true;
                        StateHasChanged();
                    }
                }
                catch (Exception)
                {
                    _showBanner = true;
                    StateHasChanged();
                }
            }
        }

        private async Task AcceptCookies()
        {
            await LocalStorage.SetItemAsync(ConsentKey, true);
            _showBanner = false;
        }
    }
}
