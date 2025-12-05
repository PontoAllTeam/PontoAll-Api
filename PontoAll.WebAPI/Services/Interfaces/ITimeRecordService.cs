using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface ITimeRecordService : IGenericService<TimeRecord, TimeRecordDTO>
{
    // Novo método para filtrar por usuário
    Task<IEnumerable<TimeRecordDTO>> GetByUserId(int userId);
}