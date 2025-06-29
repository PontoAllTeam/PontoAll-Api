using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class SubscriptionPlanBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubscriptionPlan>().HasKey(sp => sp.Id);
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.SubscriptionType).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.StartDate).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.EndDate).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.IsActive).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.Price).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.PaymentMethod).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.CreatedAt).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.UpdatedAt).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.AutoRenew).IsRequired();
        modelBuilder.Entity<SubscriptionPlan>().Property(sp => sp.CompanyId).IsRequired();

        var startDate = new DateTime(2025, 6, 20, 12, 0, 0, DateTimeKind.Utc);
        var endDate = new DateTime(2025, 6, 30, 23, 59, 59, DateTimeKind.Utc);
        var createdAt = new DateTime(2025, 6, 20, 12, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2025, 6, 21, 13, 30, 0, DateTimeKind.Utc);

        modelBuilder.Entity<SubscriptionPlan>().HasData(new List<SubscriptionPlan>
        {
            new(1, SubscriptionPlanType.FREE_TRIAL, startDate, endDate, true, (decimal) 0.0, PaymentMethod.PIX, createdAt, updatedAt, false, 1),
        });
    }
}
