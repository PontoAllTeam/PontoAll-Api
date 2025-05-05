using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.Security.Claims;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserDTO userDTO)
    {
        var secretKey = _configuration["Jwt:Key"];

        if (secretKey is null)
        {
            throw new Exception("A chave JWT é nula");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                    new Claim(JwtRegisteredClaimNames.Sub, userDTO.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userDTO.Email),
                ]),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public async Task<bool> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var handler = new JsonWebTokenHandler();

        // Parâmetros de validação
        var validationParameters = new TokenValidationParameters
        {
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        var result = await handler.ValidateTokenAsync(token, validationParameters);

        if (!result.IsValid || result.SecurityToken == null)
        {
            return false;
        }

        return true;
    }

    public string ExtractSubjectEmail(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return string.Empty;
        }

        var handler = new JsonWebTokenHandler();

        if (!handler.CanReadToken(token))
        {
            return string.Empty;
        }

        var jwtToken = handler.ReadJsonWebToken(token);

        if (jwtToken is null)
        {
            return string.Empty;
        }

        var subjectClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        return subjectClaim ?? string.Empty;
    }
}
