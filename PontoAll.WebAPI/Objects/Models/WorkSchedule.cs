using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("workschedule")]
public class WorkSchedule
{
    [Column("id")]
    public int Id { get; set; }

    [Column("dayofmonth")]
    public int DayOfMonth { get; set; }

    [Column("yearmonth")]
    public string YearMonth { get; set; }

    [Column("daytype")]
    public ScheduleDayType DayType { get; set; }

    [Column("marktime1")]
    public TimeOnly? MarkTime1 { get; set; }

    [Column("marktime2")]
    public TimeOnly? MarkTime2 { get; set; }

    [Column("marktime3")]
    public TimeOnly? MarkTime3 { get; set; }

    [Column("marktime4")]
    public TimeOnly? MarkTime4 { get; set; }

    [Column("marktime5")]
    public TimeOnly? MarkTime5 { get; set; }

    [Column("marktime6")]
    public TimeOnly? MarkTime6 { get; set; }

    [Column("marktime7")]
    public TimeOnly? MarkTime7 { get; set; }

    [Column("marktime8")]
    public TimeOnly? MarkTime8 { get; set; }

    [Column("marktime9")]
    public TimeOnly? MarkTime9 { get; set; }

    [Column("marktime10")]
    public TimeOnly? MarkTime10 { get; set; }

    [Column("userid")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("geofenceid")]
    public int GeofenceId { get; set; }
    public Geofence Geofence { get; set; } = null!;

    public WorkSchedule()
    {

    }

    public WorkSchedule(int id, int dayOfMonth, string yearMonth, ScheduleDayType dayType, int userId, int geofenceId)
    {
        Id = id;
        DayOfMonth = dayOfMonth;
        YearMonth = yearMonth;
        DayType = dayType;
        UserId = userId;
        GeofenceId = geofenceId;
    }
}