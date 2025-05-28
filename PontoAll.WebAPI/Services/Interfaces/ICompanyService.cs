using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface ICompanyService : IGenericService<Company, CompanyDTO>
{
    // Removi esses métodos porque a validação agora está na Controller
    // Task CreateValidatedAsync(CompanyDTO dto);
    // Task UpdateValidatedAsync(CompanyDTO dto, int id);
}
