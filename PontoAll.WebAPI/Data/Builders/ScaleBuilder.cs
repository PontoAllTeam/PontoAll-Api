using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class ScaleBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scale>().HasKey(s => s.Id);
        modelBuilder.Entity<Scale>().Property(s => s.Day).IsRequired();
        modelBuilder.Entity<Scale>().Property(s => s.YearMonth).IsRequired().HasMaxLength(7);
        modelBuilder.Entity<Scale>().Property(s => s.DayType).IsRequired();

        modelBuilder.Entity<Scale>().Property(s => s.Pick1);
        modelBuilder.Entity<Scale>().Property(s => s.Pick2);
        modelBuilder.Entity<Scale>().Property(s => s.Pick3);
        modelBuilder.Entity<Scale>().Property(s => s.Pick4);
        modelBuilder.Entity<Scale>().Property(s => s.Pick5);
        modelBuilder.Entity<Scale>().Property(s => s.Pick6);
        modelBuilder.Entity<Scale>().Property(s => s.Pick7);
        modelBuilder.Entity<Scale>().Property(s => s.Pick8);
        modelBuilder.Entity<Scale>().Property(s => s.Pick9);
        modelBuilder.Entity<Scale>().Property(s => s.Pick10);

        modelBuilder.Entity<Scale>().HasData(new List<Scale>
        {
            new(1, 30, "2025/04", DayType.WORK_DAY, 1),
            new(2, 31, "2025/04", DayType.WORK_DAY, 1),
        });
    }
}
