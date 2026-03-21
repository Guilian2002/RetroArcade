using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine
{
    public class ArcadeMachineViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
        public CategorieViewModel? Categorie { get; set; }
    }
}
