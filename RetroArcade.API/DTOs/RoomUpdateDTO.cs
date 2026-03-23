namespace RetroArcade.API.DTOs
{
    public record RoomUpdateDTO(Guid RoomId, string Name, int Number, int MachineCapacity, Decimal Price, Guid BuildingId);
}
