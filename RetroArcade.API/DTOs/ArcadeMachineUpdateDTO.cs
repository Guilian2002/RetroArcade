namespace RetroArcade.API.DTOs
{
    public record ArcadeMachineUpdateDTO(Guid ArcadeMachineId, string Name, string GameName, Guid CategorieId);
}
