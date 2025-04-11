using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PontoAll.WebAPI.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
    Task<User> Login(Login login);
}
