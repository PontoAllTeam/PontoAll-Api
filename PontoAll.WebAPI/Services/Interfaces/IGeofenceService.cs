using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IGeofenceService : IGenericService<Geofence, GeofenceDTO>
{
    Task<bool> IsInsideGeofence(Geolocation location, int geofenceId);
}
