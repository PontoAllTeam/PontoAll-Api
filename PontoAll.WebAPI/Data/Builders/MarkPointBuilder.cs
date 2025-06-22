using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class MarkPointBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeRecord>().HasKey(m => m.Id);
        modelBuilder.Entity<TimeRecord>().Property(m => m.Date).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(m => m.Time).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(m => m.UserId).IsRequired();
        modelBuilder.Entity<TimeRecord>().OwnsOne(m => m.Location, loc =>
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
