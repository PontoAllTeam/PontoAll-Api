using AutoMapper;
using NuGet.Protocol.Core.Types;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Services.Utils;

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

    public async Task<bool> IsInsideGeofence(double latitude, double longitude, int geofenceId)
    {
        var geofence = await _geofenceRepository.GetById(geofenceId);
        double distance = GeoUtils.DistanceMeters(geofence.CenterLatitude, geofence.CenterLongitude, latitude, longitude);
        return distance <= geofence.RadiusInMeters;
    }
}
