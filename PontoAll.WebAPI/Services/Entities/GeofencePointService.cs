using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class GeofencePointService : GenericService<GeofencePoint, GeofencePointDTO>, IGeofencePointService
{
    private readonly IGeofencePointRepository _geofencePointRepository;
    private readonly IMapper _mapper;

    public GeofencePointService(IGeofencePointRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _geofencePointRepository = repository;
        _mapper = mapper;
    }
}
