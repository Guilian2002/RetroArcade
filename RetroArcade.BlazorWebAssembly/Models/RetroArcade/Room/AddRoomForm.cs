using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room
{
    public class AddRoomForm
    {
        [Required(ErrorMessage = "Le nom de la salle est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom est trop long.")]
        [Display(Name = "Nom de la salle")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de salle est obligatoire.")]
        [Range(1, 999, ErrorMessage = "Le numéro doit être compris entre 1 et 999.")]
        [Display(Name = "Numéro de salle")]
        public int Number { get; set; }

        [Required(ErrorMessage = "La capacité est obligatoire.")]
        [Range(1, 50, ErrorMessage = "La capacité doit être entre 1 et 50.")]
        [Display(Name = "Capacité de machines")]
        public int MachineCapacity { get; set; } = 10;

        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Range(0.01, 5000, ErrorMessage = "Le prix doit être positif.")]
        [Display(Name = "Prix de location")]
        public decimal Price { get; set; }

        [Required]
        public Guid BuildingId { get; set; }
    }
}
