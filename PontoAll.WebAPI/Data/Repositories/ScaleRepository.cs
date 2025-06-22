using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class ScaleRepository : GenericRepository<WorkSchedule>, IScaleRepository
{
    private readonly AppDbContext _context;

    public ScaleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}