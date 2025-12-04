using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IDailyRecordService : IGenericService<DailyRecord, DailyRecordDTO>
{
    Task<int> EnsureDailyRecordExists(int employeeId, int workScheduleId, DateOnly date);
    Task CalculateAndUpdateDailyRecord(int dailyRecordId);
}