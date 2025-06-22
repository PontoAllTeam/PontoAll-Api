namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class DailyRecordDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public double TotalWorkedHours { get; set; }
    public double ExpectedHours { get; set; }
    public double OvertimeHours { get; set; }
    public double MissingHours { get; set; }
    public bool IsAbsent { get; set; }
    public int ReviewStatus { get; set; }
    public string? ReviewerComments { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public int WorkScheduleId { get; set; }
    public int EmployeeId { get; set; }
    public int? ReviewerId { get; set; }
}
