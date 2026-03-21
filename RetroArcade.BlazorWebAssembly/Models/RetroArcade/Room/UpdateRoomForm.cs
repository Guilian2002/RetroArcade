using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room
{
    public class UpdateRoomForm
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [Display(Name = "Nom de la salle")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 999)]
        public int Number { get; set; }

        [Required]
        [Range(1, 50)]
        public int MachineCapacity { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Required]
        public Guid BuildingId { get; set; }
    }
}
