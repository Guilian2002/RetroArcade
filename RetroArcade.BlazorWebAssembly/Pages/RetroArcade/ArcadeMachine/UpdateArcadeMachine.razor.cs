using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RetroArcade.BlazorWebAssembly.Clients;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;

namespace RetroArcade.BlazorWebAssembly.Pages.RetroArcade.ArcadeMachine
{
    public partial class UpdateArcadeMachine
    {
        [Inject] public RetroArcadeAPIClient ApiClient { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        protected UpdateArcadeMachineForm _form = new();
        protected List<CategorieViewModel> _categories = new();
        protected string? _errorMessage;
        protected bool _isLoading = true;
        protected bool _isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                var categoriesTask = ApiClient.GetAllCategoriesAsync();
                var machineTask = ApiClient.GetArcadeMachineByIdAsync(Id);

                await Task.WhenAll(categoriesTask, machineTask);

                _categories = categoriesTask.Result.ToList();
                var machine = machineTask.Result;

                _form = new UpdateArcadeMachineForm
                {
                    Id = machine.Id,
                    Name = machine.Name,
                    GameName = machine.GameName,
                    CategorieId = machine.Categorie?.Id ?? Guid.Empty
                };
            }
            catch (Exception)
            {
                _errorMessage = "ERREUR SYSTÈME : Impossible d'accéder aux registres de la borne.";
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;
            try
            {
                await ApiClient.UpdateArcadeMachineAsync(Id, _form);
                Nav.NavigateTo("/arcademachines");
            }
            catch (Exception ex)
            {
                _errorMessage = "PROTOCOLE ÉCHOUÉ : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }
    }
}
