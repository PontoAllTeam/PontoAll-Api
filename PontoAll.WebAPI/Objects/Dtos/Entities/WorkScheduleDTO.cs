namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class WorkScheduleDTO
{
    public int Id { get; set; }
    public int DayOfMonth { get; set; }
    public string YearMonth { get; set; }
    public int DayType { get; set; }
    public TimeOnly? MarkTime1 { get; set; }
    public TimeOnly? MarkTime2 { get; set; }
    public TimeOnly? MarkTime3 { get; set; }
    public TimeOnly? MarkTime4 { get; set; }
    public TimeOnly? MarkTime5 { get; set; }
    public TimeOnly? MarkTime6 { get; set; }
    public TimeOnly? MarkTime7 { get; set; }
    public TimeOnly? MarkTime8 { get; set; }
    public TimeOnly? MarkTime9 { get; set; }
    public TimeOnly? MarkTime10 { get; set; }
    public int UserId { get; set; }
}
