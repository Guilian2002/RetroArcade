using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.Categorie
{
    public partial class SeeAllCategories
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        private List<CategorieViewModel>? _categories;
        private bool _isLoading = true;
        private string? _errorMessage;

        private bool showDeleteModal = false;
        private CategorieViewModel? categoryToDelete;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            try
            {
                _isLoading = true;
                _errorMessage = null;
                var result = await ApiClient.GetAllCategoriesAsync();
                _categories = result?.ToList() ?? new List<CategorieViewModel>();
            }
            catch (Exception)
            {
                _errorMessage = "ERREUR SYSTÈME : Impossible de récupérer les catégories.";
            }
            finally { _isLoading = false; }
        }

        private void OpenDeleteModal(CategorieViewModel category)
        {
            categoryToDelete = category;
            showDeleteModal = true;
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
            categoryToDelete = null;
        }

        private async Task ConfirmDelete()
        {
            if (categoryToDelete != null)
            {
                try
                {
                    await ApiClient.DeleteCategorieAsync(categoryToDelete.Id);
                    showDeleteModal = false;
                    await LoadCategories();
                }
                catch (Exception)
                {
                    _errorMessage = "ERREUR : Protocole de suppression interrompu.";
                    showDeleteModal = false;
                }
            }
        }
    }
}
