using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Data.Builders;

public class GeofencePointBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeofencePoint>().HasKey(gp => gp.Id);
        modelBuilder.Entity<GeofencePoint>().Property(gp => gp.Order).IsRequired();
        modelBuilder.Entity<GeofencePoint>().Property(gp => gp.GeofenceId).IsRequired();
        modelBuilder.Entity<GeofencePoint>().OwnsOne(gp => gp.Location, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("latitude")
                .IsRequired();

            loc.Property(l => l.Longitude)
                .HasColumnName("longitude")
                .IsRequired();
        });

        /*
         * Não é possível adicionar dados iniciais aqui porque o EF Core não aceita
         * tipos complexos como Geolocation para esse tipo de operação.
         */
    }
}
