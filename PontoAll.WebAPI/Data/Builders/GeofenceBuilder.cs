using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class GeofenceBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Geofence>().HasKey(g => g.Id);
        modelBuilder.Entity<Geofence>().Property(g => g.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Geofence>().Property(g => g.RadiusInMeters).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.CenterLatitude).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.CenterLongitude).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.CompanyId).IsRequired();

        modelBuilder.Entity<Geofence>().HasData(new List<Geofence>
        {
            new(1, "Fatec Jales", 50, -20.27594017626513, -50.54124464159447, 1),
        });
    }
}
