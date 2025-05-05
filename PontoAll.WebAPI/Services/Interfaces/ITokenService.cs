using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserDTO userDTO);
    Task<bool> ValidateToken(string token);
    string ExtractSubjectEmail(string token);
}
