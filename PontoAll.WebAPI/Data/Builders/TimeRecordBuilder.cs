using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class TimeRecordBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeRecord>().HasKey(tr => tr.Id);
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Date).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Time).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Justification);
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.UserId).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.DailyRecordId).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.WorkScheduleId).IsRequired();
        modelBuilder.Entity<TimeRecord>().OwnsOne(tr => tr.Location, loc =>
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
