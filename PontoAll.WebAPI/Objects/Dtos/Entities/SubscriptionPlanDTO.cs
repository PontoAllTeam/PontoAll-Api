namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class SubscriptionPlanDTO
{
    public int Id { get; set; }
    public int SubscriptionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
    public int PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool AutoRenew { get; set; }
    public int CompanyId { get; set; }
}
