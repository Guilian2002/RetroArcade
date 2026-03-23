using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking
{
    public class UpdateBookingForm
    {
        [Required]
        public Guid BookingId { get; set; }

        [Required(ErrorMessage = "La taille du groupe est obligatoire.")]
        [Range(4, 10, ErrorMessage = "Le groupe doit être composé de 4 à 10 personnes.")]
        [Display(Name = "Nombre de participants")]
        public int GroupSize { get; set; }
    }
}
