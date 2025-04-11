using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IUserService : IGenericService<User, UserDTO>
{
    Task<UserDTO> Login(Login login);
}
