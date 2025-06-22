using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class TimeRecordService : GenericService<TimeRecord, TimeRecordDTO>, ITimeRecordService
{
    private readonly ITimeRecordRepository _timeRecordRepository;
    private readonly IMapper _mapper;

    public TimeRecordService(ITimeRecordRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _timeRecordRepository = repository;
        _mapper = mapper;
    }
}
