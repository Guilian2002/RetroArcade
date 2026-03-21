using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Categorie
{
    public class CategorieViewModel
    {
        public Guid Id { get; set; }

        [Required] 
        public string Name { get; set; } = string.Empty;
    }
}
