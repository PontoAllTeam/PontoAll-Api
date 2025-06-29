using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class TimeRecordRepository : GenericRepository<TimeRecord>, ITimeRecordRepository
{
    private readonly AppDbContext _context;

    public TimeRecordRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
