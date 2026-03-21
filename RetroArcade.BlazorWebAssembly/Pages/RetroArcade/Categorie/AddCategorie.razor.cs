using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Categorie
{
    public partial class AddCategorie
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected AddCategorieForm _form = new();
        protected string? _errorMessage;
        protected bool _isSubmitting = false;

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                await ApiClient.AddCategorieAsync(_form);
                Nav.NavigateTo("/categories/list");
            }
            catch (Exception ex)
            {
                _errorMessage = "ERREUR DE TRANSMISSION : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
