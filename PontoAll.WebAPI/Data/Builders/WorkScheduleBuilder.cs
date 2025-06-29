using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class WorkScheduleBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkSchedule>().HasKey(ws => ws.Id);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.DayOfMonth).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.YearMonth).IsRequired().HasMaxLength(7);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.DayType).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.UserId).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.GeofenceId).IsRequired();

        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime1);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime2);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime3);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime4);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime5);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime6);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime7);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime8);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime9);
        modelBuilder.Entity<WorkSchedule>().Property(ws => ws.MarkTime10);

        modelBuilder.Entity<WorkSchedule>().HasData(new List<WorkSchedule>
        {
            new(1, 30, "2025/04", ScheduleDayType.WORK_DAY, 1, 1),
            new(2, 31, "2025/04", ScheduleDayType.WORK_DAY, 2, 1),
        });
    }
}
