using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Building;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomArcadeMachine;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomFeedback;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }

        [JsonPropertyName("machineCapacity")]
        public int MachineCapacity { get; set; }

        public decimal Price { get; set; }
        public BuildingViewModel Building { get; set; } = new();

        [JsonPropertyName("roomFeedbacks")]
        public List<RoomFeedbackViewModel> Feedbacks { get; set; } = new();

        [JsonPropertyName("roomArcadeMachines")]
        public List<RoomArcadeMachineViewModel> Machines { get; set; } = new();
    }
}
