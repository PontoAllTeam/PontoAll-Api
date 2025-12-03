using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

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
}