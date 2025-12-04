using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class TimeRecordService : GenericService<TimeRecord, TimeRecordDTO>, ITimeRecordService
{
    private readonly ITimeRecordRepository _timeRecordRepository;
    private readonly IDailyRecordService _dailyRecordService;
    private readonly IMapper _mapper;

    public TimeRecordService(ITimeRecordRepository repository, IDailyRecordService dailyRecordService, IMapper mapper) : base(repository, mapper)
    {
        _timeRecordRepository = repository;
        _dailyRecordService = dailyRecordService;
        _mapper = mapper;
    }

    public new async Task Create(TimeRecordDTO timeRecordDTO)
    {
        var dailyRecordId = await _dailyRecordService.EnsureDailyRecordExists(
            timeRecordDTO.UserId,
            timeRecordDTO.WorkScheduleId,
            timeRecordDTO.Date);

        timeRecordDTO.DailyRecordId = dailyRecordId;

        await base.Create(timeRecordDTO);
    }
}
