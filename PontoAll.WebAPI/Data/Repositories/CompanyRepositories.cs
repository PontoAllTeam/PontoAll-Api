using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PontoAll.WebAPI.Data.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Company> GetByCNPJ(string cnpj)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
    }

}
