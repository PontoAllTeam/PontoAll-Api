using AutoMapper;
using NuGet.Protocol.Core.Types;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
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

    public async Task<bool> IsInsideGeofence(Geolocation point, int geofenceId)
    {
        var geofence = await _geofenceRepository.GetById(geofenceId) ?? throw new KeyNotFoundException("A cerca virtual requisitada não existe");
        var geofencePoints = typeof(Geofence).GetProperties()
            .Where(p => p.Name.StartsWith("Point") && p.PropertyType == typeof(Geolocation))
            .OrderBy(p => p.Name)
            .ToList() ?? throw new KeyNotFoundException("Não foi possível encontrar os pontos da cerca virtual");

        int intersections = 0;
        int count = geofencePoints.Count;

        for (int i = 0; i < count; i++)
        {
            var valueA = geofencePoints[i].GetValue(geofence);
            var valueB = geofencePoints[(i + 1) % count].GetValue(geofence);

            if (valueA is not Geolocation pointA || valueB is not Geolocation pointB) continue;

            // Verifica se o ponto está entre os limites verticais da cerca
            if ((pointA.Latitude > point.Latitude) != (pointB.Latitude > point.Latitude))
            {
                // Calcula o ponto de interseção da aresta com a linha de latitude do ponto
                double intersectLongitude = (pointB.Longitude - pointA.Longitude) * 
                    (point.Latitude - pointA.Latitude) / (pointB.Latitude - pointA.Latitude) + pointA.Longitude;

                if (point.Longitude < intersectLongitude)
                {
                    intersections++;
                }
            }
        }

        // Se o número de interseções for ímpar, o ponto está dentro
        return intersections % 2 == 1;
    }
}
