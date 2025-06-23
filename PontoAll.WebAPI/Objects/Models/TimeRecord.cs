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

    [Column("location")]
    public Geolocation Location { get; set; }

    [Column("justification")]
    public string? Justification { get; set; }

    [Column("userid")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("dailyrecordid")]
    public int DailyRecordId { get; set; }
    public DailyRecord DailyRecord { get; set; } = null!;

    public TimeRecord() { }

    public TimeRecord(int id, DateOnly date, TimeOnly time, Geolocation location, string? justification, int userId, int dailyRecordId)
    {
        Id = id;
        Date = date;
        Time = time;
        Location = location;
        UserId = userId;
        Justification = justification;
        DailyRecordId = dailyRecordId;
    }
}