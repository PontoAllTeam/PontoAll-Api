using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class SectorRepository : GenericRepository<Sector>,ISectorRepository
{
    private readonly AppDbContext _context;
    public SectorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
