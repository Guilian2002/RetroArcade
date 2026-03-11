using Microsoft.Identity.Client;

namespace RetroArcade.API.DTOs
{
    public record RoomArcadeMachineCreateDTO(Guid RoomId, Guid ArcadeMachineId, string State,
        DateTime InstallationDate);
}
