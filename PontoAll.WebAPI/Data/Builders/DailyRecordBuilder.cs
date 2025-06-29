using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Enums;

namespace PontoAll.WebAPI.Data.Builders;

public class DailyRecordBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyRecord>().HasKey(dr => dr.Id);
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.Date).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.TotalWorkedHours).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.ExpectedHours).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.OvertimeHours).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.MissingHours).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.IsAbsent).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.ReviewStatus).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.WorkScheduleId).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.ReviewerComments);
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.ReviewedAt);
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.EmployeeId).IsRequired();
        modelBuilder.Entity<DailyRecord>().Property(dr => dr.ReviewerId);

        var date = new DateOnly(2025, 6, 20);
        var reviewedAt = new DateTime(2025, 6, 21, 20, 30, 0, DateTimeKind.Utc);

        modelBuilder.Entity<DailyRecord>().HasData(new List<DailyRecord>
        {
            new(1, date, 10, 8, 2, 0, false, ReviewStatus.APPROVED, "Aprovado", reviewedAt, 2, 2, 1),
        });
    }
}
