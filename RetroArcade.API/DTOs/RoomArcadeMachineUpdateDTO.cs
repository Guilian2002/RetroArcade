namespace RetroArcade.API.DTOs
{
    public record RoomArcadeMachineUpdateDTO(Guid RoomId, Guid ArcadeMachineId, string State,
        DateTime InstallationDate);
}
