using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class ScaleBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkSchedule>().HasKey(s => s.Id);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Day).IsRequired();
        modelBuilder.Entity<WorkSchedule>().Property(s => s.YearMonth).IsRequired().HasMaxLength(7);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.DayType).IsRequired();

        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick1);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick2);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick3);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick4);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick5);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick6);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick7);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick8);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick9);
        modelBuilder.Entity<WorkSchedule>().Property(s => s.Pick10);

        modelBuilder.Entity<WorkSchedule>().HasData(new List<WorkSchedule>
        {
            new(1, 30, "2025/04", ScheduleDayType.WORK_DAY, 1),
            new(2, 31, "2025/04", ScheduleDayType.WORK_DAY, 1),
        });
    }
}
