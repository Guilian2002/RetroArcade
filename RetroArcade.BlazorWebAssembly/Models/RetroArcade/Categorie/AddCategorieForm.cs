using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie
{
    public class AddCategorieForm
    {
        [Required(ErrorMessage = "Le nom de la catégorie est obligatoire.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit faire entre 3 et 50 caractères.")]
        [Display(Name = "Nom de la catégorie")]
        public string Name { get; set; } = string.Empty;
    }
}
