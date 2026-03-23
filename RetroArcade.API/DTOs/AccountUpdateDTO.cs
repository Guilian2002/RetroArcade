using RetroArcade.Domain.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RetroArcade.API.DTOs
{
    public record AccountUpdateDTO(Guid Id, string Firstname, string Lastname, string Username, string Role);
}
