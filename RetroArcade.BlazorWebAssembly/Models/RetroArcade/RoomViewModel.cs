using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nom de la salle")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Numéro")]
        public int Number { get; set; }

        [Display(Name = "Capacité de machines")]
        public int MachineCapacity { get; } = 10;

        [DataType(DataType.Currency)]
        [Display(Name = "Prix de location")]
        public decimal Price { get; set; }

        public BuildingViewModel Building { get; set; } = new BuildingViewModel();
    }
}
