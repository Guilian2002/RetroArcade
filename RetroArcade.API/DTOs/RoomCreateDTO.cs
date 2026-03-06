namespace RetroArcade.API.DTOs
{
    public record RoomCreateDTO(string Name, int Number, int MachineCapacity, Decimal Price, Guid BuildingId);
}
