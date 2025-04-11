using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Login(Login login)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
    }
}
