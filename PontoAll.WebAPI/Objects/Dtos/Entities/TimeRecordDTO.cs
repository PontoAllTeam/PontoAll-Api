using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class TimeRecordDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public Geolocation Location { get; set; }
    public string? Justification { get; set; }
    public int UserId { get; set; }
    public string Photo { get; set; }
    public int DailyRecordId { get; set; }
    public int WorkScheduleId { get; set; }
}
