using RetroArcade.Domain.Domain.Entities;

namespace RetroArcade.API.DTOs
{
    public record BookingCreateDTO (DateTime BeginDate, DateTime EndDate, int GroupSize, Status Status,
        Decimal Price, Guid RoomId);
}
