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
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Latitude).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Longitude).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.Justification);
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.UserId).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.DailyRecordId).IsRequired();
        modelBuilder.Entity<TimeRecord>().Property(tr => tr.WorkScheduleId).IsRequired();

        var date = new DateOnly(2025, 6, 10);
        var time = new TimeOnly(20, 30);

        modelBuilder.Entity<TimeRecord>().HasData(new List<TimeRecord>
        {
            new(1, date, time, 10.15, 10.15, null, 1, 1, 1),
        });
    }
}
