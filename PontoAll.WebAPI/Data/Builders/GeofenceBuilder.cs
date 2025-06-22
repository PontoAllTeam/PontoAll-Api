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
        modelBuilder.Entity<Geofence>().OwnsOne(g => g.Point1, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("point1_lat")
                .IsRequired();
            loc.Property(l => l.Longitude)
                .HasColumnName("point1_lon")
                .IsRequired();
        });
        modelBuilder.Entity<Geofence>().OwnsOne(g => g.Point2, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("point2_lat")
                .IsRequired();
            loc.Property(l => l.Longitude)
                .HasColumnName("point2_lon")
                .IsRequired();
        });
        modelBuilder.Entity<Geofence>().OwnsOne(g => g.Point3, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("point3_lat")
                .IsRequired();
            loc.Property(l => l.Longitude)
                .HasColumnName("point3_lon")
                .IsRequired();
        });
        modelBuilder.Entity<Geofence>().OwnsOne(g => g.Point4, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("point4_lat");
            loc.Property(l => l.Longitude)
                .HasColumnName("point4_lon");
        });
        modelBuilder.Entity<Geofence>().OwnsOne(g => g.Point5, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("point5_lat");
            loc.Property(l => l.Longitude)
                .HasColumnName("point5_lon");
        });

        /*
         * Não é possível adicionar dados iniciais aqui porque o EF Core não aceita
         * tipos complexos como Geolocation para esse tipo de operação.
         */
    }
}
