using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> Login(Login login);
}
