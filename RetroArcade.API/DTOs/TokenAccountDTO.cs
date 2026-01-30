using RetroArcade.Domain.Domain.Entities;

namespace RetroArcade.API.DTOs
{
    public record TokenAccountDTO (Guid Id, string Email, Role Role)
    {
    }
}
