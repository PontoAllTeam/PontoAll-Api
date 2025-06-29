namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class WorkScheduleDTO
{
    public int Id { get; set; }
    public int DayOfMonth { get; set; }
    public string YearMonth { get; set; }
    public int DayType { get; set; }
    public string? MarkTime1 { get; set; }
    public string? MarkTime2 { get; set; }
    public string? MarkTime3 { get; set; }
    public string? MarkTime4 { get; set; }
    public string? MarkTime5 { get; set; }
    public string? MarkTime6 { get; set; }
    public string? MarkTime7 { get; set; }
    public string? MarkTime8 { get; set; }
    public string? MarkTime9 { get; set; }
    public string? MarkTime10 { get; set; }
    public int UserId { get; set; }
}
