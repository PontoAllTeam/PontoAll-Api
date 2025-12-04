using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Services.Entities;

public class DailyRecordService : GenericService<DailyRecord, DailyRecordDTO>, IDailyRecordService
{
    private readonly IDailyRecordRepository _dailyRecordRepository;
    private readonly ITimeRecordRepository _timeRecordRepository;
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IMapper _mapper;

    public DailyRecordService(IDailyRecordRepository repository, ITimeRecordRepository timeRecordRepository, IWorkScheduleRepository workScheduleRepository, IMapper mapper) : base(repository, mapper)
    {
        _dailyRecordRepository = repository;
        _timeRecordRepository = timeRecordRepository;
        _workScheduleRepository = workScheduleRepository;
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

    public async Task CalculateAndUpdateDailyRecord(int dailyRecordId)
    {
        var dailyRecord = await _dailyRecordRepository.GetById(dailyRecordId);
        if (dailyRecord == null) return;

        var allTimeRecords = await _timeRecordRepository.Get();
        var timeRecords = allTimeRecords.Where(tr => tr.DailyRecordId == dailyRecordId).ToList();
        var workSchedule = await _workScheduleRepository.GetById(dailyRecord.WorkScheduleId);

        if (workSchedule != null)
        {
            DailyRecordCalculator.CalculateValues(dailyRecord, timeRecords, workSchedule);
            await _dailyRecordRepository.Update(dailyRecord);
            await _dailyRecordRepository.SaveChanges();
        }
    }
}