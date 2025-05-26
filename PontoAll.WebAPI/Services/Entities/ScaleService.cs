using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class ScaleService : GenericService<Scale, ScaleDTO>, IScaleService
{
    private readonly IScaleRepository _scaleRepository;
    private readonly IMapper _mapper;

    public ScaleService(IScaleRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _scaleRepository = repository;
        _mapper = mapper;
    }

    public async Task CreateValidatedAsync(ScaleDTO dto)
    {
        await Create(dto);
    }

    public async Task UpdateValidatedAsync(ScaleDTO dto, int id)
    {
        await Update(dto, id);
    }
}
