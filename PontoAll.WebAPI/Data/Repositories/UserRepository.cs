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
    public async Task<User> GetByCPF(string cpf)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
    }
    public async Task<List<User>> GetByDepartmentId(int departmentId)
    {
        var sectorIds = await _context.Sectors.Where(s => s.DepartmentId == departmentId).Select(s => s.Id).ToListAsync();
        return await _context.Users.Where(u => sectorIds.Contains(u.SectorId)).ToListAsync();
    }
    public async Task<List<User>> GetBySectorId(int sectorId)
    {
        return await _context.Users.Where(u => u.SectorId == sectorId).ToListAsync();
    }
}
