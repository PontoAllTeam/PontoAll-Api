using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Enums;

namespace PontoAll.WebAPI.Services.Entities;

public class DailyRecordService : GenericService<DailyRecord, DailyRecordDTO>, IDailyRecordService
{
    private readonly IDailyRecordRepository _dailyRecordRepository;
    private readonly IMapper _mapper;

    public DailyRecordService(IDailyRecordRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _dailyRecordRepository = repository;
        _mapper = mapper;
    }

    public async Task<int> EnsureDailyRecordExists(int employeeId, int workScheduleId, DateOnly date)
    {
        var dailyRecords = await _dailyRecordRepository.Get();
        var existingRecord = dailyRecords.FirstOrDefault(dr =>
            dr.EmployeeId == employeeId &&
            dr.Date == date);

        if (existingRecord != null)
            return existingRecord.Id;

        var newDailyRecord = new DailyRecord
        {
            Date = date,
            TotalWorkedHours = 0,
            ExpectedHours = 8,
            OvertimeHours = 0,
            MissingHours = 0,
            IsAbsent = false,
            ReviewStatus = ReviewStatus.PENDING,
            WorkScheduleId = workScheduleId,
            EmployeeId = employeeId
        };

        await _dailyRecordRepository.Add(newDailyRecord);
        await _dailyRecordRepository.SaveChanges();

        return newDailyRecord.Id;
    }
}