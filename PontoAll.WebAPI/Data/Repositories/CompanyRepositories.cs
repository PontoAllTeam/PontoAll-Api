using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
