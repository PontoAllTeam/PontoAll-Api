using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class WorkScheduleService : GenericService<WorkSchedule, WorkScheduleDTO>, IWorkScheduleService
{
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public WorkScheduleService(IWorkScheduleRepository repository,IUserRepository userRepository, IMapper mapper) : base(repository, mapper)
    {
        _workScheduleRepository = repository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<int> CreateByDepartment(int departmentId, WorkScheduleDTO template)
    {
        var users = await _userRepository.GetByDepartmentId(departmentId);
        int created = 0;
        foreach (var user in users)
        {
            var dto = CloneTemplate(template);
            dto.Id = 0;
            dto.UserId = user.Id;
            await Create(dto);
            created++;
        }
        return created;
    }
    public async Task<int> CreateBySector(int sectorId, WorkScheduleDTO template)
    {
        var users = await _userRepository.GetBySectorId(sectorId);
        int created = 0;
        foreach (var user in users)
        {
            var dto = CloneTemplate(template);
            dto.Id = 0;
            dto.UserId = user.Id;
            await Create(dto);
            created++;
        }
        return created;
    }

    public async Task<int> RemoveByDepartment(int departmentId, int dayOfMonth, string yearMonth)
    {
        var users = await _userRepository.GetByDepartmentId(departmentId);
        var schedules = await _workScheduleRepository.Get();
        var toRemove = schedules.Where(s => users.Any(u => u.Id == s.UserId) && s.DayOfMonth == dayOfMonth && s.YearMonth == yearMonth).ToList();
        
        foreach (var schedule in toRemove)
        {
            await _workScheduleRepository.Remove(schedule);
        }
        
        return toRemove.Count;
    }

    public async Task<int> RemoveBySector(int sectorId, int dayOfMonth, string yearMonth)
    {
        var users = await _userRepository.GetBySectorId(sectorId);
        var schedules = await _workScheduleRepository.Get();
        var toRemove = schedules.Where(s => users.Any(u => u.Id == s.UserId) && s.DayOfMonth == dayOfMonth && s.YearMonth == yearMonth).ToList();
        
        foreach (var schedule in toRemove)
        {
            await _workScheduleRepository.Remove(schedule);
        }
        
        return toRemove.Count;
    }
    private static WorkScheduleDTO CloneTemplate(WorkScheduleDTO t) => new WorkScheduleDTO
    {
        Id = 0,
        DayOfMonth = t.DayOfMonth,
        YearMonth = t.YearMonth,
        DayType = t.DayType,
        UserId = t.UserId,
        GeofenceId = t.GeofenceId,
        MarkTime1 = t.MarkTime1,
        MarkTime2 = t.MarkTime2,
        MarkTime3 = t.MarkTime3,
        MarkTime4 = t.MarkTime4,
        MarkTime5 = t.MarkTime5,
        MarkTime6 = t.MarkTime6,
        MarkTime7 = t.MarkTime7,
        MarkTime8 = t.MarkTime8,
        MarkTime9 = t.MarkTime9,
        MarkTime10 = t.MarkTime10,
    };
}