using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class WorkScheduleService : GenericService<WorkSchedule, WorkScheduleDTO>, IWorkScheduleService
{
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IMapper _mapper;

    public WorkScheduleService(IWorkScheduleRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _workScheduleRepository = repository;
        _mapper = mapper;
    }
}