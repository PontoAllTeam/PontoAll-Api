using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Repositories;

public class GeofencePointRepository : GenericRepository<GeofencePoint>, IGeofencePointRepository
{
    private readonly AppDbContext _context;

    public GeofencePointRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
