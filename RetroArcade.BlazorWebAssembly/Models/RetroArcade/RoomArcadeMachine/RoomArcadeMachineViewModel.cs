using RetroArcade.BlazorWebAssembly.Models.RetroArcade.ArcadeMachine;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomArcadeMachine
{
    public class RoomArcadeMachineViewModel
    {
        public string State { get; set; } = "Active";
        public DateTime InstallationDate { get; set; }
        public ArcadeMachineViewModel ArcadeMachine { get; set; } = new();
    }
}
