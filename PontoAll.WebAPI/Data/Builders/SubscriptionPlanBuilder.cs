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

        modelBuilder.Entity<SubscriptionPlan>().HasData(new List<SubscriptionPlan>
        {
            new(1, SubscriptionPlanType.FREE_TRIAL, new DateTime(2025, 6, 20), new DateTime(2025, 6, 30), true, (decimal) 0.0, PaymentMethod.PIX, DateTime.Now, DateTime.Now, false, 1),
        });
    }
}
