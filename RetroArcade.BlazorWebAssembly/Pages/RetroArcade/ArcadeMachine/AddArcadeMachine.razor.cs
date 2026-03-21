using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.ArcadeMachine
{
    [Authorize(Roles = "Manager")]
    public partial class AddArcadeMachine : ComponentBase
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        protected AddArcadeMachineForm _form = new();
        protected List<CategorieViewModel> _categories = new();
        protected string? _errorMessage;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await ApiClient.GetAllCategoriesAsync();
                _categories = result?.ToList() ?? new List<CategorieViewModel>();
            }
            catch (Exception)
            {
                _errorMessage = "ERREUR : Impossible de synchroniser les catégories.";
            }
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;
            try
            {
                await ApiClient.AddArcadeMachineAsync(_form);
                Nav.NavigateTo("/arcademachines");
            }
            catch (Exception ex)
            {
                _errorMessage = "ÉCHEC DE CRÉATION : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
