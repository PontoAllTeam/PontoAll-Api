using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class GeofenceBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Geofence>().HasKey(g => g.Id);
        modelBuilder.Entity<Geofence>().Property(g => g.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Geofence>().Property(g => g.Name).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.CompanyId).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point1Lat).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point1Lon).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point2Lat).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point2Lon).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point3Lat).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point3Lon).IsRequired();
        modelBuilder.Entity<Geofence>().Property(g => g.Point4Lat);
        modelBuilder.Entity<Geofence>().Property(g => g.Point4Lon);
        modelBuilder.Entity<Geofence>().Property(g => g.Point5Lat);
        modelBuilder.Entity<Geofence>().Property(g => g.Point5Lon);

        /*
         * N�o � poss�vel adicionar dados iniciais aqui porque o EF Core n�o aceita
         * tipos complexos como Geolocation para esse tipo de opera��o.
         */
        modelBuilder.Entity<Geofence>().HasData(new List<Geofence>
        {
            new(1, "P�tio 1", 10.0, 10.0, 10.1, 10.1, 10.2, 10.0, null, null, null, null, 1),
        });
    }
}
