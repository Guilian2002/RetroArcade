using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building
{
    public class AddBuildingForm
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public TimeOnly OpeningHour { get; set; } = new TimeOnly(8, 0);

        [Required]
        public TimeOnly ClosingHour { get; set; } = new TimeOnly(22, 0);

        [Required(ErrorMessage = "La rue est obligatoire.")]
        public string AddressStreet { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro est obligatoire.")]
        public string AddressNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code postal est obligatoire.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ville est obligatoire.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le pays est obligatoire.")]
        public string Country { get; set; } = "Belgique";
    }
}
