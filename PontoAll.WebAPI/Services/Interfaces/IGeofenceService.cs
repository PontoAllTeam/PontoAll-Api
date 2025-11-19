using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IGeofenceService : IGenericService<Geofence, GeofenceDTO>
{
    Task<bool> IsInsideGeofence(double latitude, double longitude, int geofenceId);
}
