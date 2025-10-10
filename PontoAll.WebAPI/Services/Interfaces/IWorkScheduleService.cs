using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IWorkScheduleService : IGenericService<WorkSchedule, WorkScheduleDTO>
{
    Task<int> CreateByDepartment(int departmentId, WorkScheduleDTO template);
    Task<int> CreateBySector(int sectorId, WorkScheduleDTO template);
}