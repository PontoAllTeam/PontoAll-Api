using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class BiometricDataRepository : GenericRepository<BiometricData>, IBiometricDataRepository
{
    private readonly AppDbContext _context;

    public BiometricDataRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BiometricData?> GetByUserId(int userId)
    {
        return await _context.BiometricDatas.AsNoTracking().FirstOrDefaultAsync(b => b.UserId == userId);
    }
}