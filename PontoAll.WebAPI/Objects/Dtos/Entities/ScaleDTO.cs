namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class ScaleDTO
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string YearMonth { get; set; }
    public int DayType { get; set; }
    public TimeOnly? Pick1 { get; set; }
    public TimeOnly? Pick2 { get; set; }
    public TimeOnly? Pick3 { get; set; }
    public TimeOnly? Pick4 { get; set; }
    public TimeOnly? Pick5 { get; set; }
    public TimeOnly? Pick6 { get; set; }
    public TimeOnly? Pick7 { get; set; }
    public TimeOnly? Pick8 { get; set; }
    public TimeOnly? Pick9 { get; set; }
    public TimeOnly? Pick10 { get; set; }
    public int UserId { get; set; }
}