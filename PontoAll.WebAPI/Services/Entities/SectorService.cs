using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class SectorService : GenericService<Sector, SectorDTO>, ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IMapper _mapper;

    public SectorService(ISectorRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _sectorRepository = repository;
        _mapper = mapper;
    }
}
