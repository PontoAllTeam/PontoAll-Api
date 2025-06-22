using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("subscriptionplan")]
public class SubscriptionPlan
{
    [Column("id")]
    public int Id { get; set; }

    [Column("subscriptiontype")]
    public SubscriptionPlanType SubscriptionType { get; set; }

    [Column("startdate")]
    public DateTime StartDate { get; set; }

    [Column("enddate")]
    public DateTime EndDate { get; set; }

    [Column("isactive")]
    public bool IsActive { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("paymentmethod")]
    public PaymentMethod PaymentMethod { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [Column("updatedat")]
    public DateTime UpdatedAt { get; set; }

    [Column("autorenew")]
    public bool AutoRenew { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public SubscriptionPlan() { }

    public SubscriptionPlan(int id, SubscriptionPlanType subscriptionType, DateTime startDate, DateTime endDate, bool isActive, decimal price, PaymentMethod paymentMethod, DateTime createdAt, DateTime updatedAt, bool autoRenew, int companyId)
    {
        Id = id;
        SubscriptionType = subscriptionType;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = isActive;
        Price = price;
        PaymentMethod = paymentMethod;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AutoRenew = autoRenew;
        CompanyId = companyId;
    }
}
