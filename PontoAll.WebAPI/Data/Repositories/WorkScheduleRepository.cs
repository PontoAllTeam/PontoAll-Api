using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class WorkScheduleRepository : GenericRepository<WorkSchedule>, IWorkScheduleRepository
{
    private readonly AppDbContext _context;

    public WorkScheduleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}