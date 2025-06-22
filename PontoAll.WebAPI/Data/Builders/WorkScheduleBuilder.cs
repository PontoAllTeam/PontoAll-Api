using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class WorkScheduleBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkSchedule>().HasKey(s => s.Id);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.DayOfMonth).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(s => s.YearMonth).IsRequired().HasMaxLength(7);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.DayType).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(s => s.UserId).IsRequired();

        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime1);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime2);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime3);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime4);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime5);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime6);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime7);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime8);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime9);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.MarkTime10);

        modelBuilder.Entity<WorkSchedule>().HasData(new List<WorkSchedule>
        {
            new(1, 30, "2025/04", ScheduleDayType.WORK_DAY, 1),
            new(2, 31, "2025/04", ScheduleDayType.WORK_DAY, 1),
        });
    }
}
