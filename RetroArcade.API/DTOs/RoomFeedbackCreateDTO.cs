using Microsoft.Identity.Client;
using RetroArcade.Domain.Domain.Entities;

namespace RetroArcade.API.DTOs
{
    public record RoomFeedbackCreateDTO(int Stars, string Comment, Guid RoomId);
}
