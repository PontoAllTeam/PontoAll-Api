using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Interfaces;

public interface ICompanyRepository : IGenericRepository<Company>
{
    Task<Company> GetByCNPJ(string cnpj);
}
