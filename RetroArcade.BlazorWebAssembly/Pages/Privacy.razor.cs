using Microsoft.AspNetCore.Components;

namespace RetroArcade.BlazorWebAssembly.Pages
{
    public partial class Privacy
    {
        private string _signature = string.Empty;
        private string _dateMaj = "29.01.2026";

        protected override void OnInitialized()
        {
            // Génère une signature unique pour la session
            _signature = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}
