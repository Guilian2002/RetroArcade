using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomFeedback
{
    public class AddRoomFeedbackForm
    {
        [Required(ErrorMessage = "La note est obligatoire.")]
        [Range(1, 5, ErrorMessage = "La note doit être comprise entre 1 et 5 étoiles.")]
        [Display(Name = "Note")]
        public int Stars { get; set; } = 5;

        [Required(ErrorMessage = "Le commentaire ne peut pas être vide.")]
        [StringLength(500, ErrorMessage = "Le commentaire est trop long (500 caractères max).")]
        [Display(Name = "Votre avis")]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public Guid RoomId { get; set; }
    }
}
