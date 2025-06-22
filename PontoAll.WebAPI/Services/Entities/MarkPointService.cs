using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class MarkPointService : GenericService<TimeRecord, MarkPointDTO>, IMarkPointService
{
    private readonly IMarkPointRepository _markPointRepository;
    private readonly IMapper _mapper;

    public MarkPointService(IMarkPointRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _markPointRepository = repository;
        _mapper = mapper;
    }
}
