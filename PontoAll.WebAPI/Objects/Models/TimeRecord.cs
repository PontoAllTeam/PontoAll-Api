using PontoAll.WebAPI.Objects.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("timerecord")]
public class TimeRecord
{
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("time")]
    public TimeOnly Time { get; set; }

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("longitude")]
    public double Longitude { get; set; }

    [NotMapped]
    public Geolocation Location => new(Latitude, Longitude);

    [Column("justification")]
    public string? Justification { get; set; }

    [Column("userid")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("dailyrecordid")]
    public int DailyRecordId { get; set; }
    public DailyRecord DailyRecord { get; set; } = null!;

    [Column("workscheduleid")]
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; } = null!;

    public TimeRecord() { }

    public TimeRecord(int id, DateOnly date, TimeOnly time, double latitude, double longitude, string? justification, int userId, int dailyRecordId, int workScheduleId)
    {
        Id = id;
        Date = date;
        Time = time;
        Latitude = latitude;
        Longitude = longitude;
        UserId = userId;
        Justification = justification;
        DailyRecordId = dailyRecordId;
        WorkScheduleId = workScheduleId;
    }
}