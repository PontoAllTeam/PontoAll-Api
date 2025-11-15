using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Objects.Contracts;

public class LoginResponse
{
    public string Token { get; set; }
    public UserDTO User { get; set; }

    public LoginResponse()
    {

    }
    public LoginResponse(string token, UserDTO user)
    {
        Token = token;
        User = user;
    }
}
