using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class GeofenceRepository : GenericRepository<Geofence>, IGeofenceRepository
{
    private readonly AppDbContext _context;

    public GeofenceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
