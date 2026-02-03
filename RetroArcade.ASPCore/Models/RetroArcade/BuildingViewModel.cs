using System.ComponentModel.DataAnnotations;

namespace RetroArcade.ASPCore.Models.RetroArcade
{
    public class BuildingViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [Display(Name = "Nom du bâtiment")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'heure d'ouverture est requise")]
        [Display(Name = "Heure d'ouverture")]
        [DataType(DataType.Time)]
        public TimeSpan OpeningHour { get; set; }

        [Required(ErrorMessage = "L'heure de fermeture est requise")]
        [Display(Name = "Heure de fermeture")]
        [DataType(DataType.Time)]
        public TimeSpan ClosingHour { get; set; }

        [Required(ErrorMessage = "La rue est obligatoire")]
        [Display(Name = "Rue")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro est obligatoire")]
        [Display(Name = "Numéro")]
        public string Number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code postal est obligatoire")]
        [Display(Name = "Code Postal")]
        // Autorise : 1000 (BE), 75001 (FR), SW1A 1AA (UK), H3Z 2Y7 (CA), 90210-1234 (US)
        [RegularExpression(@"^[a-zA-Z0-9\s-]{3,10}$", ErrorMessage = "Code postal invalide")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ville est obligatoire")]
        [Display(Name = "Ville")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le pays est obligatoire")]
        [Display(Name = "Pays")]
        public string Country { get; set; } = string.Empty;

        public string FullAddress => $"{Street} {Number}, {PostalCode} {City}, {Country}";
    }
}
