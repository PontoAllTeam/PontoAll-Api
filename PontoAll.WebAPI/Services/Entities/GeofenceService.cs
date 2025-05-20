using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class GeofenceService : GenericService<Geofence, GeofenceDTO>, IGeofenceService
{
    private readonly IGeofenceRepository _geofenceRepository;
    private readonly IMapper _mapper;

    public GeofenceService(IGeofenceRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _geofenceRepository = repository;
        _mapper = mapper;
    }
}
