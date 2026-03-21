using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomArcadeMachine
{
    public class AddRoomArcadeMachineForm
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner une machine du catalogue.")]
        public Guid ArcadeMachineId { get; set; }

        [Required(ErrorMessage = "L'état est obligatoire.")]
        public string State { get; set; } = "Active";

        [Required(ErrorMessage = "La date d'installation est requise.")]
        public DateTime InstallationDate { get; set; } = DateTime.Now;
    }
}
