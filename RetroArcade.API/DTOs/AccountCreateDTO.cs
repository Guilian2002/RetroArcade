using RetroArcade.Domain.Domain.Entities;

namespace RetroArcade.API.DTOs
{
    public record AccountCreateDTO(string Firstname, string Lastname, string Username, 
                                    string Email, string Password, Role Role)
    {

    }
}
