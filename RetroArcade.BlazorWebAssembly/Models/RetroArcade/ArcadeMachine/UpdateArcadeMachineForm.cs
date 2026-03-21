using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine
{
    public class UpdateArcadeMachineForm
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [Display(Name = "Nom de la borne")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom du jeu est obligatoire.")]
        [Display(Name = "Jeu installé")]
        public string GameName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veuillez sélectionner une catégorie.")]
        [Display(Name = "Catégorie")]
        public Guid CategorieId { get; set; }
    }
}
