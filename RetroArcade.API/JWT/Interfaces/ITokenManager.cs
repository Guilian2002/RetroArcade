using RetroArcade.API.DTOs;

namespace RetroArcade.API.JWT.Interfaces
{
    public interface ITokenManager
    {
        string GenerateToken(TokenAccountDTO account);
    }
}
