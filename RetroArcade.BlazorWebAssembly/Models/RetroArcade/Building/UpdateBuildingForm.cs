using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building
{
    public class UpdateBuildingForm
    {
        [Required] public Guid Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")] public string Name { get; set; } = string.Empty;

        [Required] public TimeOnly OpeningHour { get; set; }
        [Required] public TimeOnly ClosingHour { get; set; }

        [Required] public string AddressStreet { get; set; } = string.Empty;
        [Required] public string AddressNumber { get; set; } = string.Empty;
        [Required] public string PostalCode { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string Country { get; set; } = string.Empty;
    }
}
