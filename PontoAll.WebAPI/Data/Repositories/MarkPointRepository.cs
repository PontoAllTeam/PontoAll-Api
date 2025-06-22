using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class MarkPointRepository : GenericRepository<TimeRecord>, IMarkPointRepository
{
    private readonly AppDbContext _context;

    public MarkPointRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
