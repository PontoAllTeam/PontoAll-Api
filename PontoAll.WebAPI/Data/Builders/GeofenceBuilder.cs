using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class GeofenceBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Geofence>().HasKey(g => g.Id);
        modelBuilder.Entity<Geofence>().Property(g => g.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Geofence>().Property(g => g.CompanyId).IsRequired();

        modelBuilder.Entity<Geofence>().HasData(new List<Geofence>
        {
            new(1, "Pátio", 1),
            new(2, "Filial", 1),
        });
    }
}
