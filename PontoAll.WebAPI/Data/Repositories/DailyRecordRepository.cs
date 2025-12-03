using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class DailyRecordRepository : GenericRepository<DailyRecord>, IDailyRecordRepository
{
    private readonly AppDbContext _context;

    public DailyRecordRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}