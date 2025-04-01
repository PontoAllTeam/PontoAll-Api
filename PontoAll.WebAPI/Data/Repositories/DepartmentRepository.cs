using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
{
    private readonly AppDbContext _context;
    public DepartmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
